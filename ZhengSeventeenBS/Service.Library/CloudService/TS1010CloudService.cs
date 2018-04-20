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
    public class TS1010CloudService : ITS1010Service
    {
        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public JObject AddModule(JObject Module, UserInfo UserInfo)
        {
            var requests = new List<Object>();
            requests.Add(new
            {
                method = "POST",
                path = "/mcm/api/module",
                body = Module
            });
            JArray ret = ComCloud.ajaxRequest(requests, UserInfo.Token);
            return ComCloud.callBackData(ret);
        }
        
        /// <summary>
        /// 根据Id删除模块
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public JObject DelModuleById(string ModuleId, UserInfo UserInfo)
        {
            var requests = new List<Object>();
            requests.Add(new
            {
                method = "DELETE",
                path = "/mcm/api/module/" + ModuleId
            });
            JArray ret = ComCloud.ajaxRequest(requests, UserInfo.Token);
            return ComCloud.callBackData(ret);
        }
        
        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public JObject EditModuleById(JObject Module, UserInfo UserInfo)
        {
            var requests = new List<Object>();
            string Spath = "/mcm/api/module/" + Module["id"];
            Module.Remove("id");
            requests.Add(new
            {
                method = "PUT",
                path = Spath,
                body = Module
            });
            JArray ret = ComCloud.ajaxRequest(requests, UserInfo.Token);
            return ComCloud.callBackData(ret); ;

        }

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public JArray GetAllModules(UserInfo UserInfo)
        {
            var requests = new List<Object>();
            //获取所有父模块
            JObject paramParent = new JObject
            {
                {
                    "where", new JObject
                    {
                        { "pagePath",null}
                    }
                },
                {
                    "pageNo", new JArray{"id DESC"}
                },
                {
                    "fields",new JArray{ "id", "name","type"}
                }
            };
            requests.Add(new
            {
                method = "GET",
                path = "/mcm/api/module?filter=" + JsonConvert.SerializeObject(paramParent)
            });
            //获取所有子模块
            JObject param = new JObject
            {
                {"include","parentPointer" },
                {"where", new JObject{{"parent",new JObject{{ "ne",null }}}}},
                {"fields",new JArray{ "id", "enabled", "name","type", "parent", "pagePath", "pageEnNo", "pageNo"}},
                {"includeFilter",new JObject{{ "module",new JObject{{ "fields", new JArray { "id", "name"}}}}}}
            };
            requests.Add(new
            {
                method = "GET",
                path = "/mcm/api/module?filter=" + JsonConvert.SerializeObject(param)

            });
            JArray ret = ComCloud.ajaxRequest(requests, UserInfo.Token);
            return ret;
        }

        /// <summary>
        /// 获取所有父模块
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public JArray GetModuleType(UserInfo UserInfo) {
            var requests = new List<Object>();
            //获取所有父模块
            JObject paramParent = new JObject
            {
                {
                    "where", new JObject
                    {
                        { "parent",null }
                    }
                },
                {
                    "pageNo", new JArray{"id DESC"}
                },
                {
                    "fields",new JArray{ "id", "name","type","pageEnNo", "pageNo" }
                }
            };
            requests.Add(new
            {
                method = "GET",
                path = "/mcm/api/module?filter=" + JsonConvert.SerializeObject(paramParent)
            });
            JArray ret = ComCloud.ajaxRequest(requests,UserInfo.Token);
            return ret;
        }

        /// <summary>
        /// 根据Id获取模块
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public JArray GetModuleById(string moduleId,UserInfo UserInfo)
        {
            var requests = new List<Object>();
            JObject param = new JObject {
                {
                    "where", new JObject
                    {
                        { "id",moduleId }
                    }
                },
                {
                    "fields",new JArray{"id", "name","type", "parent","pageEnNo", "pageNo","enabled" }
                }
            };
            requests.Add(new
            {
                method = "GET",
                path = "/mcm/api/module?filter=" + JsonConvert.SerializeObject(param),
            });
            JArray ret = ComCloud.ajaxRequest(requests,UserInfo.Token);
            return ret;
        }
    }
}
