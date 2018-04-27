using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminWeb.MyClasses
{
    public class GetCookieToken
    {

        /// <summary>
        /// 获取cookies的Token
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            HttpCookie Z17Cookie = System.Web.HttpContext.Current.Request.Cookies["Z17Cookie"];
            string Token = Z17Cookie?["Token"]?.ToString();
            return Token;
        }
    }
}