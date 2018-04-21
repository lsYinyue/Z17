using System;
using System.Configuration;

namespace Z17.Core.Base
{
    public abstract class BaseService
    {

    }

    /// <summary>
    /// Service基类
    /// public方法一定要带 virtual 关键字
    /// 需要添加针对非虚方法的处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("没写代理")]
    public abstract class BaseService<T> : BaseService where T : BaseService, new()
    {
        public BaseService()
        {

        }

        private static T _proxy = null;
        public static T Proxy => _proxy ?? (_proxy = new T());
    }


}
