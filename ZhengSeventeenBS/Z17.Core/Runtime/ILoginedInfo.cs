using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Runtime
{
    public interface ILoginedInfo
    {
        string Session { get; }

        /// <summary>
        /// 用户登录Ip地址
        /// </summary>
        string LoginIp { get; }

        /// <summary>
        /// 用户登录主机名
        /// </summary>
        string LoginMachine { get; }

        /// <summary>
        /// 用户登录时间
        /// </summary>
        DateTime? LoginTime { get; }

        /// <summary>
        /// 只允许一个客户端登陆
        /// </summary>
        bool OnlyOneClient { get; }

        /// <summary>
        /// 用户登录时间
        /// </summary>
        DateTime? SessionUpdateTime { get; }

    }
}
