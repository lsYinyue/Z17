using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.Library.MyClasses;

namespace AdminWeb.MyClasses
{
    public class LoginUsers
    {
        private Dictionary<string, LoginUserItem> userLogin = new Dictionary<string, LoginUserItem>();

        public static LoginUsers UserCache = new LoginUsers();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="token"></param>
        public void Login(string token)
        {
            string user = getCookieUserId();
            if (userLogin.ContainsKey(user))
            {
                userLogin[user].DateTime = DateTime.Now;
                userLogin[user].Token = token;
            }
            else
            {
                userLogin.Add(user, new LoginUserItem
                {
                    UserName = user,
                    DateTime = DateTime.Now,
                    Token = token
                });
            }
        }

        /// <summary>
        /// 设置用户company
        /// </summary>
        /// <param name="company"></param>
        /// <param name="comppanyName"></param>
        public void SetCompany(string company, string comppanyName)
        {
            string user = getCookieUserId();
            if (userLogin.ContainsKey(user))
            {
                userLogin[user].Company = company;
                userLogin[user].CompanyName = comppanyName;
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        public void LoginOut()
        {
            string user = getCookieUserId();
            if (userLogin.ContainsKey(user))
            {
                userLogin.Remove(user);
            }
        }

        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogined()
        {
            string user = getCookieUserId();
            if (userLogin.ContainsKey(user))
            {
                if ((DateTime.Now - userLogin[user].DateTime).TotalHours <= 2)
                {
                    return true;
                }

                userLogin.Remove(user);
            }
            return false;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public LoginUserItem GetCurrentUser()
        {
            string user = getCookieUserId();
            if (userLogin.ContainsKey(user))
            {
                return userLogin[user];
            }
            return null;
        }

        /// <summary>
        /// 获取用户信息UserInfo
        /// </summary>
        /// <returns></returns>
        public UserInfo GetUserInfo()
        {
            string user = getCookieUserId();
            if (userLogin.ContainsKey(user))
            {
                UserInfo UserInfo = new UserInfo();
                UserInfo.Company = userLogin[user].Company;
                UserInfo.Token = userLogin[user].Token;
                UserInfo.UserName = userLogin[user].UserName;
                return UserInfo;
            }
            return null;
        }

        /// <summary>
        /// 获取cookies的userId 
        /// </summary>
        /// <returns></returns>
        public string getCookieUserId()
        {
            HttpCookie zhengCookies = System.Web.HttpContext.Current.Request.Cookies["zhengCookies"];
            string user = zhengCookies?["userId"]?.ToString();
            return user;
        }
    }
}