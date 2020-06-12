using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_MilitaryServiceController : Controller
    {
        // GET: Area/Def_MilitaryService

        HREntities db = unit.getEntity();

        public ActionResult DeleteDef_MilitaryService(long? ID) {
            try
            {
                var ob=db.Def_MilitaryService.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_MilitaryService.Remove(ob);
                db.SaveChanges();
                var Def_MilitaryServiceList = JsonConvert.SerializeObject(db.Def_MilitaryService.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                return Json(new {action="yes",msg="Deleted", Def_MilitaryServiceList = Def_MilitaryServiceList },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            ViewBag.Def_MilitaryServiceViewBag = JsonConvert.SerializeObject( db.Def_MilitaryService.Select(g=> new {ID=g.ID,Name=g.Name}).ToList() );
            return View();
        }


        public ActionResult AddDef_MilitaryService()
        {
            return View();
        }

        public ActionResult AddDef_MilitaryServiceEnd(Def_MilitaryService Def_MilitaryService) {

            if (ModelState.IsValid) {
                try
                {
                    db.Def_MilitaryService.Add(Def_MilitaryService);
                    db.SaveChanges();
                    var Def_MilitaryServiceList=JsonConvert.SerializeObject(db.Def_MilitaryService.Select(g=> new {ID=g.ID,Name=g.Name}).ToList());
                    return Json(new {action="yes",msg="Added", Def_MilitaryServiceList = Def_MilitaryServiceList} ,JsonRequestBehavior.AllowGet);
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


        public ActionResult EditDef_MilitaryService(long? ID)
        {
            ViewBag.Def_MilitaryServiceViewBag = JsonConvert.SerializeObject(db.Def_MilitaryService.Where(a => a.ID == ID).Select(g=> new { ID=g.ID,Name=g.Name }).FirstOrDefault());
            return View();
        }

        public ActionResult EditDef_MilitaryServiceEnd(Def_MilitaryService Def_MilitaryService)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob= db.Def_MilitaryService.Where(a => a.ID == Def_MilitaryService.ID).FirstOrDefault();
                    ob.Name = Def_MilitaryService.Name;
                    db.SaveChanges();

                    var Def_MilitaryServiceList = JsonConvert.SerializeObject(db.Def_MilitaryService.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                    return Json(new { action = "yes", msg = "Editted", Def_MilitaryServiceList = Def_MilitaryServiceList }, JsonRequestBehavior.AllowGet);
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