using AdminWeb.MyClasses;
using Newtonsoft.Json.Linq;
using Service.Library;
using Service.Library.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers.Admin.TS1000
{
    public class TS1010Controller : Controller
    {
        // GET: TS1010
        public ActionResult TS1010()
        {
            return View();
        }

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public JArray GetAllModules()
        {
            //获取token,userId
            UserInfo UserInfo = LoginUsers.UserCache.GetUserInfo();
            ITS1010Service TS1010Service = ServiceManager<ITS1010Service>.Get();
            JArray ret = TS1010Service.GetAllModules(UserInfo);
            return ret;
        }
        /// <summary>
        /// 根据Id删除模块
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public JObject DelModule(string ModuleId)
        {
            UserInfo UserInfo = LoginUsers.UserCache.GetUserInfo();
            ITS1010Service TS1010Service = ServiceManager<ITS1010Service>.Get();
            JObject ret = TS1010Service.DelModuleById(ModuleId, UserInfo);
            return ret;
        }

    }
}