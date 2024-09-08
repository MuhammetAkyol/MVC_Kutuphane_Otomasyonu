using MVC_Kutuphane_Otomasyonu.Entities.DAL;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using MVC_Kutuphane_Otomasyonu.Entities.Model.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Kutuphane_Otomasyonu.Controllers
{
    [AllowAnonymous]
    public class KitapTurleriController : Controller
    {
        // GET: KitapTurleri
        KutuphaneContext context = new KutuphaneContext();
        KitapTurleriDAL kitapTurleriDAL = new KitapTurleriDAL();


        public ActionResult Index(int? page)
        {

            int pageSize = 10;  // Number of items per page
            int pageNumber = (page ?? 1);  // If no page is specified, default to the first page

            var kitapTurleriList = kitapTurleriDAL.GetAll(context, null, null);

            IPagedList<KitapTurleri> pagedKitapTurleriList = kitapTurleriList.ToPagedList(pageNumber, pageSize);

            return View(pagedKitapTurleriList);


            //var kitapTurleriList = kitapTurleriDAL.GetAll(context, null, null);
            //return View(kitapTurleriList);
        }

        public ActionResult Index2(string ara,int? page)
        {
            var model = kitapTurleriDAL.GetAll(context).ToPagedList(page ?? 1,3);
            if (ara!=null)
            {
                model = kitapTurleriDAL.GetAll(context,x=>x.KitapTuru.Contains(ara)).ToPagedList(page?? 1,3);
            }
            return View("Index",model);
        }
        public ActionResult Ekle() 
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Ekle(KitapTurleri entity) 
        {
            if (ModelState.IsValid)
            {
                kitapTurleriDAL.InsertorUpdate(context, entity);
                kitapTurleriDAL.Save(context);
                return RedirectToAction("Index");
            }
            return View("Index");
        }



        public ActionResult Duzenle(int? id)
        {
            if (id==null) 
            {
                return HttpNotFound();
            }
            var model = kitapTurleriDAL.GetById(context, id);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(KitapTurleri kitapturleri)
        {
            if (ModelState.IsValid)
            {
                kitapTurleriDAL.InsertorUpdate(context, kitapturleri);
                kitapTurleriDAL.Save(context);
                return RedirectToAction("Index");
            }
            return View(kitapturleri);
        }
        public ActionResult Sil(int? id)
        {
            kitapTurleriDAL.Delete(context,x=>x.Id==id);
            kitapTurleriDAL.Save(context);
            return RedirectToAction("Index");
        }

    }
}