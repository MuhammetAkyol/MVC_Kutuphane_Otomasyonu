using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_Kutuphane_Otomasyonu.Entities.Mapping;
using MVC_Kutuphane_Otomasyonu.Entities.Model.ViewModel;


namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    [Authorize(Roles = "Admin,Moderatör")]
    //[AllowAnonymous]

    public class KullanicilarController : Controller
    {
        // GET: Kullanicilar
        KutuphaneContext context = new KutuphaneContext();
        KullanicilarDAL kullanicilarDAL = new KullanicilarDAL();
        KullaniciRolleriDAL KullaniciRolleriDAL = new KullaniciRolleriDAL();
        RollerDAL rollerDAL = new RollerDAL();
        KullaniciHareketleriDAL KullaniciHareketleriDAL = new KullaniciHareketleriDAL();

        public void KullaniciHAreketleri(int kullaniciId,int islemYapanId,string aciklama)
        {
            var model = new KullaniciHareketleri
            {
                Aciklama = aciklama,
                islemYapan = islemYapanId,
                KullaniciId = kullaniciId,
                Tarih=DateTime.Now,
            };

            KullaniciHareketleriDAL.InsertorUpdate(context, model);
            KullaniciHareketleriDAL.Save(context);

        }
        public ActionResult Index()
        {
            var model = kullanicilarDAL.GetAll(context);
            return View(model);
        }

        public ActionResult index2()
        {
            var kullanicilar = kullanicilarDAL.GetAll(context, tbl: "KullaniciRolleri");
            var roller = rollerDAL.GetAll(context);
            return View(new KullaniciRolViewModel { kullanicilar = kullanicilar, Roller = roller });
        }
        public ActionResult KRolleri(int id)
        {
            var model = KullaniciRolleriDAL.GetAll(context, x => x.KullaniciId == id, "Roller");
            if (model!=null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        public ActionResult creat() 
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult creat(Kullanicilar entity)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }
            kullanicilarDAL.InsertorUpdate(context, entity);
            kullanicilarDAL.Save(context);

            int kullaniciId = context.Kullanicilar.Max(x => x.Id);
            var userName=User.Identity.Name;
            var model = kullanicilarDAL.GetByFilter(context, x => x.EMail == userName);
            var islemYapanId = model.Id;
            string aciklama = model.KullaniciAdi + "Kullanıcısı Yeni Bir Kullanıcı Ekledi";
            KullaniciHAreketleri(kullaniciId,islemYapanId,aciklama);

            return RedirectToAction("index2");
        }

        public ActionResult update(int? id) 
         {
            if (id==null)
            {
                return HttpNotFound("İd Değeri Girilmedi");
            }
            var model = kullanicilarDAL.GetById(context, id);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult update(Kullanicilar entity)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }
            kullanicilarDAL.InsertorUpdate(context, entity);
            kullanicilarDAL.Save(context);
            return RedirectToAction("index2");
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public ActionResult Login(Kullanicilar entity)
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            var model = kullanicilarDAL.GetByFilter(context, x => x.EMail == entity.EMail && x.Sifre == entity.Sifre);
            if (model != null)
            {
                FormsAuthentication.SetAuthCookie(entity.EMail, false);

                int islemYapanId = model.Id;
                string aciklama = model.KullaniciAdi + " Kullanıcısı Giriş Yaptı";
                KullaniciHAreketleri(islemYapanId, islemYapanId, aciklama);

                return RedirectToAction("Index2", "KitapTurleri");
            }
            ViewBag.error = "Kullanıcı Adı veya Şifre Yanlış";
            return View();
        }
        [AllowAnonymous]

        public ActionResult KayitOl()
        {
            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult KayitOl(Kullanicilar entity, string sifreTekrar, bool kabul)
        {
            if (!ModelState.IsValid)//model doğrulanmazsa
            {
                return View(entity);
            }
            else//model doğrulanırsa
            {
                if (entity.Sifre != sifreTekrar)//şifreler uyuşmazsa
                {
                    ViewBag.sifreError = "Şifreler uyuşmuyor";
                    return View();
                }
                else//şifreler uyuşursa
                {
                    if (!kabul)//Şartlar Kabul Edilmemişse
                    {
                        ViewBag.kabulError = "Lütfen Şartları Kabul Ettiğinizi Onaylayın";
                        return View();
                    }
                    else //şartlar kabul edilmişse
                    {
                        entity.KayitTarihi = DateTime.Now;
                        kullanicilarDAL.InsertorUpdate(context, entity);
                        kullanicilarDAL.Save(context);

                        int kullaniciId = context.Kullanicilar.Max(x => x.Id);
                        
                        string aciklama ="Yeni Bir Kullanıcı Oluşturuldu";
                        KullaniciHAreketleri(kullaniciId, kullaniciId, aciklama);

                        return RedirectToAction("Login");
                    }
                }
            }
        }
        [AllowAnonymous]

        public ActionResult SifremiUnuttum()
        {
            return View();
        }

        [ValidateAntiForgeryToken, HttpPost]
        [AllowAnonymous]
        public ActionResult SifremiUnuttum(Kullanicilar entity)
        {
            var model = kullanicilarDAL.GetByFilter(context, x => x.EMail == entity.EMail);
            if (model != null)
            {
                Guid rastgele = Guid.NewGuid();
                model.Sifre = rastgele.ToString().Substring(0, 8);
                kullanicilarDAL.Save(context);

                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mamiakyol3547@hotmail.com", "Şifre sıfırlama");
                mail.To.Add(model.EMail);
                mail.IsBodyHtml = true;
                mail.Subject = "Şifre Değiştirme İsteği";
                mail.Body += "Merhaba " + model.AdiSoyadi + "<br/> Kullanıcı Adınız=" + model.KullaniciAdi + "<br/> Şifreniz=" + model.Sifre;

                // SMTP kimlik doğrulaması için kullanıcı adı ve parola
                NetworkCredential net = new NetworkCredential("mamiakyol3547@hotmail.com", "220376Ma.");
                client.Credentials = net;

                try
                {
                    client.Send(mail);
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ViewBag.hata = "E-posta gönderilirken bir hata oluştu: " + ex.Message;
                    return View();
                }
            }
            else if (model == null && entity.EMail != null)
            {
                ViewBag.hata = "Böyle bir e-mail adresi bulunamadı.";
                return View();
            }

            return View();
        }
        public ActionResult Ekle()
        {
            ViewBag.liste = new SelectList(context.Kullanicilar, "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Kullanicilar entity)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.liste = new SelectList(context.Kullanicilar, "Id");
                return View();
            }
            kullanicilarDAL.InsertorUpdate(context,entity);
            kullanicilarDAL.Save(context);

            int kullaniciId = context.Kullanicilar.Max(x => x.Id);
            var userName = User.Identity.Name;
            var model = kullanicilarDAL.GetByFilter(context, x => x.EMail == userName);
            var islemYapanId = model.Id;
            string aciklama = model.KullaniciAdi + "Kullanıcısı Yeni Bir Kullanıcı Ekledi";
            KullaniciHAreketleri(kullaniciId, islemYapanId, aciklama);

            return RedirectToAction("index");
        }
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ViewBag.liste = new SelectList(context.Kullanicilar, "Id");
            var model = kullanicilarDAL.GetByFilter(context, x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kullanicilar entity)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.liste = new SelectList(context.Kullanicilar, "Id");
                return View();
            }
            kullanicilarDAL.InsertorUpdate(context, entity);
            kullanicilarDAL.Save(context);
            return RedirectToAction("index");
        }
      
            public ActionResult Detay(int id)
            {
                var kullaniciHareketleri = context.KullaniciHareketleri.FirstOrDefault(x => x.KullaniciId == id);
                if (kullaniciHareketleri == null)
                {
                    return HttpNotFound();
                }
                return View(kullaniciHareketleri);
            }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            kullanicilarDAL.Delete(context, x => x.Id == id);
            kullanicilarDAL.Save(context);
            return RedirectToAction("index2");
        }
    }

    }
