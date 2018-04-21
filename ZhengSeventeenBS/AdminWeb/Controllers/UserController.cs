using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z17.Core.Services;

namespace AdminWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserMenus(string userid)
        {
            var userMenus = PermissionService.Proxy.GetUserMenuItems(userid);
            return View(userMenus);
        }
    }
}