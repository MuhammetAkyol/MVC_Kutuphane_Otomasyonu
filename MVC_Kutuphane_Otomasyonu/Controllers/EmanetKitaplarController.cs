using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    //[AllowAnonymous]
    public class EmanetKitaplarController : Controller
    {
        // GET: EmanetKitaplar
        KutuphaneContext context = new KutuphaneContext();
        EmanetKitaplarDAL EmanetKitaplarDAL = new EmanetKitaplarDAL();
        KitaplarDAL kitaplarDAL=new KitaplarDAL();
        KitapHareketleriDAL kitapHareketleriDAL = new KitapHareketleriDAL();


        public ActionResult Index()
            {
                var model = EmanetKitaplarDAL.GetAll(context, x => x.KitapIadeTarihi == null, "Kitaplar", "Uyeler");
                return View(model);
            }

        public ActionResult EmanetKitapVer()
        {
            ViewBag.Uyeliste = new SelectList(context.Uyeler, "Id", "AdiSoyadi");
            ViewBag.Kitapliste = new SelectList(context.Kitaplar, "Id", "KitapAdi");

            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult EmanetKitapVer(EmanetKitaplar entity)
        {
            if (ModelState.IsValid)
            {
                var email = User.Identity.Name;
                var modelkullanici = context.Kullanicilar.FirstOrDefault(k => k.EMail == email);
                EmanetKitaplarDAL.InsertorUpdate(context, entity);

                var kitapHareket = new KitapHareketleri
                {
                    KullaniciId = modelkullanici.Id,
                    KıtapId=entity.KitapId,
                    UyeId=entity.UyeId,
                    YapilanIslem= modelkullanici.KullaniciAdi + " Kullanıcısı Emanet Kitap İşlemi Yaptı",
                    Tarih=DateTime.Now,
                };
                kitapHareketleriDAL.InsertorUpdate(context, kitapHareket); 
                EmanetKitaplarDAL.Save(context);                
                return RedirectToAction("Index");
            }
            ViewBag.Uyeliste = new SelectList(context.Uyeler, "Id", "AdiSoyadi");
            ViewBag.Kitapliste = new SelectList(context.Kitaplar, "Id", "KitapAdi");
            return RedirectToAction("Index");
        }
        
        public ActionResult Duzenle(int? id)
        {
            if (id==null)
            {
                return HttpNotFound("İd Değeri Girilmedi");
            }
            ViewBag.Uyeliste = new SelectList(context.Uyeler, "Id", "AdiSoyadi");
            ViewBag.Kitapliste = new SelectList(context.Kitaplar, "Id", "KitapAdi");
            var model = EmanetKitaplarDAL.GetByFilter(context, x => x.Id == id, "Uyeler", "Kitaplar");
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Duzenle(EmanetKitaplar entity)
        {
            if (ModelState.IsValid)
            {               
                EmanetKitaplarDAL.InsertorUpdate(context, entity);
                EmanetKitaplarDAL.Save(context);
                
                return RedirectToAction("index");
            }
            ViewBag.Uyeliste = new SelectList(context.Uyeler, "Id", "AdiSoyadi");
            ViewBag.Kitapliste = new SelectList(context.Kitaplar, "Id", "KitapAdi");
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("İd Değeri Girilmedi");
            }

            EmanetKitaplarDAL.Delete(context, x => x.Id == id);
            EmanetKitaplarDAL.Save(context);
            return RedirectToAction("Index");

        }

        public ActionResult TeslimAl(int? id)
        {
            var model = EmanetKitaplarDAL.GetByFilter(context, x => x.Id == id);
            model.KitapIadeTarihi = DateTime.Now;

            var kitaplar = kitaplarDAL.GetByFilter(context, x => x.Id == model.KitapId);
            kitaplar.StokAdedi = kitaplar.StokAdedi + model.KitapSayisi;
            EmanetKitaplarDAL.Save(context);
            return RedirectToAction("Index"); 
        }

    }
}