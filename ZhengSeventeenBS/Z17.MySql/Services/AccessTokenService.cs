using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z17.MySql.Base;
using Z17.MySql.Dtos;
using Z17.MySql.Enums;

namespace Z17.MySql.Services
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
            var userFromToken = BoneAuthService.Proxy.GetUserFromToken(token);
            if (userFromToken == null)
            {
                throw new Exception("登录信息不正确，请重新登录！可能是以下原因：\n1、非法或者伪造的登录信息\n2、登录信息已经过期\n3、当前登录用户已经在其他机器登录");
            }
            var boneIdentity = new BoneIdentity();
            //设置信息
            boneIdentity.UserName = userFromToken.Id;
            boneIdentity.UserName = userFromToken.CUserName;
            boneIdentity.Company = userFromToken.CCompany;
            boneIdentity.Department = userFromToken.CDepartment;
            return boneIdentity;
        }

        /// <summary>
        /// 登录
        /// </summary>
        public string Login(string userid, string password)
        {
            var token = BoneAuthService.Proxy.Token(userid, password);
            return token;
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
