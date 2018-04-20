using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Library.MyClasses;
using AdminWeb.MyClasses;
using Service.Library;
using Newtonsoft.Json.Linq;

namespace AdminWeb.Controllers.Admin.TS1000
{
    public class TS1012Controller : Controller
    {
        // GET: TS1012
        public ActionResult TS1012()
        {
            return View();
        }
        [HttpPost]
        public JObject AddModule(String Module)
        {
            UserInfo UserInfo = LoginUsers.UserCache.GetUserInfo();
            ITS1010Service TS1010Service = ServiceManager<ITS1010Service>.Get();
            JObject ret = TS1010Service.AddModule(JObject.Parse(Module), UserInfo);
            return ret;
        }
    }
}