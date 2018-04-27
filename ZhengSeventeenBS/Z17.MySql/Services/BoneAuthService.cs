using System;
using System.Linq;
using LinqToDB;
using Z17.MySql.Base;
using Z17.MySql.Dtos;
using Z17.MySql.Entities;
using Z17.MySql.Extensions;
using Z17.MySql.Helpers;

namespace Z17.MySql.Services
{
    /// <summary>
	/// 系统登录认证服务
	/// </summary> 
    public class BoneAuthService : BaseService<BoneAuthService>
    {
        /// <summary>
		/// 系统默认密码
		/// </summary>
		public const string DEFAULT_PASSWORD = "123456";
        /// <summary>
        /// token缓存键前缀
        /// </summary>
        public const string TOKEN_CACHE_PREFIX = "AccessToken:";
        /// <summary>
        /// 检查用户是否已经登录过
        /// </summary> 
        //[AllowAnonymous]
        public virtual BoneIdentity CheckOnlyOneClientLogined(string userid)
        {
            using (var db = new Db())
            {
                var result = db.GetTable<TsUser>()
                    .Where(x => x.Id.Equals(userid) && !string.IsNullOrEmpty(x.CLoginedIp) && x.COnlyOneClient.IsTrue())
                    .Select(x => new BoneIdentity
                    {
                        Session = x.CSessionId,
                        LoginIp = x.CLoginedIp,
                        LoginMachine = x.CLoginedMachine,
                        LoginTime = x.DLoginedTime,
                        SessionUpdateTime = x.DSessionUpdateTime
                    })
                    .FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// 使用用户名和密码获取Token
        /// </summary> 
        //[AllowAnonymous]
        public virtual string Token(string userid, string password)
        {
            using (var db = new Db())
            {
                var tsUser = db.GetTable<TsUser>().FirstOrDefault(x => x.Id.Equals(userid));
                if (tsUser == null)
                {
                    throw new Exception("用户账户不存在，请检查！");
                }

                string b = SimpleCipherHelper.Instance.MD5EncryptWithSalt(password, "lis");
                if (tsUser.CPassword != b)
                {
                    throw new Exception("密码错误，请检查！");
                }

                if (!tsUser.CEnable.IsTrue())
                {
                    throw new Exception("用户已禁用，请联系管理员！");
                }

                //session 为空或超时情况下重新设定session
                bool flag5 = (string.IsNullOrEmpty(tsUser.CSessionId) || tsUser.COnlyOneClient.IsTrue() || tsUser.IsSessionTimeout(720));
                if (flag5)
                {
                    //获取SessionId
                    tsUser.CSessionId = Guid.NewGuid().ToString("N").ToUpper();
                }

                UpdateWhileLogin(tsUser.Id, tsUser.CSessionId);

                return tsUser.CSessionId;
            }
        }

        //[AllowAnonymous]
        public virtual TsUser GetUserFromToken(string token)
        {
            using (var db = new Db())
            {
                var result = db.GetTable<TsUser>()
                    .FirstOrDefault(x => x.CSessionId.Equals(token));
                return result;
            }
        }
        /// <summary>
        /// 登录后更新用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="token"></param>
        private void UpdateWhileLogin(string userid, string token)
        {
            using (var db = new Db())
            {
                db.GetTable<TsUser>()
                .Where(x => x.Id.Equals(userid))
                .Set(x => x.DLoginedTime, DateTime.Now)
                .Set(x => x.DSessionUpdateTime, DateTime.Now)
                .Set(x => x.CSessionId, token)
                .Update();
            }
        }

        //[AllowAnonymous, ProhibitExternalAccess]
        public virtual void UpdateUserSession(string userid)
        {
            using (var db = new Db())
            {
                db.GetTable<TsUser>()
                .Where(x => x.Id.Equals(userid))
                .Set(x => x.DSessionUpdateTime, DateTime.Now)
                .Update();
            }
        }

        //[AllowAnonymous, ProhibitExternalAccess]
        public virtual void ClearNotValidateSession()
        {
            DateTime? value = null;
            using (var db = new Db())
            {
                db.GetTable<TsUser>()
                .Where(x => x.DSessionUpdateTime < DateTime.Now.AddHours(-8) || string.IsNullOrEmpty(x.CSessionId))
                .Set(x => x.CSessionId, string.Empty)
                .Set(x => x.CLoginedIp, string.Empty)
                .Set(x => x.CLoginedMachine, string.Empty)
                .Set(x => x.DLoginedTime, value)
                .Set(x => x.DSessionUpdateTime, value)
                .Update();
            }
        }

        public virtual void ClearUserLoginInfo(string userid)
        {
            using (var db = new Db())
            {
                db.GetTable<TsUser>()
                .Where(x => x.Id.Equals(userid))
                .Set(x => x.CLoginedIp, string.Empty)
                .Set(x => x.CLoginedMachine, string.Empty)
                .Update();
            }
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary> 
        //public virtual BoneIdentity GetUserInfo()
        //{
        //    return AppContext.Current.User as BoneIdentity;
        //}

        /// <summary>
        /// 修改密码
        /// </summary> 
        public virtual void ModifyPassword(string userid, string oldpwd, string newpwd)
        {
            using (var db = new Db())
            {
                var tsUser = db.GetTable<TsUser>()
                .FirstOrDefault(x => x.Id.Equals(userid));
                string b = SimpleCipherHelper.Instance.MD5EncryptWithSalt(oldpwd, "lis");
                if (tsUser == null || tsUser.CPassword != b)
                {
                    throw new Exception("用户名或者原始密码验证错误，请检查！");
                }

                tsUser.CPassword = SimpleCipherHelper.Instance.MD5EncryptWithSalt(newpwd, "lis");
                db.Update(tsUser);
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary> 
        public virtual void ResetPassword(string userid)
        {
            using (var db = new Db())
            {
                var tsUser = db.GetTable<TsUser>()
                .FirstOrDefault(x => x.Id.Equals(userid));
                if (tsUser == null)
                {
                    throw new Exception("用户名或者原始密码验证错误，请检查！");
                }
                tsUser.CPassword = SimpleCipherHelper.Instance.MD5EncryptWithSalt("123456", "lis");
                db.Update(tsUser);
            }
        }
    }
}
