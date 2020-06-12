using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_PenaltyController : Controller
    {
        // GET: Area/Def_Penalty

        HREntities db = unit.getEntity();
        public ActionResult Index()
        {
            ViewBag.PenaltyTypes = JsonConvert.SerializeObject(db.Def_Penalty.Select(a=> new {ID=a.ID, Name=a.Name, Value=a.Value, Daysnum=a.Daysnum }).ToList());
            return View();
        }

        public ActionResult AddDef_Penalty()
        {
            return View();
        }


        public ActionResult AddDef_PenaltyEnd(Def_Penalty Def_Penalty)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Def_Penalty.Add(Def_Penalty);
                    db.SaveChanges();
                    var ob = JsonConvert.SerializeObject( db.Def_Penalty.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, Daysnum = a.Daysnum }).ToList());
                    return Json(new {action="yes",msg="Added",result=ob },JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "error occurred", result = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var ob = JsonConvert.SerializeObject(db.Def_Penalty.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, Daysnum = a.Daysnum }).ToList());
                return Json(new { action = "no", msg = "error occurred", result = ob }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult EditDef_Penalty(int ID)
        {
           ViewBag.PenaltyType = JsonConvert.SerializeObject( db.Def_Penalty.Where(a=>a.ID==ID).Select(g=> new { ID=g.ID, Name=g.Name, Value=g.Value, Daysnum=g.Daysnum }).FirstOrDefault() );
            return View();
        }

        public ActionResult EditDef_PenaltyEnd(Def_Penalty Def_Penalty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = db.Def_Penalty.Where(a => a.ID == Def_Penalty.ID).FirstOrDefault();
                    obj.Name = Def_Penalty.Name;
                    obj.Value = Def_Penalty.Value;
                    obj.Daysnum = Def_Penalty.Daysnum;

                    db.SaveChanges();
                    var ob = JsonConvert.SerializeObject(db.Def_Penalty.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, Daysnum = a.Daysnum }).ToList());
                    return Json(new { action = "yes", msg = "Editted", result = ob }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    var ob = JsonConvert.SerializeObject(db.Def_Penalty.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, Daysnum = a.Daysnum }).ToList());
                    return Json(new { action = "no", msg = "error occurred", result = ob }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var ob = JsonConvert.SerializeObject(db.Def_Penalty.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, Daysnum = a.Daysnum }).ToList());
                return Json(new { action = "yes", msg = "error occurred", result = ob }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteDef_Penalty(int ID)
        {
            try {
                var ob = db.Def_Penalty.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_Penalty.Remove(ob);
                db.SaveChanges();
                var obb = JsonConvert.SerializeObject(db.Def_Penalty.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, Daysnum = a.Daysnum }).ToList());
                return Json(new { action = "yes", msg = "Deleted", result = obb }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { action = "no", msg = "error occurred", result = "" }, JsonRequestBehavior.AllowGet);
            }

        }




    }
}