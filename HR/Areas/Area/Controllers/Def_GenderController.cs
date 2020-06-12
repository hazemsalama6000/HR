using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_GenderController : Controller
    {
        // GET: Area/Def_Gender

        HREntities db = unit.getEntity();

        public ActionResult DeleteDef_Gender(long? ID) {
            try
            {
                var ob=db.Def_Gender.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_Gender.Remove(ob);
                db.SaveChanges();
                var Def_GenderList = JsonConvert.SerializeObject(db.Def_Gender.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                return Json(new {action="yes",msg="Deleted", Def_GenderList = Def_GenderList },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            ViewBag.Def_GenderViewBag = JsonConvert.SerializeObject( db.Def_Gender.Select(g=> new {ID=g.ID,Name=g.Name}).ToList() );
            return View();
        }


        public ActionResult AddDef_Gender()
        {
            return View();
        }

        public ActionResult AddDef_GenderEnd(Def_Gender Def_Gender) {

            if (ModelState.IsValid) {
                try
                {
                    db.Def_Gender.Add(Def_Gender);
                    db.SaveChanges();
                    var Def_GenderList=JsonConvert.SerializeObject(db.Def_Gender.Select(g=> new {ID=g.ID,Name=g.Name}).ToList());
                    return Json(new {action="yes",msg="Added", Def_GenderList = Def_GenderList} ,JsonRequestBehavior.AllowGet);
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


        public ActionResult EditDef_Gender(long? ID)
        {
            ViewBag.Def_GenderViewBag = JsonConvert.SerializeObject(db.Def_Gender.Where(a => a.ID == ID).Select(g=> new { ID=g.ID,Name=g.Name }).FirstOrDefault());
            return View();
        }

        public ActionResult EditDef_GenderEnd(Def_Gender Def_Gender)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob= db.Def_Gender.Where(a => a.ID == Def_Gender.ID).FirstOrDefault();
                    ob.Name = Def_Gender.Name;
                    db.SaveChanges();

                    var Def_GenderList = JsonConvert.SerializeObject(db.Def_Gender.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                    return Json(new { action = "yes", msg = "Editted", Def_GenderList = Def_GenderList }, JsonRequestBehavior.AllowGet);
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