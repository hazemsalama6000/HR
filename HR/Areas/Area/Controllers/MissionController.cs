using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class MissionController : Controller
    {
        HREntities db = unit.getEntity();
        // GET: Area/Mission
        public ActionResult Index(int ID)
        {
            return View();
        }


        public ActionResult DeleteMission(int MissionID)
        {
            try {
                var ob = db.Mission.Where(a => a.ID == MissionID).FirstOrDefault();
                db.Mission.Remove(ob);
                db.SaveChanges();
                var result = new {action="yes" ,msg="Deleted"};
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EmpMissions(int EmpID)
        {
           var objects=db.Mission.Where(a=>a.EmpID==EmpID).Select(g => new {ID=g.ID,Note=g.Note , MissionOrTravel = g.MissionOrTravel, fromDate = g.fromDate, toDate = g.toDate }).ToList();
           var list= objects.Select(g => new { ID = g.ID, Note = g.Note, MissionOrTravel = g.MissionOrTravel, fromDate = (g.fromDate == null || g.fromDate == DateTime.MinValue) ? null : g.fromDate, toDate = (g.toDate == null || g.toDate == DateTime.MinValue) ? null : g.toDate }).ToList();

           return Json(JsonConvert.SerializeObject(list),JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmpMission(int MissionID)
        {
            var ob = db.Mission.Where(a => a.ID == MissionID).Select(g => new { ID = g.ID, Note = g.Note, MissionOrTravel = g.MissionOrTravel, fromDate = g.fromDate, toDate = g.toDate }).FirstOrDefault();
            var obb = new { ID = ob.ID, Note = ob.Note, MissionOrTravel = ob.MissionOrTravel, fromDate = ob.fromDate, toDate = ob.toDate };   
            return Json(JsonConvert.SerializeObject(obb),JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddMission()
        {
            return View();
        }
        public ActionResult AddMissionEnd(Mission Mission)
        {
            if (ModelState.IsValid) {
                try {
                    db.Mission.Add(Mission);
                    db.SaveChanges();
                    var result = new { action = "yes", msg = "Added Successfully" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    var result = new {action="no",msg="error ocurred" };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { action = "no", msg = "error ocurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult EditMission()
        {
            return View();
        }
        public ActionResult EditMissionEnd(Mission Mission)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var ob= db.Mission.Where(a => a.ID == Mission.ID).FirstOrDefault();
                    ob.Note = Mission.Note;
                    ob.fromDate = Mission.fromDate;
                    ob.toDate = Mission.toDate;
                    ob.MissionOrTravel = Mission.MissionOrTravel;

                    db.SaveChanges();
                    var result = new {action="yes",msg="Edited successfully"};
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    var result = new { action = "no", msg = "error occurred" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { action = "no", res = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


    }
}