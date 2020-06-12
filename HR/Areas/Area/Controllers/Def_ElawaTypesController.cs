using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_ElawaTypesController : Controller
    {
        public HREntities db = unit.getEntity();
        // GET: Area/EmpElawa
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getDef_ElawaTypes()
        {
            ViewBag.ElawaTypes = JsonConvert.SerializeObject(db.Def_ElawaTypes.Select(g => new { ID = g.ID, Name = g.Name , Included = g.Included, Repeated = g.Repeated, SalaryPercent = g.SalaryPercent, Taxed = g.Taxed, Value = g.Value, DateAdded = g.DateAdded }).ToList());
            return View();
        }

        public ActionResult AddDef_ElawaTypes()
        {
            return View();
        }
        public ActionResult AddDef_ElawaTypesEnd(Def_ElawaTypes Def_ElawaTypes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Def_ElawaTypes.Add(Def_ElawaTypes);
                    db.SaveChanges();
                    var result = new {action="yes",msg="Saved Successfully" };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    var result = new {action="no",msg="error occurred" };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult EditDef_ElawaTypes(int ID)
        {
            var ob = db.Def_ElawaTypes.Where(a => a.ID == ID).Select(g => new { DateAdded = g.DateAdded , ID = g.ID, Included = g.Included, Name = g.Name, Repeated = g.Repeated, SalaryPercent = g.SalaryPercent, Taxed = g.Taxed, Value = g.Value }).FirstOrDefault();
            ViewBag.Def_ElawaType=JsonConvert.SerializeObject(ob);
            return View();
        }
        public ActionResult EditDef_ElawaTypesEnd(Def_ElawaTypes Def_ElawaType)
        {
            if (ModelState.IsValid) {
                try
                {
                    var ob = db.Def_ElawaTypes.Where(a => a.ID == Def_ElawaType.ID).FirstOrDefault();
                    ob.DateAdded = Def_ElawaType.DateAdded;
                    ob.Included = Def_ElawaType.Included;
                    ob.Name = Def_ElawaType.Name;
                    ob.Repeated = Def_ElawaType.Repeated;
                    ob.SalaryPercent = Def_ElawaType.SalaryPercent;
                    ob.Taxed = Def_ElawaType.Taxed;
                    ob.Value = Def_ElawaType.Value;
                    db.SaveChanges();
                    var result = new {action="yes",msg="Saved" };
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                catch (Exception)

                {
                    var result = new { action="no",msg="Saved"};
                    return Json(result,JsonRequestBehavior.AllowGet);
                }   
            
            }
            else
            {
                var result = new { action = "no", msg = "Saved" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteDef_ElawaType(int ID)
        {
            try
            {
               var ob= db.Def_ElawaTypes.Where(a=>a.ID==ID).FirstOrDefault();
                db.Def_ElawaTypes.Remove(ob);
                db.SaveChanges();
                var result = new {action="yes",msg="Deleted" };
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


    }
}