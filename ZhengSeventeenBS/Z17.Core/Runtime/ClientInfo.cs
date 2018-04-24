using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Runtime
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class ClientInfo
    {
        private static ClientInfo _me;
        public string IpAddress { get; set; }
        public string HostName { get; set; }
        public string MacAddress { get; set; }
        public string Token { get; set; }
        public static ClientInfo Local
        {
            get
            {
                if (_me == null)
                {
                    _me = new ClientInfo
                    {
                        IpAddress = Helpers.NetWorkHelper.Instance.GetLocalIpv4(),
                        MacAddress = Helpers.NetWorkHelper.Instance.GetMacAddress(),
                        HostName = Dns.GetHostName()
                    };
                }
                _me.Token = AppContext.Current.Token;
                return _me;
            }
        }
    }
}
