using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AdminWeb.MyClasses
{
    public class CheckIsLoin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string Token = GetCookieToken.GetToken();
            if (string.IsNullOrEmpty(Token))
            {
                filterContext.HttpContext.Response.Redirect("/login/login");//否则跳转至登录页
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }

            //string user = LoginUsers.UserCache.getCookieUserId();
            //if (string.IsNullOrEmpty(user) || LoginUsers.UserCache.IsLogined() == false)
            //{
            //    filterContext.HttpContext.Response.Redirect("/login/login");//否则跳转至登录页
            //}
            //else
            //{
            //    base.OnActionExecuting(filterContext);
            //}
        }

    }
}