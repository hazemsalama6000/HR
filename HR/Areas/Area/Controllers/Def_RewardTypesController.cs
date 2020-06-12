using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Area.Controllers
{
    public class Def_RewardTypesController : Controller
    {
        HREntities db = unit.getEntity();
        // GET: Area/Def_RewardTypes

        public ActionResult Index()
        {
            ViewBag.RewardTypes =JsonConvert.SerializeObject( db.Def_RewardTypes.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, DaysNum = a.DaysNum, Note = a.Note }).ToList() );
            return View();
        }

        public ActionResult DeleteRewardTypes(int ID)
        {
            try
            {
                var ob = db.Def_RewardTypes.Where(a => a.ID == ID).FirstOrDefault();
                db.Def_RewardTypes.Remove(ob);
                db.SaveChanges();
                var list = JsonConvert.SerializeObject(db.Def_RewardTypes.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, DaysNum = a.DaysNum, Note = a.Note }).ToList());
                return Json(new { action = "yes", msg = "Deleted", result = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { action = "no", msg = "Error", result = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddRewardTypes()
        {
            return View();
        }
        public ActionResult AddRewardTypesEnd(Def_RewardTypes RewardType)
        {
            if (ModelState.IsValid) {
                try {
                    db.Def_RewardTypes.Add(RewardType);
                    db.SaveChanges();
                    var list = JsonConvert.SerializeObject(db.Def_RewardTypes.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, DaysNum = a.DaysNum, Note = a.Note }).ToList());
                    return Json(new {action = "yes" , msg = "Added" , result = list } ,JsonRequestBehavior.AllowGet);
                    }
                catch (Exception ex) {
                    return Json(new { action = "no", msg = "Error", result = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "Error", result = "" }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult EditRewardTypes(int ID)
        {
            ViewBag.RewardType = JsonConvert.SerializeObject(db.Def_RewardTypes.Where(g=>g.ID==ID).Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, DaysNum = a.DaysNum, Note = a.Note }).FirstOrDefault());
            return View();
        }
        public ActionResult EditRewardTypesEnd(Def_RewardTypes RewardType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var ob = db.Def_RewardTypes.Where(a=>a.ID== RewardType.ID).FirstOrDefault();
                    ob.Name = RewardType.Name;
                    ob.Value = RewardType.Value;
                    ob.DaysNum = RewardType.DaysNum;
                    db.SaveChanges();
                    var list = JsonConvert.SerializeObject(db.Def_RewardTypes.Select(a => new { ID = a.ID, Name = a.Name, Value = a.Value, DaysNum = a.DaysNum, Note = a.Note }).ToList());

                    return Json(new { action = "yes", msg = "Editted", result = list }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { action = "no", msg = "Error", result = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "no", msg = "Error", result = "" }, JsonRequestBehavior.AllowGet);
            }

        }




    }
}