using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_NationalityController : Controller
    {
        // GET: Area/Def_Nationality

        HREntities db = unit.getEntity();

        public ActionResult DeleteDef_Nationality(long? ID) {
            try
            {
                var ob=db.Def_Nationality.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_Nationality.Remove(ob);
                db.SaveChanges();
                var Def_NationalityList = JsonConvert.SerializeObject(db.Def_Nationality.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                return Json(new {action="yes",msg="Deleted", Def_NationalityList = Def_NationalityList },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            ViewBag.Def_NationalityViewBag = JsonConvert.SerializeObject( db.Def_Nationality.Select(g=> new {ID=g.ID,Name=g.Name}).ToList() );
            return View();
        }


        public ActionResult AddDef_Nationality()
        {
            return View();
        }

        public ActionResult AddDef_NationalityEnd(Def_Nationality Def_Nationality) {

            if (ModelState.IsValid) {
                try
                {
                    db.Def_Nationality.Add(Def_Nationality);
                    db.SaveChanges();
                    var Def_NationalityList=JsonConvert.SerializeObject(db.Def_Nationality.Select(g=> new {ID=g.ID,Name=g.Name}).ToList());
                    return Json(new {action="yes",msg="Added", Def_NationalityList = Def_NationalityList} ,JsonRequestBehavior.AllowGet);
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


        public ActionResult EditDef_Nationality(long? ID)
        {
            ViewBag.Def_NationalityViewBag = JsonConvert.SerializeObject(db.Def_Nationality.Where(a => a.ID == ID).Select(g=> new { ID=g.ID,Name=g.Name }).FirstOrDefault());
            return View();
        }

        public ActionResult EditDef_NationalityEnd(Def_Nationality Def_Nationality)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob= db.Def_Nationality.Where(a => a.ID == Def_Nationality.ID).FirstOrDefault();
                    ob.Name = Def_Nationality.Name;
                    db.SaveChanges();

                    var Def_NationalityList = JsonConvert.SerializeObject(db.Def_Nationality.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                    return Json(new { action = "yes", msg = "Editted", Def_NationalityList = Def_NationalityList }, JsonRequestBehavior.AllowGet);
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