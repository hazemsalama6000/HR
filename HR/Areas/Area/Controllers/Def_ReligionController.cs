using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_ReligionController : Controller
    {
        // GET: Area/Def_Religion

        HREntities db = unit.getEntity();

        public ActionResult DeleteDef_Religion(long? ID) {
            try
            {
                var ob=db.Def_Religion.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_Religion.Remove(ob);
                db.SaveChanges();
                var Def_ReligionList = JsonConvert.SerializeObject(db.Def_Religion.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                return Json(new {action="yes",msg="Deleted", Def_ReligionList = Def_ReligionList },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            ViewBag.Def_ReligionViewBag = JsonConvert.SerializeObject( db.Def_Religion.Select(g=> new {ID=g.ID,Name=g.Name}).ToList() );
            return View();
        }


        public ActionResult AddDef_Religion()
        {
            return View();
        }

        public ActionResult AddDef_ReligionEnd(Def_Religion Def_Religion) {

            if (ModelState.IsValid) {
                try
                {
                    db.Def_Religion.Add(Def_Religion);
                    db.SaveChanges();
                    var Def_ReligionList=JsonConvert.SerializeObject(db.Def_Religion.Select(g=> new {ID=g.ID,Name=g.Name}).ToList());
                    return Json(new {action="yes",msg="Added", Def_ReligionList = Def_ReligionList} ,JsonRequestBehavior.AllowGet);
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


        public ActionResult EditDef_Religion(long? ID)
        {
            ViewBag.Def_ReligionViewBag = JsonConvert.SerializeObject(db.Def_Religion.Where(a => a.ID == ID).Select(g=> new { ID=g.ID,Name=g.Name }).FirstOrDefault());
            return View();
        }

        public ActionResult EditDef_ReligionEnd(Def_Religion Def_Religion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob= db.Def_Religion.Where(a => a.ID == Def_Religion.ID).FirstOrDefault();
                    ob.Name = Def_Religion.Name;
                    db.SaveChanges();

                    var Def_ReligionList = JsonConvert.SerializeObject(db.Def_Religion.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                    return Json(new { action = "yes", msg = "Editted", Def_ReligionList = Def_ReligionList }, JsonRequestBehavior.AllowGet);
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