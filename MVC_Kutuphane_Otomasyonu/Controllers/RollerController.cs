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
    [Authorize(Roles = "Admin")]
    [AllowAnonymous]

    public class RollerController : Controller
    {
        // GET: Roller
        KutuphaneContext context = new KutuphaneContext();
        RollerDAL rollerDal=new RollerDAL();
        public ActionResult Index()
        {
            var model = rollerDal.GetAll(context);
            return View(model);
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Ekle(Roller entity)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }
            rollerDal.InsertorUpdate(context, entity);
            rollerDal.Save(context);
            return RedirectToAction("Index");
        }
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Id Değeri Girilmedi");
            }
            var model = rollerDal.GetByFilter(context, x => x.Id == id);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Roller entity)
        {
            if (!ModelState.IsValid)
            {
                return View(entity);
            }
            rollerDal.InsertorUpdate(context, entity);
            rollerDal.Save(context);
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Id Değeri Girilmedi");
            }
            rollerDal.Delete(context, x => x.Id == id);
            rollerDal.Save(context);
            return RedirectToAction("Index");
        }
    }
}