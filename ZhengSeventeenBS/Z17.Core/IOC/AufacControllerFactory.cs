using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;

namespace Z17.Core.IOC
{
    /// <summary>
    /// 自定义的控制器实例化工厂
    /// </summary>
    public class AufacControllerFactory : DefaultControllerFactory
    {
        private IContainer Container
        {
            get
            {
                return IocManager.Instance.Container;
            }
        }

        /// <summary>
        /// 创建控制器对象
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (null == controllerType)
            {
                return null;
            }
            IController controller = (IController)Container.Resolve(controllerType);
            return controller;
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            //Container.Release(controller);//释放对象
        }
    }
}
