using AdminWeb.MyClasses;
using Newtonsoft.Json.Linq;
using Service.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers.admin.main
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            //清空cookies
            if (Request.Cookies["zhengCookies"] != null)
            {
                LoginUsers.UserCache.LoginOut();
                HttpCookie zhengCookies = new HttpCookie("zhengCookies");
                zhengCookies.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(zhengCookies);
            }
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public int LoginCheck(string username, string password)
        {
            ILoginService loginService = ServiceManager<ILoginService>.Get();
            JObject ret = loginService.LoginCheck(username, password);
            if (ret["id"] != null)
            {
                //存储userId
                HttpCookie zhengCookies = new HttpCookie("zhengCookies");
                zhengCookies["userId"] = ret["userId"].ToString();
                zhengCookies.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(zhengCookies);
                // 设置登录凭证
                LoginUsers.UserCache.Login(ret["id"].ToString());
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}