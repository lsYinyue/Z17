using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.Core.Logging;

namespace Z17.Core.IOC
{
    /// <summary>
    /// 依赖注入管理器
    /// </summary>
    public class IocManager
    {
        private static ILogger _log = Logging.LoggerManager.CreateLogger<IocManager>();

        private static IocManager _ioc;
        public static IocManager Instance
        {
            get
            {
                if (_ioc == null)
                {
                    _log.Info("_ioc == null");
                    _ioc = new IocManager();
                }

                if (_ioc != null)
                {
                    _log.Info("_ioc != null");
                }
                return _ioc;
            }
        }

        private ContainerBuilder Builder { get; set; }
        public IContainer Container { get; private set; }

        public IocManager()
        {
            if (Builder == null)
            {
                Builder = new ContainerBuilder();
            }
        }

        public void BuildContainer()
        {
            this.Container = Builder.Build();
            _log.Info("Container创建完成");
        }

        public bool IsRegistered<T>()
        {
            return Container.IsRegistered(typeof(T));
        }

        public bool IsRegistered(Type type)
        {
            return Container.IsRegistered(type);
        }

        public void Register<TType>() where TType : class
        {
            Builder.RegisterType<TType>();
        }

        public void Register<TType, TInterface>() where TType : class, TInterface where TInterface : class
        {
            _log.Info("开始Register 类 " + typeof(TType).FullName + " 接口 " + typeof(TInterface).FullName);
            Builder.RegisterType<TType>().As<TInterface>();
            _log.Info("Register完成 类 " + typeof(TType).FullName + " 接口 " + typeof(TInterface).FullName);
        }

        public void RegisterNamed<TType, TInterface>(string name) where TType : class, TInterface where TInterface : class
        {
            Builder.RegisterType<TType>().Named<TInterface>(name);
        }

        public void RegisterInterceptor<TType, TInterface, TInterceptor>() where TType : class, TInterface where TInterface : class where TInterceptor : Castle.DynamicProxy.IInterceptor
        {
            Builder.RegisterType<TInterceptor>();//注册拦截器
            Builder.RegisterType<TType>().As<TInterface>().InterceptedBy(typeof(TInterceptor)).EnableInterfaceInterceptors();//注册TType并为其添加拦截器
        }

        public TInterface Resolve<TInterface>() where TInterface : class
        {
            var name = typeof(TInterface).FullName;
            _log.Info(name + " 开始Resolve");
            try
            {
                var result = Container.Resolve<TInterface>();
                _log.Info(name + " Resolve成功");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(name + " Resolve失败", ex);
                throw ex;
            }
        }

        public TInterface ResolveNamed<TInterface>(string name) where TInterface : class
        {
            return Container.ResolveNamed<TInterface>(name);
        }
    }
}
