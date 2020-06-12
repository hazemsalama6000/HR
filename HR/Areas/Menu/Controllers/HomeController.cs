using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Menu.Controllers
{
    public class HomeController : Controller
    {
        HREntities db = unit.getEntity();
        // GET: Menu/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string UserName,float? password) {

            if (ModelState.IsValid)
            {
              var ob=db.Def_Users.Where(a => a.UserName == UserName && a.password == password).FirstOrDefault();
                if (ob==null)
                {
                    return Json(new {action="no",msg="المستخدم خطاء"},JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {action="yes",msg=""}, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { action = "error",msg="حدث خطاء"}, JsonRequestBehavior.AllowGet);
            }
        }


    }
}