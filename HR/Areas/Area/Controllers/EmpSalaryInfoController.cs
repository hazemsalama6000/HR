using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpSalaryInfoController : Controller
    {
        // GET: Area/EmpSalaryInfo
        HREntities db = unit.getEntity();
        public ActionResult getEmpSalaryInfo(int EmpID)
        {
           var Emp = db.EmpSalaryInfo.Where(a=>a.EmpID==EmpID).Select(g=>new {ID=g.ID,EmpID=g.EmpID,HireSalary=g.HireSalary, BasicSalary=g.BasicSalary,Insured=g.Insured,Taxed=g.Taxed}).FirstOrDefault();
           ViewBag.EmpSalaryInfo = JsonConvert.SerializeObject(Emp);
           return View();
        }

        public ActionResult SaveEmpSalaryInfo(EmpSalaryInfo EmpSalaryInfo)
        {
            try
            {
                if (EmpSalaryInfo.ID != 0)
                {
                    var ob = db.EmpSalaryInfo.Where(a => a.ID == EmpSalaryInfo.ID).FirstOrDefault();
                    ob.Insured = EmpSalaryInfo.Insured;
                    ob.Taxed = EmpSalaryInfo.Taxed;
                    ob.HireSalary = EmpSalaryInfo.HireSalary;
                    ob.BasicSalary = EmpSalaryInfo.BasicSalary;
                    db.SaveChanges();
                    var result = new { action = "yes", msg = "Saved" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.EmpSalaryInfo.Add(EmpSalaryInfo);
                    db.SaveChanges();
                    var result = new { action = "yes", msg = "Saved" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                var result = new { action = "no", msg = "error occurred" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }




    }
}