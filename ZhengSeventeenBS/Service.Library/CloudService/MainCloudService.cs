using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Library.MyClasses;

namespace Service.Library
{
    public class MainCloudService : IMainService
    {

        /// <summary>
        /// 根据用户Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public JArray GetUserCompany(UserInfo UserInfo)
        {
            var userId = UserInfo.UserName;
            var Token = UserInfo.Token;
            var requests = new List<Object>();
            JObject param = new JObject
            {
                {"include","companyPointer" },
                {"where", new JObject{ { "status","1" },{ "user",userId} }},
                {"fields",new JArray{ "id", "company"}},
                {"includeFilter",new JObject{{ "company",new JObject{{ "fields", new JArray { "id", "name"}}}}}}
            };
            requests.Add(new
            {
                method = "GET",
                path = "/mcm/api/comStaff?filter=" + JsonConvert.SerializeObject(param)

            });
            JArray ret = ComCloud.ajaxRequest(requests, Token);
            return JArray.Parse(ret.First().ToString());
        }
        /// <summary>
        /// 获取用户对应的模块
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="company"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public JArray GetUserModule(UserInfo UserInfo)
        {
            var userId = UserInfo.UserName;
            var Token = UserInfo.Token;
            var company = UserInfo.Company;
            var requests = new List<Object>();
            JObject param = new JObject
            {
                {
                    "where",new JObject{
                        {
                            "type","PC"
                        },
                        {
                            "enabled","Y"
                        }
                    }
                },{
                    "fields",new JArray{ "id", "enabled", "name", "parent", "pagePath", "pageNo"}
                }
            };
            requests.Add(new
            {
                method = "GET",
                path = "/mcm/api/module?filter=" + JsonConvert.SerializeObject(param)
            });
            JArray ret = ComCloud.ajaxRequest(requests, Token);
            return ret;
        }
    }
}
