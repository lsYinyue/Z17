using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Z17.Core.Helpers;

namespace Z17.Core.Extensions
{
    /// <summary>
    /// 自定义扩展json格式result
    /// </summary>
    public class JsonResult : ActionResult
    {
        private object _data = null;

        public JsonResult(object data)
        {
            _data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.Write(JsonHelper.Instance.ToJson(_data));
        }
    }
}
