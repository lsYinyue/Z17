using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Z17.Core.Base;

namespace Z17.Core.Helpers
{
    /// <summary>
    /// 处理网络连接等问题帮助类
    /// </summary>
    public class NetWorkHelper : BoneHelper<NetWorkHelper>
    {
        private readonly ManualResetEvent TimeoutObject = new ManualResetEvent(false);
        /// <summary>
        /// 测试服务器是否能够PING通
        /// </summary> 
        public bool TestPing(string host, int timeout = 300)
        {
            var options = new PingOptions { DontFragment = true };
            var pingReply = new Ping().Send(host, 300, Encoding.ASCII.GetBytes("a"), options);
            return pingReply != null && pingReply.Status == IPStatus.Success;
        }
        /// <summary>
        /// 测试是否能正常连接到服务器制定端口
        /// </summary> 
        public bool TestSocket(string host, int port, int timeout = 300)
        {
            TimeoutObject.Reset();
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(host, port, x => TimeoutObject.Set(), socket);
            return TimeoutObject.WaitOne(timeout, false);
        }
        /// <summary>
        /// 获取本机IPV4地址
        /// </summary>  
        public string GetLocalIpv4()
        {
            var hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            var iPAddress = hostAddresses.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            return iPAddress?.ToString();
        }

        /// <summary>
        /// 测试服务器是否通讯正常
        /// </summary>
        /// <param name="urlstring">url地址</param>
        /// <param name="throwexception">是否抛出异常</param>
        /// <returns>true正常，false失败</returns>
        public bool TestConnectServer(string urlstring, bool throwexception = true)
        {
            if (string.IsNullOrEmpty(urlstring))
            {
                return false;
            }

            var uri = new Uri(urlstring);
            bool flag2 = TestSocket(uri.Host, uri.Port, 300);
            bool flag3 = !flag2 & throwexception;
            if (flag3)
            {
                string message = string.Format("无法连接服务器，网络故障，请检查后重试！\n服务地址：{0}\n本机地址：{1}", uri, GetLocalIpv4());
                throw new Exception(message);
            }
            return true;
        }
        /// <summary>
        /// 获取机器mac地址
        /// </summary> 
        public string GetMacAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces().First(x => x.OperationalStatus == OperationalStatus.Up).GetPhysicalAddress().ToString();
        }
    }
}
