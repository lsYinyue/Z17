using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z17.Core.Base;
using Z17.Core.Dtos;

namespace Z17.Core.Services
{
    public class AccessTokenService : BaseService<AccessTokenService> //IAccessTokenService
    {
        /// <summary>
        /// 从token中获取用户
        /// </summary>  
        public BoneIdentity GetUserFromAccessToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("需要授权：Token不允许为空值");
            }

            token = Guid.Parse(token).ToString("N").ToUpper();
            //string key = "AccessToken:" + token;
            //int timeoutOfLogin = AppContext.Current.Settings().TimeoutOfLogin;
            //var boneIdentity = CacheManager.MemoryCache.Get(key, null, 7200) as BoneIdentity;
            //if (boneIdentity == null)
            //{
            //    var userFromToken = BoneAuthService.Proxy.GetUserFromToken(token);
            //    if (userFromToken == null)
            //    {
            //        throw new Exception(string.Format("登陆信息不正确，请重新登陆！可能是以下原因：\n1、非法或者伪造的登陆信息\n2、登陆信息已经过期（{0}分钟）\n3、当前登陆信息已经在其他机器登陆", timeoutOfLogin));
            //    }
            //    boneIdentity = userFromToken.AsIdentity();
            //    boneIdentity.SessionUpdateTime = new DateTime?(DateTime.Now);
            //    BoneAuthService.Proxy.UpdateUserSession(boneIdentity.UserID);
            //    //CacheManager.GetCache().Set(key, boneIdentity, 300);
            //}

            //if (boneIdentity.OnlyOneClient)
            //{
            //    bool flag4 = boneIdentity.LoginIp != AppContext.Current.Request.UserClient.IpAddress && boneIdentity.LoginMachine != AppContext.Current.Request.UserClient.HostName;
            //    if (flag4)
            //    {
            //        CacheManager.GetCache().Remove(key);
            //        throw new AuthenticationException("该用户已经在其他地方进行独占式登陆，请与对方协商后重新登陆！");
            //    }
            //}
            //return boneIdentity;

            return new BoneIdentity();
        }

        /// <summary>
        /// 登录
        /// </summary>
        public void Login(string userid, string password)
        {
            //AppContext.Current.Token = BoneAuthService.Proxy.Token(userid, password);
            //AppContext.Current.User = GetUserFromAccessToken(AppContext.Current.Token);
            //BoneAuthService.Proxy.ClearNotValidateSession();

            var token = BoneAuthService.Proxy.Token(userid, password);
            Console.WriteLine("token = " + token);

            var user = GetUserFromAccessToken(token);
            BoneAuthService.Proxy.ClearNotValidateSession();
        }

        /// <summary>
        /// 登出
        /// </summary>  
        public void Logout()
        {
            //if (AppContext.Current.User != null)
            //{
            //    try
            //    {
            //        BoneAuthService.Proxy.ClearUserLoginInfo(AppContext.Current.User.UserID);
            //    }
            //    catch
            //    {
            //    }
            //}
            //CallContext.HostContext = null;
        }
    }
}
