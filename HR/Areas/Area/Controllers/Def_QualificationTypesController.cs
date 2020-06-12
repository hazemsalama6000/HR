using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_QualificationTypesController : Controller
    {
        // GET: Area/Def_QualificationTypes

        HREntities db = unit.getEntity();

        public ActionResult Index()
        {
            ViewBag.QualificationTypesViewBag =JsonConvert.SerializeObject( db.Def_QualificationTypes.Select(a=> new { ID=a.ID,Name=a.Name}).ToList() ) ;
            return View();
        }


        public ActionResult DeleteQualificationTypes(long ? ID)
        {
            try
            {
               var ob = db.Def_QualificationTypes.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_QualificationTypes.Remove(ob);
                db.SaveChanges();
                var l = JsonConvert.SerializeObject(db.Def_QualificationTypes.Select(a => new { ID = a.ID, Name = a.Name }).ToList());
                return Json(new { action = "yes", msg = "Deleted", QualificationTypesList = l }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddQualificationTypes() {
            return View();
        }

        public ActionResult AddQualificationTypesEnd(Def_QualificationTypes Def_QualificationTypes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Def_QualificationTypes.Add(Def_QualificationTypes);
                    db.SaveChanges();
                    var l= JsonConvert.SerializeObject(db.Def_QualificationTypes.Select(a => new { ID = a.ID, Name = a.Name }).ToList());
                    return Json(new {action="yes",msg="Added" , QualificationTypesList = l},JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult EditQualificationTypes(long? ID)
        {
            ViewBag.QualificationTypeViewBag = JsonConvert.SerializeObject( db.Def_QualificationTypes.Where(g=>g.ID==ID).Select(a=> new {ID=a.ID,Name=a.Name }).FirstOrDefault() );
            return View();
        }

        public ActionResult EditQualificationTypesEnd(Def_QualificationTypes Def_QualificationTypes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob = db.Def_QualificationTypes.Where(a => a.ID == Def_QualificationTypes.ID).FirstOrDefault();
                    ob.Name = Def_QualificationTypes.Name;
                    db.SaveChanges();
                    var l = JsonConvert.SerializeObject(db.Def_QualificationTypes.Select(a => new { ID = a.ID, Name = a.Name }).ToList());
                    return Json(new { action = "yes", msg = "Editted", QualificationTypesList = l }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}