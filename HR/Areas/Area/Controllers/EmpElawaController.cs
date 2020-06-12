using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpElawaController : Controller
    {
        // GET: Area/EmpElawa
        HREntities db = unit.getEntity();

        public ActionResult getElawaTypeData(int ID)
        {
            var ob = db.Def_ElawaTypes.Where(a => a.ID == ID).FirstOrDefault();
            return Json(new { Percent=ob.SalaryPercent , value=ob.Value }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEmpElawa(int ID)
        {
            try
            {
                var ob = db.EmpElawa.Where(a => a.ID == ID).FirstOrDefault();
                db.EmpElawa.Remove(ob);
                db.SaveChanges();
                return Json( new {action="yes",msg="Deleted" },JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new {action="no" ,msg="error occurred" },JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getEmpElawa(int ID)
        {
            var ob = db.EmpElawa.Where(a=>a.EmpID==ID).Select(g => new { ID = g.ID , DateAdded = g.DateAdded, Name = g.Def_ElawaTypes.Name, SalaryPercent = g.SalaryPercent, Value = g.Value}).ToList();
            ViewBag.EmpElawas = JsonConvert.SerializeObject(ob);
            return View();
        }

        public ActionResult AddEmpElawa()
        {
            ViewBag.ElawaTypes =JsonConvert.SerializeObject( db.Def_ElawaTypes.Select(g => new { ID = g.ID, Name = g.Name }).ToList() );
            return View();
        }

        public ActionResult AddEmpElawaEnd(EmpElawa EmpElawa)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.EmpElawa.Add(EmpElawa);
                    db.SaveChanges();
                    var result = new { action="yes",msg="Added" };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    var result = new { action="no" ,msg="error occurred" };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditEmpElawa(int ID)
        {
            ViewBag.EmpElawa = JsonConvert.SerializeObject( db.EmpElawa.Where(a => a.ID == ID).Select(g => new { ID = g.ID, DateAdded = g.DateAdded, SalaryPercent = g.SalaryPercent, Value = g.Value, EmpID = g.EmpID, ElawaID = g.ElawaID }).FirstOrDefault() );
            ViewBag.ElawaTypes = JsonConvert.SerializeObject( db.Def_ElawaTypes.Select(g=> new {ID=g.ID, Name=g.Name}).ToList());
            return View();
        }
        
        public ActionResult EditEmpElawaEnd(EmpElawa EmpElawa)
        {
            if (ModelState.IsValid) {
                try
                {
                    var ob = db.EmpElawa.Where(a => a.ID == EmpElawa.ID).FirstOrDefault();
                    ob.DateAdded = EmpElawa.DateAdded;
                    ob.ElawaID = EmpElawa.ElawaID;
                    ob.SalaryPercent = EmpElawa.SalaryPercent;
                    ob.Value = EmpElawa.Value;
                    db.SaveChanges();
                    return Json(new { action = "yes", msg = "Edited" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { action="no",msg="error occurred" },JsonRequestBehavior.AllowGet);
                }
            }
            else {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
            }
        }







    }
}