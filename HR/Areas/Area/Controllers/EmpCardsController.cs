using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class EmpCardsController : Controller
    {

        HREntities db = unit.getEntity();

        // GET: Area/EmpCards

        //public ActionResult Upload(HttpPostedFileBase Cardimage)
        //{
        //    Cardimage.SaveAs(Server.MapPath("~/UploadImages/Cards/localImage.jpg"));
        //    return Json(new {action=""},JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult SaveImage(int? CardID)
        //{
        //    var path = Server.MapPath("~/UploadImages/Cards/localImage.jpg");
        //    var path2 = Server.MapPath("~/UploadImages/Cards/"+CardID+".jpg");
        //    System.IO.File.Copy(path, path2);
        //    return Json(new { action = "" }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult getEmpCards(int EmpID)
        {
            var s = db.EmpCards.Where(a => a.EmpID == EmpID).Select(g=>new { ID=g.ID , CardType=g.Def_CardType.Name , IdentityNumber= g.IdentityNumber, Note=g.Note }).ToList();
            ViewBag.EmpCards = JsonConvert.SerializeObject(s);
            return View();
        }

        public ActionResult DeleteImage(int? CardID)
        {
            var path = Server.MapPath("~/UploadImages/Cards/" + CardID + ".jpg");
            var path2 = Server.MapPath("~/UploadImages/default.jpg");
            System.IO.File.Delete(path);
            System.IO.File.Copy(path2, path);

            return Json("",JsonRequestBehavior.AllowGet);
        }

        public ActionResult displayImage(int? CardID, DateTime? d)
        {
            var path = Server.MapPath("~/UploadImages/Cards/" + CardID + ".jpg");

            if (path != null)
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read)) { 

                    BinaryReader reader = new BinaryReader(stream);
                    byte[] imagebytes = reader.ReadBytes((int)stream.Length);

                if (imagebytes != null)
                {
                    return File(imagebytes, "image/jpg");
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            }
            else {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AddEmpCards()
        {
            ViewBag.CardTypes =JsonConvert.SerializeObject( db.Def_CardType.Select(a=> new { ID=a.ID,Name=a.Name}).ToList() );
            return View();
        }

        public ActionResult AddEmpCardsEnd(int? EmpID,int? CardTypeID,string IdentityNumber,string Note, HttpPostedFileBase Cardimage)
        {
            EmpCards EmpCards = new EmpCards();
            EmpCards.IdentityNumber = IdentityNumber;
            EmpCards.Note = Note;
            EmpCards.EmpID = EmpID;
            EmpCards.CardTypeID = CardTypeID;

            if (ModelState.IsValid)
            {
                try {

                    db.EmpCards.Add(EmpCards);
                    db.SaveChanges();
                    Cardimage.SaveAs(Server.MapPath("~/UploadImages/Cards/" + EmpCards.ID + ".jpg"));

                    var res = new { action = "yes", msg = "Added Successfully", CardID = EmpCards.ID };

                    return Json(res,JsonRequestBehavior.AllowGet);
                    }
                catch
                {
                    var res = new {action="no",msg="error occured"};
                    return Json(res ,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var res = new { action = "no", msg = "error occured" };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }



       public ActionResult EditEmpCards(int? EmpCardID)
        {
            var EmpCardEntity= db.EmpCards.Where(a => a.ID == EmpCardID).Select(g=> new { ID = g.ID , EmpID = g.EmpID , CardTypeID = g.CardTypeID , IdentityNumber=g.IdentityNumber, Note = g.Note }).FirstOrDefault();
            ViewBag.EmpCardEntity = JsonConvert.SerializeObject(EmpCardEntity);
            ViewBag.CardTypes = JsonConvert.SerializeObject( db.Def_CardType.Select(g => new { ID = g.ID, Name = g.Name }).ToList() );

            return View();
        }

       public ActionResult EditEmpCardsEnd(int ID,int? EmpID, int? CardTypeID, string IdentityNumber, string Note, HttpPostedFileBase Cardimage)
        {
            EmpCards EmpCards = new EmpCards();
            EmpCards.ID = ID;
            EmpCards.IdentityNumber = IdentityNumber;
            EmpCards.Note = Note;
            EmpCards.EmpID = EmpID;
            EmpCards.CardTypeID = CardTypeID;

            if (ModelState.IsValid)
            {
                try {
                    var ob = db.EmpCards.Where(a => a.ID == EmpCards.ID).FirstOrDefault();
                    ob.CardTypeID = EmpCards.CardTypeID;
                    ob.Note = EmpCards.Note;
                    ob.IdentityNumber = EmpCards.IdentityNumber;
                    db.SaveChanges();
                    if (Cardimage != null)
                    {
                        var path = Server.MapPath("~/UploadImages/Cards/" + EmpCards.ID + ".jpg");
                        System.IO.File.Delete(path);
                        Cardimage.SaveAs(path);
                    }
                    var res = new {action="yes" , msg="Editted Successfully" };
                    return Json(res,JsonRequestBehavior.AllowGet);
                    }
                catch(Exception ex)
                {
                    var res = new { action="no",msg="error occurred"};
                    return Json(res,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var res = new { action = "no", msg = "error occurred" };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteEmpCard(int ID)
        {
            try {
                var ob = db.EmpCards.Where(a => a.ID == ID).FirstOrDefault();
                db.EmpCards.Remove(ob);
                db.SaveChanges();
                var result = new {action="yes",msg="Deleted Successfully"};
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                var result = new {action="no",msg="Error Occurred"};
                return Json(result,JsonRequestBehavior.AllowGet);
            }
        }




    }
}