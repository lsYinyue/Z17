using AdminWeb.MyClasses;
using Newtonsoft.Json.Linq;
using Service.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z17.MySql.Dtos;
using Z17.MySql.Helpers;
using Z17.MySql.Services;

namespace AdminWeb.Controllers.admin.main
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            //清空cookies
            string Token = GetCookieToken.GetToken();
            if (!string.IsNullOrEmpty(Token))
            {
                //LoginUsers.UserCache.LoginOut();
                //缺少数据库登出
                HttpCookie Z17Cookie = new HttpCookie("Z17Cookie");
                Z17Cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(Z17Cookie);
            }
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string LoginService(string username, string password)
        {
            try
            {
                var Token = AccessTokenService.Proxy.Login(username, password);
                //存储userId
                HttpCookie Z17Cookie = new HttpCookie("Z17Cookie");
                Z17Cookie["Token"] = Token;
                Z17Cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(Z17Cookie);
                return "登录成功";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            };
        }

    }
}