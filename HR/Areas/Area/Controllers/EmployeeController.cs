using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{

    public class ModelImage
    {
       public int? EmpID { get; set; }
       public HttpPostedFileWrapper EmpImg { get; set; }

    }


    public class EmployeeController : Controller
    {
        HREntities db = unit.getEntity();
        // GET: Area/Employee

      public ActionResult DeleteImg(int ? EmpID)
        {
            try
            {
              var ob = db.Employee.Where(a => a.ID == EmpID).FirstOrDefault();
              ob.imagebytes = null;
              db.SaveChanges();
                return Json("",JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadImg(ModelImage ModelImage)
        {
           var file = ModelImage.EmpImg;

            if (file!=null)
            {
                try
                {
                string path = Server.MapPath("~/UploadImages/"+ModelImage.EmpID+".jpg");
                file.SaveAs(path);
                byte[] imagebytes = null;
                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebytes = reader.ReadBytes(file.ContentLength);

                    var ob = db.Employee.Where(a => a.ID == ModelImage.EmpID).FirstOrDefault();
                    ob.imagebytes = imagebytes;
                    db.SaveChanges();
                    return Json(new {action="yes",msg="Image Added",Image=File(ob.imagebytes, "image/jpg") },JsonRequestBehavior.AllowGet);
                }
                catch{
                    return Json(new { action = "no", msg = "error",Image="" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "error", Image = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult displayImage(int ? EmpID,DateTime? d)
        {
            var ob =  db.Employee.Where(a=>a.ID== EmpID).FirstOrDefault();
            if (ob.imagebytes != null)
            {
                return File(ob.imagebytes, "image/jpg");
            }
            else
            {
                return Json("",JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            ViewBag.Nationality = db.Def_Nationality.Select(g => new Named { ID = g.ID, Name = g.Name }).ToList();
            ViewBag.Jobs = db.Def_Jobs.Select(g=>new Named { ID=g.ID,Name=g.Name}).ToList();
            ViewBag.Maritalstatus = db.Def_MaritalStatus.Select(g=>new Named {ID=g.ID,Name=g.Name }).ToList();
            ViewBag.Religion = db.Def_Religion.Select(g=>new Named {ID=g.ID,Name=g.Name }).ToList();
            ViewBag.ServiceState = db.Def_ServiceState.Select(g=>new Named {ID=g.ID,Name=g.Name }).ToList();
            ViewBag.Gender = db.Def_Gender.Select(g=>new Named {ID=g.ID,Name=g.Name }).ToList();
            ViewBag.MilitaryService =db.Def_MilitaryService.Select(g=>new Named {ID=g.ID,Name=g.Name }).ToList();
            ViewBag.Emps = db.Employee.Select(g => new Named { ID = g.ID, Name = g.Name }).ToList();
            ViewBag.Degrees =db.Def_Degree.Select(g=>new { ID=g.ID,Name=g.Name}).ToList();
            return View();
        }
        public ActionResult EmpPersonalData()
        {

            return View();
        }

        public ActionResult Save(Employee ob)
        {
            var result = new { res = "", Message = "" };

            if (ModelState.IsValid)
            {
                try
                {
                    if (ob.ID == 0)
                    {
                        try {
                            db.Employee.Add(ob);
                            db.SaveChanges();

                            EmpJobs EmpJobs = new EmpJobs();
                            EmpJobs.EmpID = ob.ID;
                            EmpJobs.JobID = ob.JobID;
                            EmpJobs.fromDate = ob.ActualHireDate;
                            EmpJobs.MasterJobFlag = true;
                            db.EmpJobs.Add(EmpJobs);
                            db.SaveChanges();

                            result = new { res = "yes", Message = "Save successfully" };
                        }
                        catch
                        {
                            result = new { res="no", Message="error occurred" };
                        }
                    }
                    else {
                        Employee Emp = db.Employee.Where(a => a.ID == ob.ID).FirstOrDefault();
                        Emp.JobID = ob.JobID;
                        Emp.MaritalStateID = ob.MaritalStateID;
                        Emp.MilitaryServiceID = ob.MilitaryServiceID;
                        Emp.Name = ob.Name;
                        Emp.NationalityID = ob.NationalityID;
                        Emp.ReligionID = ob.ReligionID;
                        Emp.ServiceState = ob.ServiceState;
                        Emp.ActualHireDate = ob.ActualHireDate;
                        Emp.AddressLocation = ob.AddressLocation;
                        Emp.Telephone = ob.Telephone;
                        Emp.BithDate = ob.BithDate;
                        Emp.Code = ob.Code;
                        Emp.HireDate = ob.HireDate;
                        Emp.GenderID = ob.GenderID;
                        Emp.DegreeID = ob.DegreeID;
                        db.SaveChanges();
                        result = new { res = "yes", Message = "updated successfully" };
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                catch (Exception ex)
                {
                    result = new { res = "no", Message = "not Saved Error" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }

            else { return View(); }
        }


        public ActionResult getAllEmployee()
        {
            var list = db.Employee.Select(g => new { Name = g.Name }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getEmployee(string Name)
        {
            try {
                Employee Emp = db.Employee.Where(a => a.Name == Name).FirstOrDefault();
                var res = JsonConvert.SerializeObject(new { Empp = Emp, ActualHireDate = Emp.ActualHireDate, BithDate = Emp.BithDate, HireDate = Emp.HireDate }, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var res = "no";
                return Json(res,JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getEmpByCode(int Code)
        {
            var Emp = db.Employee.Where(a => a.Code == Code).FirstOrDefault();
            return Json(Emp,JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmpSearch()
        {
            return View();
        }


    }
}