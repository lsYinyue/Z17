using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using Z17.Core.Base;
using Z17.Core.Dtos;
using Z17.Core.Entities;
using Z17.Core.Enums;
using Z17.Core.Extensions;

namespace Z17.Core.Services
{
    /// <summary>
    /// 系统授权服务
    /// </summary>
    [Serializable]
    public class PermissionService : BaseService<PermissionService>
    {
        /// <summary>
		/// 获取用户资源
		/// </summary> 
		public virtual List<TsResource> GetUserResource(string userId, RbacResourceType rbacRscType)
        {
            using (var db = GetDbContext())
            {
                var tsUser = db.GetTable<TsUser>()
                    .FirstOrDefault(x => x.Id.Equals(userId));
                var result = new List<TsResource>();
                if (tsUser.CUserType == 9)
                {
                    result = db.GetTable<TsResource>()
                        .Where(x => x.CType.Equals((int)rbacRscType))
                        .ToList();
                }
                else
                {
                    var roles = GetUserRoles(userId);
                    var resourceIds = db.GetTable<TsRolePermission>()
                        .Where(x => roles.Contains(x.CRoleId) && x.CResourceType.Equals((int)rbacRscType))
                        .Select(x => x.CResourceId)
                        .ToList();
                    result = db.GetTable<TsResource>()
                        .Where(x => resourceIds.Contains(x.Id))
                        .OrderBy(x => x.COrder)
                        .ToList();
                }
                return result;
            }
        }

        public virtual List<MenuItemDto> GetUserMenuItems(string userId)
        {
            using (var db = GetDbContext())
            {
                var tsUser = db.GetTable<TsUser>()
                    .FirstOrDefault(x => x.Id.Equals(userId));

                var result = new List<MenuItemDto>();
                if (tsUser.CUserType == 9)
                {
                    result = db.GetTable<TsResource>()
                        .Where(x => x.CType.Equals(2))
                        .Select(x => MenuItemDto.Map(x))
                        .ToList();
                }
                else
                {
                    var roles = GetUserRoles(userId);
                    var resourceIds = db.GetTable<TsRolePermission>()
                        .Where(x => roles.Contains(x.CRoleId) && (x.CResourceType.Equals(2) || x.CResourceType.Equals(64)))
                        .Select(x => x.CResourceId)
                        .ToList();
                    result = db.GetTable<TsResource>()
                        .Where(x => resourceIds.Contains(x.Id))
                        .OrderBy(x => x.COrder)
                        .Select(x => MenuItemDto.Map(x))
                        .ToList();
                }
                return result;
            }
        }

        public virtual List<TsResource> GetNotAllowedWidgets(string userId, string rscId)
        {
            using (var db = GetDbContext())
            {
                var source = db.GetTable<TsResource>()
                    .Where(x => x.CPId.Equals(rscId) && x.CType.Equals(4));
                if (source.Count() == 0)
                {
                    return new List<TsResource>();
                }

                var tsUser = db.GetTable<TsUser>()
                    .FirstOrDefault(x => x.Id.Equals(userId));
                if (tsUser != null && tsUser.CUserType == 9)
                {
                    return new List<TsResource>();
                }

                var roles = GetUserRoles(userId);
                var rscs = from rsc in db.GetTable<TsResource>()
                           join permission in db.GetTable<TsRolePermission>()
                           on rsc.Id equals permission.CResourceId
                           where rsc.CType.Equals(4) && rsc.CPId.Equals(rscId)
                               && roles.Contains(permission.CRoleId)
                           select rsc;
                var allowedList = rscs.ToList();
                var result = source.Where(x => allowedList.Any(w => w.Id == x.Id)).ToList();
                return result;
            }
        }

        /// <summary>
        /// 获取角色资源
        /// </summary> 
        public virtual List<TsResource> GetRoleResource(string roleId, params RbacResourceType[] rbacRscTypes)
        {
            using (var db = GetDbContext())
            {
                var ints = rbacRscTypes.Select(x => (int)x).ToArray();
                var resourceIds = db.GetTable<TsRolePermission>()
                    .Where(x => x.CRoleId.Equals(roleId) && ints.Contains(x.CResourceType))
                    .Select(x => x.CResourceId)
                    .ToList();
                //var queryable = GetDbContext().GetTable<TsResource>().Where(x => ints.Contains(x.CType));
                var result = db.GetTable<TsResource>()
                    .Where(x => resourceIds.Contains(x.Id))
                    .OrderBy(x => x.COrder)
                    .ToList();
                return result;
            }
        }

        /// <summary>
        /// 查询所有的用户角色对应关系
        /// </summary>
        /// <returns></returns>
        public virtual List<TsUserRole> GetUserRoles()
        {
            using (var db = GetDbContext())
            {
                return db.GetTable<TsUserRole>().ToList();
            }
        }

        /// <summary>
        /// 获取用户角色表
        /// </summary> 
        public virtual List<string> GetUserRoles(string userId)
        {
            using (var db = GetDbContext())
            {
                var result = db.GetTable<TsUserRole>()
                    .Where(x => x.CUserId.Equals(userId))
                    .Select(x => x.CRoleId)
                    .ToList();
                return result;
            }
        }

        /// <summary>
        /// 按分类获取所有资源
        /// </summary> 
        public virtual List<TsResource> GetAllResouces(params RbacResourceType[] rbacRscTypes)
        {
            using (var db = GetDbContext())
            {
                var ints = rbacRscTypes.Select(x => (int)x).ToArray();
                var result = db.GetTable<TsResource>()
                    .Where(x => ints.Contains(x.CType))
                    .OrderBy(x => x.COrder)
                    .ToList();
                return result;
            }
        }

        public virtual List<TsUser> QueryUsers(string keywords)
        {
            using (var db = GetDbContext())
            {
                var result = db.GetTable<TsUser>()
                    .Where(x => !x.Id.Equals("system"))
                    //.WhereIf(AppContext.Current.User.UserType != UserType.Administrator, x => x.CMaster.Equals(AppContext.Current.User.UserID))
                    .WhereIf(!string.IsNullOrEmpty(keywords), x => x.Id.Contains(keywords) || x.CUserName.Contains(keywords))
                    .OrderBy(x => x.Id)
                    .ToList();
                return result;
            }
        }

        public virtual TsUser QueryUserById(string userId)
        {
            using (var db = GetDbContext())
            {
                var result = db.GetTable<TsUser>().FirstOrDefault(x => x.Id.Equals(userId));
                return result;
            }
        }

        public virtual void DeleteUser(string userid)
        {
            using (var db = GetDbContext())
            {
                var user = db.GetTable<TsUser>().FirstOrDefault(x => x.Id.Equals(userid));
                if (user == null)
                {
                    throw new Exception("不存在的用户");
                }

                db.GetTable<TsUser>().Delete(x => x.Id.Equals(userid) || x.CMaster.Equals(userid));
            }
        }

        public virtual TsUser InsertOrUpdateUser(TsUser user, List<string> roleids)
        {
            using (var db = GetDbContext())
            {
                db.InsertOrReplace(user);

                db.GetTable<TsUserRole>()
                    .Delete(x => x.CUserId.Equals(user.Id));

                if (roleids != null && roleids.Count > 0)
                {
                    db.BulkCopy(roleids.Select(x => new TsUserRole
                    {
                        Id = SequenceService.Proxy.GenerateLocalId(),
                        CUserId = user.Id,
                        CRoleId = x
                    }));
                }
                return user;
            }
        }

        public virtual List<TsRole> QueryRoles(string keywords)
        {
            using (var db = GetDbContext())
            {
                var result = db.GetTable<TsRole>()
                //.WhereIf(AppContext.Current.User.UserType == UserType.Admin, x => x.CMaster.Equals(AppContext.Current.User.UserID))
                .WhereIf(!string.IsNullOrEmpty(keywords), x => x.CRoleName.Contains(keywords) || x.CDescription.Contains(keywords))
                .OrderBy(x => x.CRoleName)
                .ToList();
                return result;
            }
        }

        public virtual void CheckBeforeRemoveRole(string roleId)
        {
            using (var db = GetDbContext())
            {
                var num = db.GetTable<TsUserRole>().Where(x => x.CRoleId.Equals(roleId)).Count();
                if (num > 0)
                {
                    throw new Exception("目前有 " + num + "人正在使用该角色，您不能删除该角色！");
                }
            }
        }

        //public virtual List<TsDepartment> QueryAllDepartments()
        //{
        //    return GetDbContext().GetTable<TsDepartment>().ToList();
        //}

        public virtual List<TsResource> QueryFunctionByModule(string moduleId)
        {
            using (var db = GetDbContext())
            {
                var result = db.GetTable<TsResource>()
                .Where(x => x.CPId.Equals(moduleId) && x.CType.Equals(4))
                .ToList();
                return result;
            }
        }

        public virtual void SaveChangesForRoleModule(string rolid, List<TsResource> selectResource)
        {
            using (var db = GetDbContext())
            {
                var role = db.GetTable<TsRole>().FirstOrDefault(x => x.Id.Equals(rolid));
                if (role == null)
                {
                    throw new Exception("不存在的角色：" + role.Id);
                }

                var ints = new int[] { 2, 64, 4 };
                var arg_1AA_0 = db.GetTable<TsRolePermission>()
                    .Where(x => x.CRoleId.Equals(rolid) && ints.Contains(x.CResourceType))
                    .Delete();
                var lst = selectResource.Select(x => new TsRolePermission
                {
                    Id = SequenceService.Proxy.GenerateLocalId(),
                    CRoleId = rolid,
                    CResourceId = x.Id,
                    CResourceType = x.CType
                }).ToList();
                db.BulkCopy(lst);
            }
        }

        private List<string> PermissionToDataItem(IList<TsRolePermission> lst)
        {
            if (!lst.Any())
            {
                return new List<string>();
            }

            return lst.Select(x => x.CResourceId.Substring(x.CResourceId.IndexOf("##") + 2)).ToList();
        }

        /// <summary>
        /// 查找用户对应的键值对权限
        /// </summary> 
        public virtual List<string> QueryUserKeyValues(string userId, string kvParentCode)
        {
            using (var db = GetDbContext())
            {
                var tsKeyValue = db.GetTable<TsKeyValue>()
                .FirstOrDefault(x => x.CCode.Equals(kvParentCode) && x.CEnable.IsTrue() && string.IsNullOrEmpty(x.CPCode));
                if (tsKeyValue == null)
                {
                    throw new Exception("不存在的kvParentCode，请核实");
                }

                return QueryUserRestrictData(userId, tsKeyValue.Id);
            }
        }

        /// <summary>
        /// 查找用户对应键的数据权限
        /// </summary> 
        public virtual List<string> QueryUserRestrictData(string userId, string dataKey)
        {
            using (var db = GetDbContext())
            {
                var resource = db.GetTable<TsResource>()
                .FirstOrDefault(x => x.CType.Equals(16) && x.CCode.Equals(dataKey));
                if (resource == null)
                {
                    throw new Exception("不存在的dataKey：" + dataKey);
                }

                var roles = GetUserRoles(userId);
                var lst = db.GetTable<TsRolePermission>()
                    .Where(x => roles.Contains(x.CRoleId) && x.CResourceType.Equals(16) && x.CResourceId.StartsWith(resource.Id))
                    .ToList();
                return PermissionToDataItem(lst);
            }
        }

        /// <summary>
        /// 查询受限数据的表名和字段名（含键值对）
        /// </summary> 
        //public virtual List<RestrictDataResource> QueryDataGroups()
        //{
        //    var first = CodeGenerationService.Proxy.GetDatabaseSchema()
        //        .Select(x => new RestrictDataResource
        //        {
        //            TableName = x.TableName.ToUpper(),
        //            Description = string.Format("[{0}] {1}", x.TableName.ToUpper(), x.Description),
        //            Columns = x.Columns.Select(w => new RestrictDataResourceItem { ColumnName = w.ColumnName, Description = w.Description }).ToList()
        //        });
        //    var second = GetDbContext().GetTable<TsKeyValue>()
        //        .Where(x => x.CEnable.Equals(YN.Y.ToString()) && string.IsNullOrEmpty(x.CPCode))
        //        .Select(x => new RestrictDataResource
        //        {
        //            TableName = "KV_" + x.Id,
        //            Description = string.Format("[KV_{0}] {1}", x.CCode, x.CName),
        //            Columns = new List<RestrictDataResourceItem>
        //            {
        //                new RestrictDataResourceItem { ColumnName= "Code",Description="编码" },
        //                new RestrictDataResourceItem { ColumnName= "Name",Description="名称" }
        //            }
        //        });
        //    return first.Concat(second).ToList();
        //}

        /// <summary>
        /// 根据角色查询数据权限
        /// </summary> 
        [Obsolete("Select(x => new Func<TsKeyValue, TreeDto>(x))")]
        public virtual List<TreeDto> QueryRestrictData(TsResource item, string roleId)
        {
            if (item.CType != 16)
            {
                return new List<TreeDto>();
            }

            using (var db = GetDbContext())
            {
                if (item.CCode.StartsWith("KV_"))
                {
                    var kvId = item.CCode.Substring(3);
                    var parentKv = db.GetTable<TsKeyValue>()
                        .FirstOrDefault(x => x.Id.Equals(kvId));
                    var result = db.GetTable<TsKeyValue>()
                          .Where(x => x.CPCode.Equals(parentKv.CCode))
                          .OrderBy(x => x.COrder)
                          .ToList()
                          .Select(x => new TreeDto
                          {
                              Id = x.Id,
                              Name = x.CCode,
                              Tag = x.CName
                          })
                          .ToList();
                    return result;
                }
                else
                {
                    var listRole = db.GetTable<TsRolePermission>()
                           .Where(x => x.CRoleId.Equals(roleId) && x.CResourceId.StartsWith(item.Id) && x.CResourceType.Equals(16))
                           .ToList();
                    string sql = string.Format("select {0} Id,{1} Name ", item.CResourcePath, item.CResourceSubPath) + string.Format("from {0} order by {1}", item.CCode, item.CResourcePath);
                    var result = db.Query<TreeDto>(sql)
                          .Select(x => new TreeDto
                          {
                              Id = x.Id,
                              Name = string.Format("[{0}]-{1}", x.Id, x.Name),
                              Tag = listRole.Any(w => w.CResourceId == string.Format("{0}##{1}", item.Id, x.Id))
                          }).ToList();
                    return result;
                }
            }
        }

        /// <summary>
        /// 根据用户+实体类型名查询列权限
        /// </summary> 
        public virtual List<string> QueryRestrictColumn(string userId, string entityTypeFullName)
        {
            var userRoles = GetUserRoles(userId);
            return QueryRestrictColumn(userRoles, entityTypeFullName);
        }

        /// <summary>
        /// 根据角色集合+实体类型名查询列权限
        /// </summary> 
        public virtual List<string> QueryRestrictColumn(List<string> roleids, string entityTypeFullName)
        {
            using (var db = GetDbContext())
            {
                var lst = db.GetTable<TsRolePermission>()
                .Where(x => roleids.Contains(x.CRoleId) && x.CResourceId.StartsWith(entityTypeFullName) && x.CResourceType.Equals(32))
                .ToList();
                return PermissionToDataItem(lst);
            }

        }

        /// <summary>
        /// 按角色保存提交数据行权限
        /// </summary> 
        public virtual void SaveRoleRestrictDataItem(string roleId, string dataId, string parentId, bool checkedState, RbacResourceType rtype = RbacResourceType.DataItem)
        {
            using (var db = GetDbContext())
            {
                var resourceId = string.Format("{0}##{1}", parentId, dataId);

                var tsRolePermission = db.GetTable<TsRolePermission>()
                    .FirstOrDefault(x => x.CRoleId.Equals(roleId) && x.CResourceId.Equals(resourceId) && x.CResourceType.Equals(rtype));
                if (tsRolePermission == null & checkedState)
                {
                    db.Insert(new TsRolePermission
                    {
                        Id = SequenceService.Proxy.GenerateLocalId(),
                        CResourceId = resourceId,
                        CResourceType = (int)rtype,
                        CRoleId = roleId
                    });
                }

                if (tsRolePermission != null && !checkedState)
                {
                    db.Delete(tsRolePermission);
                }
            }
        }
    }
}
