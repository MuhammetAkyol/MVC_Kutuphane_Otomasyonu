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
    public class KullaniciRolleriController : Controller
    {
        // GET: KullaniciRolleri
        KutuphaneContext context=new KutuphaneContext();
        KullaniciRolleriDAL KullaniciRolleriDAL= new KullaniciRolleriDAL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ekle(int? id)
        {
            if (id==null)
            {
                return HttpNotFound("kullanıcıid değeri girilmedi");
            }
            var model = KullaniciRolleriDAL.GetByFilter(context, x => x.KullaniciId == id, "Kullanicilar");
            ViewBag.KullaniciId = id;
            ViewBag.kullaniciAdi = model.Kullanicilar.KullaniciAdi;
            ViewBag.liste = new SelectList(context.Roller, "Id", "Rol");

            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(KullaniciRolleri entity)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.liste = new SelectList(context.Roller, "Id", "Rol");
                var model = KullaniciRolleriDAL.GetByFilter(context, x => x.KullaniciId == entity.KullaniciId, "Kullanicilar");
                ViewBag.KullaniciId = entity.Id;
                ViewBag.kullaniciAdi = model.Kullanicilar.KullaniciAdi;                
                return View(entity);
            }
            entity.Id = 0;
            KullaniciRolleriDAL.InsertorUpdate(context, entity);
            KullaniciRolleriDAL.Save(context);
            return RedirectToAction("index2", "Kullanicilar");
        }
        public ActionResult Duzenle(int? id)
        {
            if (id==null)
            {
                return HttpNotFound("kullanıcıid değeri girilmedi");
            }
            var model = KullaniciRolleriDAL.GetByFilter(context, x => x.Id == id, "Kullanicilar");
            ViewBag.kullaniciAdi = model.Kullanicilar.KullaniciAdi;
            ViewBag.liste = new SelectList(context.Roller, "Id", "Rol");

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(KullaniciRolleri entity)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.liste = new SelectList(context.Roller, "Id", "Rol");
                var model = KullaniciRolleriDAL.GetByFilter(context, x => x.Id == entity.Id, "Kullanicilar");
                ViewBag.kullaniciAdi = model.Kullanicilar.KullaniciAdi;                
                return View(entity);
            }
            KullaniciRolleriDAL.InsertorUpdate(context, entity);
            KullaniciRolleriDAL.Save(context);
            return RedirectToAction("index2", "Kullanicilar");
        }

        public ActionResult Sil(int? id)
        {
            KullaniciRolleriDAL.Delete(context, x => x.Id == id);
            KullaniciRolleriDAL.Save(context);
            return RedirectToAction("index2", "Kullanicilar");
        }
    }
}