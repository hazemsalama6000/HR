using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpPromotionsController : Controller
    {
        HREntities db = unit.getEntity();

        // GET: Area/EmpPromotions
        public ActionResult getEmpPromotions(int EmpID)
        {
            if(db.EmpPromotions.Where(a => a.EmpID == EmpID).FirstOrDefault()!=null)
               ViewBag.maxID = db.EmpPromotions.Where(a=>a.EmpID==EmpID).Max(g => g.ID);

            var list = db.EmpPromotions.Where(a=>a.EmpID==EmpID).Select(g=>new {ID=g.ID, EmpID=g.EmpID, JobLast= g.Def_Jobs1.Name, DegreeLast=g.Def_Degree.Name, JobCurrent=g.Def_Jobs.Name, DegreeCurrent=g.Def_Degree1.Name, Note=g.Note }).ToList();
            ViewBag.EmpPromotions = JsonConvert.SerializeObject(list);
            return View();
        }

        public ActionResult DeleteEmpPromotions(int ID)
        {
            try {
                var s = db.EmpPromotions.Where(a => a.ID == ID).FirstOrDefault();
                var DegreeLast= s.DegreeLast;
                var JobLast = s.JobLast;
                var emp =  db.Employee.Where(a => a.ID == s.EmpID).FirstOrDefault();
                emp.DegreeID = s.DegreeLast;
                emp.JobID = s.JobLast;
                db.SaveChanges();

                db.EmpPromotions.Remove(s);
                db.SaveChanges();
                var result = new {action="yes", DegreeLast= DegreeLast, JobLast= JobLast, msg="Deleted Successfully" };
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddEmpPromotions()
        {
            return View();
        }

        public ActionResult AddEmpPromotionsEnd(EmpPromotions EmpPromotions)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int EmpID = EmpPromotions.EmpID ?? 0;
                    Employee Employee = db.Employee.Where(a => a.ID == EmpID).FirstOrDefault();

                    EmpPromotions.DegreeLast = Employee.DegreeID;
                    EmpPromotions.JobLast = Employee.JobID;
                    db.EmpPromotions.Add(EmpPromotions);
                    db.SaveChanges();

                    Employee.DegreeID = EmpPromotions.DegreeCurrent;
                    Employee.JobID = EmpPromotions.JobCurrent;
                    db.SaveChanges();
                    var result = new { action="yes",msg="Saved Successfully"};
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    var result = new { action = "no", msg = "error occurred" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditEmpPromotions(int ID)
        {
            var ob = db.EmpPromotions.Where(a=>a.ID==ID).Select(g=> new {ID=g.ID, EmpID=g.EmpID, JobCurrent=g.JobCurrent, DegreeCurrent=g.DegreeCurrent, DegreeLast=g.DegreeLast, JobLast=g.JobLast, Note=g.Note }).FirstOrDefault();
            ViewBag.EmpPromotion = JsonConvert.SerializeObject(ob);

            return View();
        }


        public ActionResult EditEmpPromotionsEnd(EmpPromotions EmpPromotions)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob = db.EmpPromotions.Where(a => a.ID == EmpPromotions.ID).FirstOrDefault();
                    ob.JobCurrent = EmpPromotions.JobCurrent;
                    ob.DegreeCurrent = EmpPromotions.DegreeCurrent;
                    ob.Note = EmpPromotions.Note;
                    db.SaveChanges();

                    var result = new { action = "yes", msg = "Saved Successfully" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    var result = new { action = "no", msg = "error occurred" };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
            }

            else {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
                 }
        }


    }
}