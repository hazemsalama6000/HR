using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpJobsController : Controller
    {
        HREntities db = unit.getEntity();

        // GET: Area/EmpJobs


        public ActionResult DeleteJob(int ID)
        {
            try
            {
               var s= db.EmpJobs.Where(a => a.ID == ID).FirstOrDefault();
                db.EmpJobs.Remove(s);
                db.SaveChanges();
                var result = new { action = "yes", msg = "Deleted Successfully" };
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getEmpJobs(int EmpID)
        {
            var l  = db.EmpJobs.Where(a => a.EmpID == EmpID).Select(g => new { ID = g.ID , MasterJobFlag=g.MasterJobFlag, JobName = g.Def_Jobs.Name, fromDate=g.fromDate, toDate = g.toDate, Note = g.Note }).ToList();
            var s=l.Select(g => new { ID = g.ID, MasterJobFlag=g.MasterJobFlag, JobName = g.JobName, fromDate = (g.fromDate == null || g.fromDate == DateTime.MinValue) ?null : g.fromDate, toDate = (g.toDate == null || g.toDate == DateTime.MinValue) ? null : g.toDate, Note = g.Note });
            ViewBag.Jobs = JsonConvert.SerializeObject(s);
            return View();
        }
        
        public ActionResult AddEmpJobs()
        {
            ViewBag.jobs = JsonConvert.SerializeObject( db.Def_Jobs.Select(g=> new {ID=g.ID,Name=g.Name}).ToList() );
            return View();
        }

        public ActionResult AddEmpJobsEnd(EmpJobs EmpJobs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EmpJobs.MasterJobFlag = false;
                    db.EmpJobs.Add(EmpJobs);
                    db.SaveChanges();

                    var result = new {res="yes",msg="Added successfully" };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    var result = new { res = "no", msg = "error occurred" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { res = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
         }


        public ActionResult EditEmpJobs(int ID)
        {
            var jobs=  db.Def_Jobs.Select(g => new { ID = g.ID, Name = g.Name }).ToList();
            ViewBag.JobsList = JsonConvert.SerializeObject(jobs);

            var ob=  db.EmpJobs.Where(a=>a.ID==ID).Select(g=> new { ID=g.ID , MasterJobFlag=g.MasterJobFlag, JobID=g.JobID , Note=g.Note , fromDate=g.fromDate , toDate=g.toDate  }).FirstOrDefault();
            ViewBag.ObjectEdit= JsonConvert.SerializeObject(ob);

            return View();
        }


        public ActionResult EditEmpJobsEnd(EmpJobs EmpJobs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var ob= db.EmpJobs.Where(a => a.ID == EmpJobs.ID).FirstOrDefault();
                    ob.JobID = EmpJobs.JobID;
                    ob.fromDate = EmpJobs.fromDate;
                    ob.toDate = EmpJobs.toDate;
                    ob.Note = EmpJobs.Note;
                    ob.MasterJobFlag = false;
                    db.SaveChanges();

                    var res = new {action="yes",msg="Editted Successfully" };
                    return Json(res,JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    var res = new { action = "no", msg = "error occurred" };
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var res = new { action = "no", msg = "error occurred" };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }










    }
}