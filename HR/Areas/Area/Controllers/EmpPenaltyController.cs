using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpPenaltyController : Controller
    {
        HREntities db = unit.getEntity();

        public ActionResult getPenaltyValue(int ID)
        {
            var ob = db.Def_Penalty.Where(a => a.ID == ID).Select(g=>new { Value=g.Value , Daysnum=g.Daysnum}).FirstOrDefault();
            return Json(ob,JsonRequestBehavior.AllowGet);
        }

        public ActionResult getEmpPenalty(int ID)
        {
            ViewBag.EmpPenaltys = JsonConvert.SerializeObject( db.EmpPenalty.Select(g => new { ID = g.ID, DateAdded=g.DateAdded ,Name = g.Def_Penalty.Name, Daysnum = g.Daysnum, Value = g.Value , Note=g.Note}).ToList() );
            return View();
        }

        public ActionResult AddEmpPenalty()
        {
            ViewBag.PenaltyTypes=JsonConvert.SerializeObject(db.Def_Penalty.Select(a => new { ID = a.ID, Name = a.Name }).ToList());
            return View();
        }

        public ActionResult AddEmpPenaltyEnd(EmpPenalty EmpPenalty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.EmpPenalty.Add(EmpPenalty);
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


        public ActionResult EditEmpPenalty(int ID)
        {
            ViewBag.EmpPenalty = JsonConvert.SerializeObject( db.EmpPenalty.Where(a=>a.ID==ID).Select(g=> new { ID=g.ID, DateAdded=g.DateAdded , EmpID=g.EmpID, PenID=g.PenID , Value=g.Value, Daysnum=g.Daysnum, Note=g.Note }).FirstOrDefault() );
            ViewBag.PenaltyTypes = JsonConvert.SerializeObject( db.Def_Penalty.Select(g=> new { ID=g.ID, Name=g.Name }).ToList() );
            return View();
        }

        public ActionResult EditEmpPenaltyEnd(EmpPenalty EmpPenalty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob = db.EmpPenalty.Where(a => a.ID == EmpPenalty.ID).FirstOrDefault();
                    ob.PenID = EmpPenalty.PenID;
                    ob.DateAdded = EmpPenalty.DateAdded;
                    ob.Value = EmpPenalty.Value;
                    ob.Daysnum = EmpPenalty.Daysnum;
                    ob.Note = EmpPenalty.Note;
                    db.SaveChanges();
                    return Json(new {action="yes",msg="Edited" },JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new {action="no",msg="error occurred" },JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error occurred" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteEmpPenalty(int ID)
        {
            try {
                var ob = db.EmpPenalty.Where(a => a.ID == ID).FirstOrDefault();
                db.EmpPenalty.Remove(ob);
                db.SaveChanges();
                return Json( new {action="yes",msg="Deleted" },JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new {action="no",msg="error occurred" },JsonRequestBehavior.AllowGet);
            }
        }

    }
}