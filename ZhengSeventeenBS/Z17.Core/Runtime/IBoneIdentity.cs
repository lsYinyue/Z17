using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Z17.Core.Enums;

namespace Z17.Core.Runtime
{
    public interface IBoneIdentity : IIdentity, ILoginedInfo
    {
        /// <summary>
		/// 用户标识
		/// </summary>
		string UserID { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 用户类型
        /// </summary>
        UserType UserType { get; }

        /// <summary>
        /// 用户密码
        /// </summary>
        string Password { get; }

        /// <summary>
        /// 用户所属部门
        /// </summary>
        string Department { get; }

        /// <summary>
        /// 用户所属部门名称
        /// </summary>
        string DepartmentDesc { get; }
    }
}
