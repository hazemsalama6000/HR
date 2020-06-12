using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpAttendanceController : Controller
    {
        // GET: Area/EmpAttendance
        HREntities db = unit.getEntity();
        public ActionResult getEmpAttendance(int ID)
        {
            ViewBag.EmpAttendances = JsonConvert.SerializeObject(db.EmpAttendance.Where(a => a.EmpID == ID).Select(g=> new {ID=g.ID, DateAdded=g.DateAdded, TimeFrom=g.TimeFrom, TimeTo=g.TimeTo, Name=g.Def_Attendance.Name , Note =g.Note }).ToList());
            return View();
        }

        public ActionResult AddEmpAttendance()
        {
            ViewBag.AttendanceType = JsonConvert.SerializeObject( db.Def_Attendance.Select(a => new { ID = a.ID, Name = a.Name }).ToList() );
            return View();
        }

        public ActionResult AddEmpAttendanceEnd(EmpAttendance EmpAttendance)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.EmpAttendance.Add(EmpAttendance);
                    db.SaveChanges();
                    return Json(new {action="yes",msg="Added" } , JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new {action="no",msg="error occurred" },JsonRequestBehavior.AllowGet);
                }
            }
            else {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
                 }
        }

        public ActionResult EditEmpAttendance(int ID)
        {
            ViewBag.EmpAttendance = JsonConvert.SerializeObject(db.EmpAttendance.Where(a=>a.ID==ID).Select(g=> new {ID=g.ID ,DateAdded=g.DateAdded, TimeTo=g.TimeTo,TimeFrom=g.TimeFrom ,EmpID=g.EmpID, AttenID=g.AttenID,Note=g.Note }).FirstOrDefault());
            ViewBag.AttendanceType = JsonConvert.SerializeObject(db.Def_Attendance.Select(a => new { ID = a.ID, Name = a.Name }).ToList());
            return View();
        }
        public ActionResult EditEmpAttendanceEnd(EmpAttendance EmpAttendance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob= db.EmpAttendance.Where(a=>a.ID== EmpAttendance.ID).FirstOrDefault();
                    ob.AttenID = EmpAttendance.AttenID;
                    ob.DateAdded = EmpAttendance.DateAdded;
                    ob.TimeFrom = EmpAttendance.TimeFrom;
                    ob.TimeTo = EmpAttendance.TimeTo;
                    ob.Note = EmpAttendance.Note;
                    db.SaveChanges();
                    return Json(new { action = "yes", msg = "Editted" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
                }
            }
            else {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult DeleteEmpAttendance(int ID)
        {
            try
            {
                var ob =  db.EmpAttendance.Where(a => a.ID == ID).FirstOrDefault();
                db.EmpAttendance.Remove(ob);
                db.SaveChanges();
                return Json(new {action="yes", msg="Deleted" },JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);

            }
        }



    }
}