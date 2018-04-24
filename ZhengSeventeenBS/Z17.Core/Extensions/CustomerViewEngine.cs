using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Z17.Core.Extensions
{
    /// <summary>
    /// razor视图引擎扩展
    /// </summary>
    public class CustomerViewEngine : RazorViewEngine
    {
        /// <summary>
        /// 可以分开部署不同语种
        /// </summary>
        /// <param name="engineName"></param>
        public CustomerViewEngine(string engineName)
        {
            base.ViewLocationFormats = new[]
                {
                    "~/Views/Themes/" + engineName + "/{1}/{0}.cshtml",
                    "~/Views/Themes/" + engineName + "/Shared/{0}.cshtml"
                };

            base.PartialViewLocationFormats = new[]
                {
                    "~/Views/Themes/" + engineName + "/{1}/{0}.cshtml",
                    "~/Views/Themes/" + engineName + "/Shared/{0}.cshtml"
                };

            base.AreaViewLocationFormats = new[]
                {
                    "~Areas/{2}/Views/Themes/" + engineName + "/{1}/{0}.cshtml",
                    "~Areas/{2}/Views/Themes/" + engineName + "/Shared/{0}.cshtml"
                };

            base.AreaPartialViewLocationFormats = new[]
                {
                    "~Areas/{2}/Views/Themes/" + engineName + "/{1}/{0}.cshtml",
                    "~Areas/{2}/Views/Themes/" + engineName + "/Shared/{0}.cshtml"
                };
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            this.SetEngine(controllerContext);
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            this.SetEngine(controllerContext);
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        /// <summary>
        /// 根据条件自行设置,目前是chrome浏览器就展示默认的
        /// 不是chrome浏览器的话就展示/Themes/lis下的
        /// 可以直接测试是移动端还是pc端
        /// 然后写入cookie
        /// </summary>
        private void SetEngine(ControllerContext controllerContext)
        {
            string engineName = "/Themes/lis";
            if (controllerContext.HttpContext.Request.UserAgent.IndexOf("Chrome/55.0.2883.75") >= 0)
            {
                engineName = null;
            }

            if (controllerContext.HttpContext.Request.IsMobile())
            {
                engineName = null;
            }

            base.ViewLocationFormats = new[]
               {
                    "~/Views" + engineName + "/{1}/{0}.cshtml",
                    "~/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.PartialViewLocationFormats = new[]
                {
                    "~/Views" + engineName + "/{1}/{0}.cshtml",
                    "~/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.AreaViewLocationFormats = new[]
                {
                    "~Areas/{2}/Views" + engineName + "/{1}/{0}.cshtml",
                    "~Areas/{2}/Views" + engineName + "/Shared/{0}.cshtml"
                };

            base.AreaPartialViewLocationFormats = new[]
                {
                    "~Areas/{2}/Views" + engineName + "/{1}/{0}.cshtml",
                    "~Areas/{2}/Views" + engineName + "/Shared/{0}.cshtml"
                };
        }
    }
}
