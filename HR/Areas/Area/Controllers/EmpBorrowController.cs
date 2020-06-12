using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpBorrowController : Controller
    {
        HREntities db = unit.getEntity();
        // GET: Area/EmpBorrow

        public ActionResult DeleteEmpBorrow(int ID)
        {
            try {
                var ob = db.EmpBorrow.Where(a => a.ID == ID).FirstOrDefault();
                var EmpID = ob.EmpID;
                db.EmpBorrow.Remove(ob);
                db.SaveChanges();
                var result= JsonConvert.SerializeObject(db.EmpBorrow.Where(a => a.EmpID == EmpID).Select(g => new { ID = g.ID, Dateorder = g.Dateorder, payoffDate = g.payoffDate, BorrowVal = g.BorrowVal, Installmentscount = g.Installmentscount, InstallmentVal = g.InstallmentVal, payoffValue = g.payoffValue }).ToList());
                return Json(new {action="",msg="Deleted",result= result },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "error", result = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getEmpBorrow(int EmpID)
        {
            ViewBag.EmpBorrows =JsonConvert.SerializeObject( db.EmpBorrow.Where(a=>a.EmpID== EmpID).Select(g=> new { ID= g.ID, Dateorder= g.Dateorder, payoffDate=g.payoffDate, BorrowVal = g.BorrowVal , Installmentscount= g.Installmentscount, InstallmentVal=g.InstallmentVal, payoffValue=g.payoffValue}).ToList());
            return View();
        }

        public ActionResult AddEmpBorrow()
        {
            return View();
        }

        public ActionResult AddEmpBorrowEnd(EmpBorrow EmpBorrow) {

            if (ModelState.IsValid) {
                try
                {
                    db.EmpBorrow.Add(EmpBorrow);
                    db.SaveChanges();
                    var result = JsonConvert.SerializeObject(db.EmpBorrow.Where(a => a.EmpID == EmpBorrow.EmpID).Select(g => new { ID = g.ID, Dateorder = g.Dateorder, payoffDate = g.payoffDate, BorrowVal = g.BorrowVal, Installmentscount = g.Installmentscount, InstallmentVal = g.InstallmentVal, payoffValue = g.payoffValue }).ToList());
                    return Json(new {action="yes",msg="Added",result=result },JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "", result="" }, JsonRequestBehavior.AllowGet);
                }
            }
            else {
                return Json(new { action = "no", msg = "", result = "" }, JsonRequestBehavior.AllowGet);
                 }
        }

        public ActionResult EditEmpBorrow(int ID)
        {
           ViewBag.EmpBorrow=JsonConvert.SerializeObject(db.EmpBorrow.Where(a => a.ID == ID).Select(g => new { ID = g.ID, Dateorder = g.Dateorder, payoffDate = g.payoffDate, BorrowVal = g.BorrowVal, Installmentscount = g.Installmentscount, InstallmentVal = g.InstallmentVal, payoffValue = g.payoffValue }).FirstOrDefault());
            return View();
        }

        public ActionResult EditEmpBorrowEnd(EmpBorrow EmpBorrow)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var ob = db.EmpBorrow.Where(a => a.ID == EmpBorrow.ID).FirstOrDefault();
                    ob.Dateorder = EmpBorrow.Dateorder;
                    ob.payoffDate = EmpBorrow.payoffDate;
                    ob.BorrowVal = EmpBorrow.BorrowVal;
                    ob.Installmentscount = EmpBorrow.Installmentscount;
                    ob.InstallmentVal = EmpBorrow.InstallmentVal;
                    ob.payoffValue = EmpBorrow.payoffValue;
                    db.SaveChanges();
                    var result = JsonConvert.SerializeObject(db.EmpBorrow.Where(a => a.EmpID == EmpBorrow.EmpID).Select(g => new { ID = g.ID, Dateorder = g.Dateorder, payoffDate = g.payoffDate, BorrowVal = g.BorrowVal, Installmentscount = g.Installmentscount, InstallmentVal = g.InstallmentVal, payoffValue = g.payoffValue }).ToList());
                    return Json(new { action = "yes", msg = "Editted", result = result }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "", result = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else {
                return Json(new { action = "no", msg = "", result = "" }, JsonRequestBehavior.AllowGet);
            }
        }




    }
}