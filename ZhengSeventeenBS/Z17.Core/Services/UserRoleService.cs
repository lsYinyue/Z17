using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Z17.Core.Base;
using Z17.Core.Entities;

namespace Z17.Core.Services
{
    public class UserRoleService : BoneService<UserRoleService>
    {
        /// <summary>
        /// 添加或修改角色信息
        /// </summary>
        /// <param name="roles"></param>
        public virtual void InsertOrUpdateRole(List<TsRole> roles)
        {
            roles.ForEach(role => GetDbContext().InsertOrReplace(role));
        }

        /// <summary>
        /// 验证并删除角色
        /// </summary>
        /// <param name="role">角色信息</param>
        public virtual void DeleteRole(TsRole role)
        {
            PermissionService.Proxy.CheckBeforeRemoveRole(role.Id);
            GetDbContext().GetTable<TsRole>().Delete(x => x.Id == role.Id);
        }
    }
}
