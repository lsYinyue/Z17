using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Z17.Core.Base;

namespace Z17.Core.Entities
{
    /// <summary>
    /// 系统角色表
    /// </summary>
    [Description("系统角色表"), Table("TS_ROLE")]
    [Serializable]
    public class TsRole : BaseEntity<TsRole, string>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Display(Name = "角色名称"), Column("C_ROLENAME")]
        public virtual string CRoleName
        {
            get;
            set;
        }
        /// <summary>
        /// 角色描述
        /// </summary>
        [Display(Name = "角色描述"), Column("C_DESCRIPTION")]
        public virtual string CDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 管理者
        /// </summary>
        [Display(Name = "管理者"), Column("C_MASTER")]
        public virtual string CMaster
        {
            get;
            set;
        }
        /// <summary>
        /// 时间戳
        /// </summary>
        [Display(Name = "时间戳"), Column("C_TIMESTAMP")]
        public virtual DateTime? TimeStamp
        {
            get;
            set;
        }
        /// <summary>
        /// N禁用Y启用
        /// </summary>
        [Display(Name = "N禁用Y启用"), Column("C_ENABLE")]
        public virtual string CEnable
        {
            get;
            set;
        }
    }
}
