using AdminWeb.MyClasses;
using Newtonsoft.Json.Linq;
using Service.Library;
using Service.Library.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers.Admin.Main
{
    public class MainController : Controller
    {
        // GET: Main
        [CheckIsLoin]
        public ActionResult Main()
        {
            ViewData["comppanyName"] = GetUserCompany();
            return View();
        }
        public ActionResult Index()
        {
            
            return View();
        }
        /// <summary>
        /// 获取登录用户的公司
        /// </summary>
        /// <returns></returns>
        [CheckIsLoin]
        public String GetUserCompany()
        {
            //获取token,userId
            UserInfo UserInfo = LoginUsers.UserCache.GetUserInfo();
            IMainService mainService = ServiceManager<IMainService>.Get();
            JArray ret = mainService.GetUserCompany(UserInfo);

            if (0 == ret.Count)
            {
                return "";
            }
            var company = ret.First?["company"]?["id"]?.ToString();
            var comppanyName = ret.First?["company"]?["name"]?.ToString();

            LoginUsers.UserCache.SetCompany(company, comppanyName);
            return comppanyName;
        }

        /// <summary>
        /// 获取用户模块
        /// </summary>
        /// <returns></returns>
        [CheckIsLoin]
        public JArray GetUserModule()
        {
            UserInfo UserInfo = LoginUsers.UserCache.GetUserInfo();


            IMainService mainService = ServiceManager<IMainService>.Get();

            JArray ret = mainService.GetUserModule(UserInfo);
            return ret;
        }
    }
}