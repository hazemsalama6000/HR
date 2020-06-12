using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpRewardsController : Controller
    {
        HREntities db = unit.getEntity();

        public ActionResult getRewardValue(int? ID)
        {
            var ob = db.Def_RewardTypes.Where(a => a.ID == ID).Select(g => new { DaysNum = g.DaysNum, Value = g.Value }).FirstOrDefault();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getEmpRewards(int ID)
        {
            ViewBag.EmpRewards = JsonConvert.SerializeObject( db.EmpRewards.Where(a => a.EmpID == ID).Select(g=>new { ID = g.ID , DateAdded = g.DateAdded , Name = g.Def_RewardTypes.Name , Value = g.Value , DaysNum = g.DaysNum , Note = g.Note}).ToList() );
            return View();
        }

        public ActionResult AddEmpRewards()
        {
            ViewBag.RewardTypes = JsonConvert.SerializeObject( db.Def_RewardTypes.Select(g => new { ID=g.ID, Name = g.Name }).ToList() );
            return View();
        }

        public ActionResult AddEmpRewardsEnd(EmpRewards EmpRewards)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.EmpRewards.Add(EmpRewards);
                    db.SaveChanges();
                    return Json(new {action="yes",msg="Added" } , JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new {action="no",msg="error occurred" },JsonRequestBehavior.AllowGet);
                }
            }
            else { return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet); }
        }

        public ActionResult EditEmpRewards(int ID)
        {
            ViewBag.RewardTypes = JsonConvert.SerializeObject( db.Def_RewardTypes.Select(g=> new { ID=g.ID, Name=g.Name }).ToList() );
            ViewBag.EmpReward = JsonConvert.SerializeObject( db.EmpRewards.Where(a=>a.ID==ID).Select(g=> new { ID=g.ID, DateAdded=g.DateAdded, RewardID=g.RewardID, DaysNum=g.DaysNum, Value=g.Value, Note = g.Note }).FirstOrDefault() );
            return View();
        }

        public ActionResult EditEmpRewardsEnd(EmpRewards EmpReward)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob = db.EmpRewards.Where(a => a.ID == EmpReward.ID).FirstOrDefault();
                    ob.Note = EmpReward.Note;
                    ob.RewardID = EmpReward.RewardID;
                    ob.Value = EmpReward.Value;
                    ob.DaysNum = EmpReward.DaysNum;
                    ob.DateAdded = EmpReward.DateAdded;
                    db.SaveChanges();
                    return Json(new { action = "yes", msg = "Edited" }, JsonRequestBehavior.AllowGet);
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


        public ActionResult DeleteEmpRewards(int ID)
        {
            try {
                var ob = db.EmpRewards.Where(a => a.ID == ID).FirstOrDefault();
                db.EmpRewards.Remove(ob);
                db.SaveChanges();
                return Json(new {action="yes",msg="Deleted" },JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new {action="no",msg="error occurred" },JsonRequestBehavior.AllowGet);
            }
        }









    }
}