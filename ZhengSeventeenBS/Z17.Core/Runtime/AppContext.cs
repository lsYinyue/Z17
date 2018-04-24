using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using Castle.Core.Logging;

namespace Z17.Core.Runtime
{
    /// <summary>
    /// 应用程序上下文信息
    /// </summary>
    [Serializable]
    public class AppContext
    {
        private string _local_token;
        private IBoneIdentity _local_user;
        private static AppContext _instance;
        private static ILogger _logger = Logging.LoggerManager.CreateLogger<AppContext>();

        /// <summary>
        /// 当前上下文实例
        /// </summary>
        public static AppContext Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppContext();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public IBoneIdentity User
        {
            get
            {
                return string.IsNullOrEmpty(this.Request.Id) ? this._local_user : this.Request.User;
            }
            set
            {
                this._local_user = value;
            }
        }

        /// <summary>
        /// 当前服务请求上下文(客户端程序不要使用该属性)
        /// </summary>
        public BoneContextRequest Request { get; }

        /// <summary>
        /// 请求令牌信息
        /// </summary>
        public string Token
        {
            get
            {
                return string.IsNullOrEmpty(this.Request.Id) ? this._local_token : this.Request.Token;
            }
            set
            {
                this._local_user = null;
                this._local_token = value;
            }
        }

        /// <summary>
        /// bin目录地址
        /// </summary>
        public static string BaseDirectory
        {
            get
            {
                return HttpRuntime.BinDirectory;
            }
        }

        public AppContext()
        {
            Request = new BoneContextRequest();
        }
    }
}
