using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_AllowanceTypesController : Controller
    {
        // GET: Area/Def_AllowanceTypes
        HREntities db = unit.getEntity();
        public ActionResult Index()
        {
            ViewBag.AllowanceTypes = JsonConvert.SerializeObject(db.Def_AllowanceTypes.Select(a => new { ID=a.ID, Name = a.Name, Value = a.Value }).ToList());
            return View();
        }

        public ActionResult AddAllowanceType()
        {
            return View();
        }

        public ActionResult AddAllowanceTypeEnd(Def_AllowanceTypes AllowanceType)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Def_AllowanceTypes.Add(AllowanceType);
                    db.SaveChanges();
                    var s=JsonConvert.SerializeObject(db.Def_AllowanceTypes.Select(a => new { ID=a.ID,Name = a.Name, Value = a.Value }).ToList());
                    return Json(new {action="yes",msg="Added" ,result=s} , JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { action = "no", msg = "error occurred", result = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error occurred", result = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditAllowanceType(int ID)
        {
            ViewBag.AllowanceType = JsonConvert.SerializeObject( db.Def_AllowanceTypes.Where(a => a.ID == ID).Select(g=> new { ID=g.ID, Name=g.Name, Value=g.Value }).FirstOrDefault());
            return View();
        }

        public ActionResult EditAllowanceTypeEnd(Def_AllowanceTypes AllowanceType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob = db.Def_AllowanceTypes.Where(a => a.ID == AllowanceType.ID).FirstOrDefault();
                    ob.Name = AllowanceType.Name;
                    ob.Value = AllowanceType.Value;

                    db.SaveChanges();
                    var s = JsonConvert.SerializeObject(db.Def_AllowanceTypes.Select(a => new { ID=a.ID, Name = a.Name, Value = a.Value }).ToList());
                    return Json(new { action = "yes", msg = "Editted", result = s }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { action = "no", msg = "error occurred", result = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error occurred", result = "" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteAllowanceType(int ID)
        {
            try
            {
                var ob = db.Def_AllowanceTypes.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_AllowanceTypes.Remove(ob);
                db.SaveChanges();
                var s = JsonConvert.SerializeObject(db.Def_AllowanceTypes.Select(g => new { ID = g.ID, Name = g.Name, Value = g.Value }).ToList());
                return Json(new { action = "yes", msg = "Deleted", result = s }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { action = "no", msg = "error occurred", result = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }




    }
}