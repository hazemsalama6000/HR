using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpAllowanceController : Controller
    {
        // GET: Area/EmpAllowance

        HREntities db = unit.getEntity();

        public ActionResult getAllowanceValue(int? ID)
        {
          var ob = db.Def_AllowanceTypes.Where(a => a.ID == ID).Select(g=> new { Value=g.Value}).FirstOrDefault();
          return Json(ob,JsonRequestBehavior.AllowGet);
        }


        public ActionResult getEmpAllowance(int? ID)
        {
            var obList = JsonConvert.SerializeObject( db.EmpAllowance.Where(a => a.EmpID == ID).Select(g => new { ID = g.ID, Name=g.Def_AllowanceTypes.Name,DateFrom = g.DateFrom, DateTo = g.DateTo, EmpID = g.EmpID, Value = g.Value }).ToList() );
            ViewBag.EmpAllowances = obList;
            return View();
        }

        public ActionResult DeleteEmpAllowance(int ID)
        {
            try
            {
                var ob = db.EmpAllowance.Where(a => a.ID == ID).FirstOrDefault();
                db.EmpAllowance.Remove(ob);
                db.SaveChanges();
                return Json(new {action="yes",msg="Deleted" },JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddEmpAllowance()
        {
            ViewBag.AllowanceTypes = JsonConvert.SerializeObject(db.Def_AllowanceTypes.Select(g=> new {ID=g.ID,Name=g.Name}).ToList());
            return View();
        }

        public ActionResult AddEmpAllowanceEnd(EmpAllowance EmpAllowance)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.EmpAllowance.Add(EmpAllowance);
                    db.SaveChanges();
                    return Json(new { action = "yes", msg = "Added" }, JsonRequestBehavior.AllowGet);
                    }
                catch
                {
                    return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult EditEmpAllowance(int ID)
        {
            ViewBag.EmpAllowance = JsonConvert.SerializeObject( db.EmpAllowance.Where(a => a.ID == ID).Select(g=> new { ID=g.ID, Value=g.Value,DateFrom=g.DateFrom, DateTo=g.DateTo, AllowanceTypeID=g.AllowanceTypeID }).FirstOrDefault() );
            ViewBag.AllowanceTypes = JsonConvert.SerializeObject(db.Def_AllowanceTypes.Select(g=> new {ID=g.ID, Name=g.Name }).ToList());
            return View();
        }

        public ActionResult EditEmpAllowanceEnd(EmpAllowance EmpAllowance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob =  db.EmpAllowance.Where(a => a.ID == EmpAllowance.ID).FirstOrDefault();
                    ob.AllowanceTypeID = EmpAllowance.AllowanceTypeID;
                    ob.DateFrom = EmpAllowance.DateFrom;
                    ob.DateTo = EmpAllowance.DateTo;
                    ob.Value = EmpAllowance.Value;
                    db.SaveChanges();
                    return Json(new {action="yes",msg="Editted" },JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
            }
        }




    }
}