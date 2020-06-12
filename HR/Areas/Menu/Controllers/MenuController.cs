using HR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.Menu.Controllers
{
    public class MenuController : Controller
    {
        HREntities db = unit.getEntity();
        // GET: Menu/Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getMenu()
        {
            ViewBag.menu =JsonConvert.SerializeObject(db.Conf_Menu.Select(g => new { ID = g.ID, Name = g.Name, IsEnabled = g.IsEnabled }).ToList() );
            ViewBag.SubMenu = JsonConvert.SerializeObject(db.Conf_SubMenu.Select(g => new { ID = g.ID, MenuID = g.MenuID, Name = g.Name, AreaName = g.AreaName, ControllerName = g.ControllerName, PageName = g.PageName, IsEnabled = g.IsEnabled }).ToList());
            ViewBag.SubSubMenu =JsonConvert.SerializeObject(db.Conf_SubsubMenu.Select(g => new { ID = g.ID, MenuID = g.SubMenuID, Name = g.Name, AreaName = g.AreaName, ControllerName = g.ControllerName, PageName = g.PageName, IsEnabled = g.IsEnabled }).ToList());
            return View();
        }




    }
}