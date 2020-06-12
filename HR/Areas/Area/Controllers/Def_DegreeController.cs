using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_DegreeController : Controller
    {
        // GET: Area/Def_Degree

        HREntities db = unit.getEntity();

        public ActionResult DeleteDef_Degree(long? ID) {
            try
            {
                var ob=db.Def_Degree.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_Degree.Remove(ob);
                db.SaveChanges();
                var Def_DegreeList = JsonConvert.SerializeObject(db.Def_Degree.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                return Json(new {action="yes",msg="Deleted", Def_DegreeList = Def_DegreeList },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            ViewBag.Def_DegreeViewBag = JsonConvert.SerializeObject( db.Def_Degree.Select(g=> new {ID=g.ID,Name=g.Name}).ToList() );
            return View();
        }


        public ActionResult AddDef_Degree()
        {
            return View();
        }

        public ActionResult AddDef_DegreeEnd(Def_Degree Def_Degree) {

            if (ModelState.IsValid) {
                try
                {
                    db.Def_Degree.Add(Def_Degree);
                    db.SaveChanges();
                    var Def_DegreeList=JsonConvert.SerializeObject(db.Def_Degree.Select(g=> new {ID=g.ID,Name=g.Name}).ToList());
                    return Json(new {action="yes",msg="Added", Def_DegreeList = Def_DegreeList} ,JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult EditDef_Degree(long? ID)
        {
            ViewBag.Def_DegreeViewBag = JsonConvert.SerializeObject(db.Def_Degree.Where(a => a.ID == ID).Select(g=> new { ID=g.ID,Name=g.Name }).FirstOrDefault());
            return View();
        }

        public ActionResult EditDef_DegreeEnd(Def_Degree Def_Degree)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ob= db.Def_Degree.Where(a => a.ID == Def_Degree.ID).FirstOrDefault();
                    ob.Name = Def_Degree.Name;
                    db.SaveChanges();

                    var Def_DegreeList = JsonConvert.SerializeObject(db.Def_Degree.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
                    return Json(new { action = "yes", msg = "Editted", Def_DegreeList = Def_DegreeList }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }




    }
}