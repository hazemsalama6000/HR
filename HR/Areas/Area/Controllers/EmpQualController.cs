using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HR.Areas.Area.Controllers
{
    public class EmpQualController : Controller
    {
        HREntities db = unit.getEntity();
        // GET: Area/EmpQual
        public ActionResult Index(int? ID)
        {

           var list = db.EmpQualification.Where(a => a.EmpID == ID).Select(g => new { ID = g.ID, University = g.University, fromDate = g.fromDate, toDate = g.toDate, Name = g.Def_QualificationTypes.Name }).ToList();
           var s = list.Select(g => new { ID = g.ID, University = g.University, fromDate = (g.fromDate==null||g.fromDate==DateTime.MinValue)? null:g.fromDate , toDate = (g.toDate==null|| g.toDate == DateTime.MinValue)? null:g.toDate , Name = g.Name }).ToList();
           ViewBag.AllQualification = JsonConvert.SerializeObject(s);
           return View();
        }


        public ActionResult DeleteEmpQual(int ID) {
            try {
                EmpQualification EmpQualification = db.EmpQualification.Where(a => a.ID == ID).FirstOrDefault();
                db.EmpQualification.Remove(EmpQualification);
                db.SaveChanges();
                var result = new {action="yes" , msg="Deleted successfully" };
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var result = new {action="no",msg="Error occurred"};
                return Json(result,JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AddEmpQualPopup()
        {
            ViewBag.QualificationTypes = JsonConvert.SerializeObject(db.Def_QualificationTypes.Select(g=>new {ID=g.ID,Name=g.Name }).ToList());
            return View();
        }

       public ActionResult AddEmpQual(EmpQualification EmpQualification)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    db.EmpQualification.Add(EmpQualification);
                    db.SaveChanges();
                    var result = new { res = "yes", msg = "added" };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                catch
                {
                    var result = new { res = "no", msg = "error ocured" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return View();
            }
        }


        public string getEmpquali(int ID)
        {
                var EmpQualification = db.EmpQualification.Where(a => a.ID == ID).Select(g => new { ID = g.ID, QualID=g.QualID , EmpID=g.EmpID, University=g.University, fromDate=g.fromDate,toDate=g.toDate}).FirstOrDefault();
                var EmpQualificationO = new { ID = EmpQualification.ID, QualID = EmpQualification.QualID, EmpID = EmpQualification.EmpID, University = EmpQualification.University, fromDate = (EmpQualification.fromDate==null|| EmpQualification.fromDate == DateTime.MinValue)?"" : EmpQualification.fromDate.Value.ToShortDateString() , toDate = (EmpQualification.toDate==null|| EmpQualification.toDate == DateTime.Now)?"": EmpQualification.toDate.Value.ToShortDateString() };

                return JsonConvert.SerializeObject(EmpQualificationO);   
        }

        public ActionResult EditEmpQual()
        {
            try
            {
                ViewBag.QualificationTypes = JsonConvert.SerializeObject(db.Def_QualificationTypes.Select(g => new { ID = g.ID, Name = g.Name }).ToList());
            }
            catch
            {

            }
            return View();
        }
        public ActionResult EditEmpQualEnd(EmpQualification EmpQualification)
        {

            try {
                var ob = db.EmpQualification.Where(a => a.ID == EmpQualification.ID).FirstOrDefault();
                ob.QualID = EmpQualification.QualID;
                ob.University = EmpQualification.University;
                ob.toDate = EmpQualification.toDate;
                ob.fromDate = EmpQualification.fromDate;

                db.SaveChanges();
                var result = new { res="yes",msg="Edited"};
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var result = new { res = "no", msg = "error ocured" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


    }
}