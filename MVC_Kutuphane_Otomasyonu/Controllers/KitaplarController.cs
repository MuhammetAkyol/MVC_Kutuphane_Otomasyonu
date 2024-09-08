using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles ="Admin")]
    public class KitaplarController : Controller
    {
        // GET: Kitaplar
        KutuphaneContext context=new KutuphaneContext();
        KitaplarDAL KitaplarDAL = new KitaplarDAL();
        KitapKayitHareketleriDAL kitapKayitHareketleriDAL=new KitapKayitHareketleriDAL();
        KullanicilarDAL KullanicilarDAL = new KullanicilarDAL();

        public void KitapKayitHareketleri(int kullaniciId, int kitapId, string yapilanIslem, string aciklama)
        {
            var model = new KitapKayitHareketleri
            {
                Aciklama = aciklama,
                KullaniciId = kullaniciId,
                KitapId = kitapId,
                YapilanIslem = yapilanIslem,
                Tarih=DateTime.Now,
            };

            kitapKayitHareketleriDAL.InsertorUpdate(context, model);
            kitapKayitHareketleriDAL.Save(context);

        }

        public ActionResult Index()
        {
            var model = KitaplarDAL.GetAll(context, null, "KitapTurleri");
            return View(model);
        }
        public ActionResult Ekle()
        {
            ViewBag.liste = new SelectList(context.KitapTurleri, "Id", "KitapTuru");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Kitaplar entity)
        {
            if (ModelState.IsValid)
            {
                KitaplarDAL.InsertorUpdate(context, entity);
                KitaplarDAL.Save(context);

                int kitapId = context.Kitaplar.Max(x => x.Id);
                var userName = User.Identity.Name;
                var modelKullanici = KullanicilarDAL.GetByFilter(context, x => x.EMail == userName);
                int kullaniciId = modelKullanici.Id;
                KitapKayitHareketleri(kullaniciId, kitapId, modelKullanici.KullaniciAdi + " Kullanıcı Yeni Bir Kitap EKledi.", "Kitap Ekleme İşlemi");

                return RedirectToAction("index");
            }
            ViewBag.liste = new SelectList(context.KitapTurleri, "Id", "KitapTuru");
            return View();
        }
       
        public ActionResult Duzenle(int? id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }
            ViewBag.liste = new SelectList(context.KitapTurleri, "Id", "KitapTuru");
            var model = KitaplarDAL.GetByFilter(context, x => x.Id == id, "KitapTurleri");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kitaplar entity)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.liste = new SelectList(context.KitapTurleri, "Id", "KitapTuru");
                return View();
            }
            KitaplarDAL.InsertorUpdate(context, entity);
            KitaplarDAL.Save(context);

            int kitapId = entity.Id;
            var userName = User.Identity.Name;
            var modelKullanici = KullanicilarDAL.GetByFilter(context, x => x.EMail == userName);
            int kullaniciId = modelKullanici.Id;
            KitapKayitHareketleri(kullaniciId, kitapId, modelKullanici.KullaniciAdi + " Kullanıcı Kitap Üzerinde Değişiklik Gerçekleştirdi.", "Kitap Düzenleme İşlemi");

            return RedirectToAction("index");
        }
        public ActionResult Detay(int? id)
        {
            var model = KitaplarDAL.GetByFilter(context, x => x.Id == id, "KitapTurleri");
            return View(model);
        }

        public ActionResult Sil(int? id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }
            KitaplarDAL.Delete(context, x => x.Id == id);
            KitaplarDAL.Save(context);
            return RedirectToAction("Index");
        }
    }
}