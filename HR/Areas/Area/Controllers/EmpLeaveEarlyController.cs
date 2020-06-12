using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpLeaveEarlyController : Controller
    {
        HREntities db = unit.getEntity();
        // GET: Area/EmpLeaveEarly
        public ActionResult DeleteEmpLeaveEarly(int? ID)
        {
            try
            {
                var ob = db.EmpLeaveEarly.Where(a => a.ID == ID).FirstOrDefault();
                var EmpID = ob.EmpID;
                db.EmpLeaveEarly.Remove(ob);
                db.SaveChanges();
                var res = JsonConvert.SerializeObject(db.EmpLeaveEarly.Where(a => a.EmpID == EmpID).Select(g => new { ID = g.ID, reason = g.reason, ToTime = g.ToTime, FromTime = g.FromTime, DateDay = g.DateDay }).ToList());
                return Json(new { action = "yes", msg = "deleted", result = res }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error", result = "" }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult getEmpLeaveEarly(int? EmpID)
        {
            ViewBag.EmpLeaveEarlys = JsonConvert.SerializeObject(db.EmpLeaveEarly.Where(a=>a.EmpID==EmpID).Select(g=> new { ID=g.ID, reason=g.reason, ToTime=g.ToTime, FromTime=g.FromTime, DateDay=g.DateDay }).ToList());
            return View();
        }

        public ActionResult AddEmpLeaveEarly() {

            return View();
        }
        public ActionResult AddEmpLeaveEarlyEnd(EmpLeaveEarly EmpLeaveEarly) {

            if (ModelState.IsValid)
            {
                try
                {
                    db.EmpLeaveEarly.Add(EmpLeaveEarly);
                    db.SaveChanges();
                    var result= JsonConvert.SerializeObject(db.EmpLeaveEarly.Where(a => a.EmpID == EmpLeaveEarly.EmpID).Select(g => new { ID = g.ID, reason = g.reason, ToTime = g.ToTime, FromTime = g.FromTime, DateDay = g.DateDay }).ToList());
                    return Json(new {action="yes",msg="Added", result = result },JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "Error", result = "" }, JsonRequestBehavior.AllowGet);
                }
            }

            return View();
        }

        public ActionResult EditEmpLeaveEarly(int ? ID)
        {
            ViewBag.EmpLeaveEarly = JsonConvert.SerializeObject(db.EmpLeaveEarly.Where(a => a.ID == ID).Select(g => new { ID = g.ID, reason = g.reason, ToTime = g.ToTime, FromTime = g.FromTime, DateDay = g.DateDay }).FirstOrDefault());
            return View();
        }
        public ActionResult EditEmpLeaveEarlyEnd(EmpLeaveEarly EmpLeaveEarly)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob = db.EmpLeaveEarly.Where(a=>a.ID== EmpLeaveEarly.ID).FirstOrDefault();
                    ob.DateDay = EmpLeaveEarly.DateDay;
                    ob.FromTime = EmpLeaveEarly.FromTime;
                    ob.ToTime = EmpLeaveEarly.ToTime;
                    ob.reason = EmpLeaveEarly.reason;

                    db.SaveChanges();
                    var result = JsonConvert.SerializeObject(db.EmpLeaveEarly.Where(a => a.EmpID == EmpLeaveEarly.EmpID).Select(g => new { ID = g.ID, reason = g.reason, ToTime = g.ToTime, FromTime = g.FromTime, DateDay = g.DateDay }).ToList());
                    return Json(new { action = "yes", msg = "Editted", result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "Error", result = "" }, JsonRequestBehavior.AllowGet);
                }
            }

            return View();
        }



    }
}