using System;
using System.Runtime.Remoting.Messaging;
using Z17.Core.Base;
using Z17.Core.Enums;
using Z17.Core.Extensions;
using Z17.Core.Runtime;

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
            string key = "AccessToken:" + token;
            int timeoutOfLogin = 480;// AppContext.Current.Settings().TimeoutOfLogin;
            BoneIdentity boneIdentity = null;
            //boneIdentity = CacheManager.MemoryCache.Get(key, null, 7200) as BoneIdentity;
            //if (boneIdentity == null)
            {
                var userFromToken = BoneAuthService.Proxy.GetUserFromToken(token);
                if (userFromToken == null)
                {
                    throw new Exception(string.Format("登陆信息不正确，请重新登陆！可能是以下原因：\n1、非法或者伪造的登陆信息\n2、登陆信息已经过期（{0}分钟）\n3、当前登陆信息已经在其他机器登陆", timeoutOfLogin));
                }
                boneIdentity = new BoneIdentity
                {
                    UserID = userFromToken.Id,
                    UserName = userFromToken.CUserName,
                    UserType = (UserType)userFromToken.CUserType,
                    Password = userFromToken.CPassword,
                    Department = userFromToken.CDepartment,
                    DepartmentDesc = userFromToken.CDepartment,
                    Session = userFromToken.CSessionId,
                    OnlyOneClient = userFromToken.COnlyOneClient.IsTrue(),
                    LoginIp = userFromToken.CLoginedIp,
                    LoginMachine = userFromToken.CLoginedMachine,
                    SessionUpdateTime = userFromToken.DSessionUpdateTime,
                    LoginTime = userFromToken.DLoginedTime
                };
                boneIdentity.SessionUpdateTime = new DateTime?(DateTime.Now);
                BoneAuthService.Proxy.UpdateUserSession(boneIdentity.UserID);
                //CacheManager.GetCache().Set(key, boneIdentity, 300);
            }

            //if (boneIdentity.OnlyOneClient)
            //{
            //    bool flag4 = boneIdentity.LoginIp != AppContext.Current.Request.UserClient.IpAddress && boneIdentity.LoginMachine != AppContext.Current.Request.UserClient.HostName;
            //    if (flag4)
            //    {
            //        CacheManager.GetCache().Remove(key);
            //        throw new AuthenticationException("该用户已经在其他地方进行独占式登陆，请与对方协商后重新登陆！");
            //    }
            //}

            return boneIdentity;
        }

        /// <summary>
        /// 登录
        /// </summary>
        public void Login(string userid, string password)
        {
            Runtime.AppContext.Current.Token = BoneAuthService.Proxy.Token(userid, password);
            Runtime.AppContext.Current.User = GetUserFromAccessToken(Runtime.AppContext.Current.Token);
            BoneAuthService.Proxy.ClearNotValidateSession();
        }

        /// <summary>
        /// 登出
        /// </summary>  
        public void Logout()
        {
            if (Runtime.AppContext.Current.User != null)
            {
                try
                {
                    BoneAuthService.Proxy.ClearUserLoginInfo(Runtime.AppContext.Current.User.UserID);
                }
                catch
                {
                }
            }
            CallContext.HostContext = null;
        }
    }
}
