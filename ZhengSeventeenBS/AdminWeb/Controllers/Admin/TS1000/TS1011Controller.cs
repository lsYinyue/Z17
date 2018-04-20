using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Library.MyClasses;
using AdminWeb.MyClasses;
using Service.Library;

namespace AdminWeb.Controllers.Admin.TS1000
{
    public class TS1011Controller : Controller
    {
        // GET: TS1011
        public ActionResult TS1011(string ModuleId)
        {
            ViewData["module"] = "";
            if (ModuleId != null)
            {
                ViewData["module"] = GetModuleById(ModuleId).First.ToString();
            }
            return View();
        }

        public JArray GetModuleType() {
            UserInfo UserInfo = LoginUsers.UserCache.GetUserInfo();
            ITS1010Service TS1010Service = ServiceManager<ITS1010Service>.Get();
            JArray ret = TS1010Service.GetModuleType(UserInfo);
            return ret;
        }

        public JArray GetModuleById(string ModuleId) {
            UserInfo UserInfo = LoginUsers.UserCache.GetUserInfo();
            ITS1010Service TS1010Service = ServiceManager<ITS1010Service>.Get();
            JArray ret = TS1010Service.GetModuleById(ModuleId, UserInfo);
            return ret;
        }
        [HttpPost]
        public JObject EditModuleById(String Module)
        {
            UserInfo UserInfo = LoginUsers.UserCache.GetUserInfo();
            ITS1010Service TS1010Service = ServiceManager<ITS1010Service>.Get();
            JObject ret = TS1010Service.EditModuleById(JObject.Parse(Module), UserInfo);
            return ret;
        }
    }
}