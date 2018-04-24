using System;
using System.Configuration;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using LinqToDB.Data;
using Z17.Core.Helpers;

namespace Z17.Core.Base
{
    public abstract class BoneService
    {
        private static DataConnection _connection;

        /// <summary>
        /// 获取数据库连接上下文
        /// </summary> 
        protected virtual DataConnection GetDbContext(string connStr = "default")
        {
            if (_connection == null)
            {
                using (var db = new Db(connStr))
                {
                    _connection = db.Connection;
                }
            }
            return _connection;
        }
    }

    /// <summary>
    /// Service基类
    /// public方法一定要带 virtual 关键字
    /// 需要添加针对非虚方法的处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BoneService<T> : BoneService where T : BoneService, new()
    {
        private static readonly ILogger _logger = Logging.LoggerManager.CreateLogger<T>();
        private static readonly ProxyGenerator _factory = new ProxyGenerator();

        public BoneService()
        {

        }

        /// <summary>
        /// 数据门户代理对象
        /// </summary>
        public static T Proxy
        {
            get
            {
                _logger.Info("BoneService " + typeof(T).FullName);
                return (T)_factory.CreateClassProxy(typeof(T), new IInterceptor[]
                {
                    new BoneServiceInterceptor(Activator.CreateInstance<T>())
                });
            }
        }

        protected ILogger Log
        {
            get
            {
                return _logger;
            }
        }
    }

    public class BoneServiceInterceptor : IInterceptor
    {
        private BoneService _service;
        private static readonly ILogger _logger = Logging.LoggerManager.CreateLogger<BoneServiceInterceptor>();

        public BoneServiceInterceptor(BoneService service)
        {
            _logger.Info("service进入拦截器");
            _service = service;
        }

        public void Intercept(IInvocation invocation)
        {
            var method = invocation.Method;
            if (!method.IsVirtual)
            {
                var msg = string.Format("方法【[0]】不是 Virtual", method.Name);
                throw new Exception(msg);
            }

            BeforeIntercept();
            invocation.Proceed();//method.Invoke(_service, invocation.Arguments);
            AfterIntercept();
        }

        /// <summary>
        /// 调用目标方法前要执行的一段代码
        /// </summary>
        public void BeforeIntercept()
        {
            _logger.Info("BeforeIntercept 调用Service方法前");
        }

        /// <summary>
        /// 调用目标方法后要执行的一段代码
        /// </summary>
        public void AfterIntercept()
        {
            _logger.Info("AfterIntercept 调用Service方法后");
        }
    }
}
