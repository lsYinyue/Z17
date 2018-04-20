using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections;
using APICloud.Rest;

namespace Service.Library
{
    public class ComCloud
    {
        /// <summary>
        /// 通过Batch方式操作数据库
        /// </summary>
        public static JArray ajaxRequest(List<object> requests, string Token)
        {
            var resouce = new Resource("A6078609798966", "2B20CCDD-D161-1B04-C674-F8BC736CCB78");
            try
            {
                JArray ret = JArray.Parse(resouce.Batch(requests));
                return ret;
            }
            catch
            {
                return null;
            }

        }
        public static JObject callBackData(JArray ret)
        {  
            if (ret == null)
            {
                return new JObject { { "code", "0" } };
            }
            return new JObject{ { "code", "1" } };
        }
    }
}