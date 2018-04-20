using AdminWeb.MyClasses;
using Service.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";


            return View();
        }

        //public string servicetype = "oracle";

        public ActionResult TS0000()
        {
            ViewBag.Message = "Your contact page.";

            string user = "wangshuai";

            var currentUser = LoginUsers.UserCache.GetCurrentUser();

            IUserService userService = ServiceManager<IUserService>.Get();

            IRoleService roleService = ServiceManager<IRoleService>.Get();

            userService.Add(user);

            return View();
        }
    }

}