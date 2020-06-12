using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_MaritalStatusController : Controller
    {
        // GET: Area/Def_MaritalStatus

        HREntities db = unit.getEntity();

        public ActionResult DeleteDef_MaritalStatus(long? ID) {
            try
            {
                var ob=db.Def_MaritalStatus.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_MaritalStatus.Remove(ob);
                db.SaveChanges();
                var Def_MaritalStatusList = JsonConvert.SerializeObject(db.Def_MaritalStatus.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                return Json(new {action="yes",msg="Deleted", Def_MaritalStatusList = Def_MaritalStatusList },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            ViewBag.Def_MaritalStatusViewBag = JsonConvert.SerializeObject( db.Def_MaritalStatus.Select(g=> new {ID=g.ID,Name=g.Name}).ToList() );
            return View();
        }


        public ActionResult AddDef_MaritalStatus()
        {
            return View();
        }

        public ActionResult AddDef_MaritalStatusEnd(Def_MaritalStatus Def_MaritalStatus) {

            if (ModelState.IsValid) {
                try
                {
                    db.Def_MaritalStatus.Add(Def_MaritalStatus);
                    db.SaveChanges();
                    var Def_MaritalStatusList=JsonConvert.SerializeObject(db.Def_MaritalStatus.Select(g=> new {ID=g.ID,Name=g.Name}).ToList());
                    return Json(new {action="yes",msg="Added", Def_MaritalStatusList = Def_MaritalStatusList} ,JsonRequestBehavior.AllowGet);
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


        public ActionResult EditDef_MaritalStatus(long? ID)
        {
            ViewBag.Def_MaritalStatusViewBag = JsonConvert.SerializeObject(db.Def_MaritalStatus.Where(a => a.ID == ID).Select(g=> new { ID=g.ID,Name=g.Name }).FirstOrDefault());
            return View();
        }

        public ActionResult EditDef_MaritalStatusEnd(Def_MaritalStatus Def_MaritalStatus)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob= db.Def_MaritalStatus.Where(a => a.ID == Def_MaritalStatus.ID).FirstOrDefault();
                    ob.Name = Def_MaritalStatus.Name;
                    db.SaveChanges();

                    var Def_MaritalStatusList = JsonConvert.SerializeObject(db.Def_MaritalStatus.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                    return Json(new { action = "yes", msg = "Editted", Def_MaritalStatusList = Def_MaritalStatusList }, JsonRequestBehavior.AllowGet);
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