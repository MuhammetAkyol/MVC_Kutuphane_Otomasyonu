using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Mapping;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    public class DuyurularController : Controller
    {
        // GET: Duyurular
        KutuphaneContext context = new KutuphaneContext();
        DuyurularDAL duyurularDal = new DuyurularDAL();

        public ActionResult Index()
        {
            // Üçüncü parametreyi null yerine boş bir dizi olarak gönderiyoruz.
            var model = duyurularDal.GetAll(context, null, new string[] { });

            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Duyurular duyurular)
        {
            if (ModelState.IsValid)
            {
                duyurularDal.InsertorUpdate(context, duyurular);
                duyurularDal.Save(context);
                return RedirectToAction("Index");
            }
            return View(duyurular);
        }


        [HttpPost]
        public JsonResult CreatePopup(Duyurular duyurular)
        {
            if (ModelState.IsValid)
            {
                using (var context = new KutuphaneContext())
                {
                    context.Duyurular.Add(duyurular);
                    context.SaveChanges();
                }
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invalid data" });
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var model = duyurularDal.GetById(context, id);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Duyurular duyurular)
        {
            if (ModelState.IsValid)
            {
                duyurularDal.InsertorUpdate(context, duyurular);
                duyurularDal.Save(context);
                return RedirectToAction("Index");
            }
            return View(duyurular);
        }
        public ActionResult Delete(int? id)
        {
            duyurularDal.Delete(context, x => x.Id == id);
            duyurularDal.Save(context);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult SeciliDuyuruSil(List<int> selectedIds)
        {
            if (selectedIds != null)
            {
                foreach (int id in selectedIds)
                {
                    duyurularDal.Delete(context, x => x.Id == id);
                    duyurularDal.Save(context);
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }









    }
}