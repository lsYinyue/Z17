using AdminWeb.MyClasses;
using Newtonsoft.Json.Linq;
using Service.Library;
using Service.Library.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z17.MySql.Helpers;
using Z17.MySql.Services;

namespace AdminWeb.Controllers.Admin.Main
{
    public class MainController : Controller
    {
        // GET: Index
        [CheckIsLoin]
        public ActionResult Index()
        {
            string Token = GetCookieToken.GetToken();
            string UserName = AccessTokenService.Proxy.GetUserFromAccessToken(Token).UserName;
            ViewData["UserName"] = UserName;
            return View();
        }

        /// <summary>
        /// 获取用户模块
        /// </summary>
        /// <returns></returns>
        public string GetUserModule()
        {
            string Token = GetCookieToken.GetToken();
            var ret = PermissionService.Proxy.GetUserMenuItems(Token);
            return JsonHelper.Instance.ToJson(ret);
        }
    }
}