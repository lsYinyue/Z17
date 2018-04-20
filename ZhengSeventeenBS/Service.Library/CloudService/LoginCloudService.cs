using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Library
{
    public class LoginCloudService : ILoginService
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public JObject LoginCheck(string username, string password)
        {
            var requests = new List<Object>();
            requests.Add(new
            {
                method = "POST",
                path = "/mcm/api/user/login",
                body = new
                {
                    username = username,
                    password = password
                }
            });
            var Token = "";
            JArray ret = ComCloud.ajaxRequest(requests, Token);
            return JObject.Parse(ret.First().ToString());
        }
    }
}
