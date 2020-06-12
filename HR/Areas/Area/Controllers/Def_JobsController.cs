using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_JobsController : Controller
    {
        // GET: Area/Def_Jobs
        HREntities db = unit.getEntity();

        public ActionResult DeleteDef_Jobs(long? ID) {
            try
            {
                var ob=db.Def_Jobs.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_Jobs.Remove(ob);
                db.SaveChanges();
                var Def_JobsList = JsonConvert.SerializeObject( db.Def_Jobs.Select(g=> new {ID=g.ID,Name=g.Name }).ToList());
                return Json(new {action="yes",msg="Done", Def_JobsList = Def_JobsList },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {
            ViewBag.Def_JobsViewbag = JsonConvert.SerializeObject( db.Def_Jobs.Select(a => new { ID = a.ID, Name = a.Name }).ToList());
            return View();
        }

        public ActionResult AddDef_Jobs()
        {
            return View();
        }

        public ActionResult AddDef_JobsEnd(Def_Jobs Def_Jobs) {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Def_Jobs.Add(Def_Jobs);
                    db.SaveChanges();
                    var Def_JobsList = JsonConvert.SerializeObject(db.Def_Jobs.Select(g => new { ID = g.ID, Name = g.Name }).ToList());

                    return Json(new { action="yes",msg="saved" , Def_JobsList = Def_JobsList },JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex) {
                    return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EditDef_Jobs(long ? ID) {
            ViewBag.Def_JobsViewBag=JsonConvert.SerializeObject( db.Def_Jobs.Where(a => a.ID == ID).Select(g=> new { ID=g.ID,Name=g.Name}).FirstOrDefault());
            return View();
        }
        [HttpPost]
        public ActionResult EditDef_JobsEnd(Def_Jobs Def_Jobs)
        {
            if (ModelState.IsValid) {
                try
                {
                    var ob = db.Def_Jobs.Where(a => a.ID == Def_Jobs.ID).FirstOrDefault();
                    ob.Name = Def_Jobs.Name;
                    db.SaveChanges();
                  var Def_JobsList = JsonConvert.SerializeObject( db.Def_Jobs.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                    return Json(new {action="yes",msg="Editted", Def_JobsList= Def_JobsList },JsonRequestBehavior.AllowGet);
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






