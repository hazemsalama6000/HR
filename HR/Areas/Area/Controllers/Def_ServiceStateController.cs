using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_ServiceStateController : Controller
    {
        // GET: Area/Def_ServiceState

        HREntities db = unit.getEntity();

        public ActionResult DeleteDef_ServiceState(long? ID) {
            try
            {
                var ob=db.Def_ServiceState.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_ServiceState.Remove(ob);
                db.SaveChanges();
                var Def_ServiceStateList = JsonConvert.SerializeObject(db.Def_ServiceState.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                return Json(new {action="yes",msg="Deleted", Def_ServiceStateList = Def_ServiceStateList },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            ViewBag.Def_ServiceStateViewBag = JsonConvert.SerializeObject( db.Def_ServiceState.Select(g=> new {ID=g.ID,Name=g.Name}).ToList() );
            return View();
        }


        public ActionResult AddDef_ServiceState()
        {
            return View();
        }

        public ActionResult AddDef_ServiceStateEnd(Def_ServiceState Def_ServiceState) {

            if (ModelState.IsValid) {
                try
                {
                    db.Def_ServiceState.Add(Def_ServiceState);
                    db.SaveChanges();
                    var Def_ServiceStateList=JsonConvert.SerializeObject(db.Def_ServiceState.Select(g=> new {ID=g.ID,Name=g.Name}).ToList());
                    return Json(new {action="yes",msg="Added", Def_ServiceStateList = Def_ServiceStateList} ,JsonRequestBehavior.AllowGet);
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


        public ActionResult EditDef_ServiceState(long? ID)
        {
            ViewBag.Def_ServiceStateViewBag = JsonConvert.SerializeObject(db.Def_ServiceState.Where(a => a.ID == ID).Select(g=> new { ID=g.ID,Name=g.Name }).FirstOrDefault());
            return View();
        }

        public ActionResult EditDef_ServiceStateEnd(Def_ServiceState Def_ServiceState)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob= db.Def_ServiceState.Where(a => a.ID == Def_ServiceState.ID).FirstOrDefault();
                    ob.Name = Def_ServiceState.Name;
                    db.SaveChanges();

                    var Def_ServiceStateList = JsonConvert.SerializeObject(db.Def_ServiceState.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                    return Json(new { action = "yes", msg = "Editted", Def_ServiceStateList = Def_ServiceStateList }, JsonRequestBehavior.AllowGet);
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