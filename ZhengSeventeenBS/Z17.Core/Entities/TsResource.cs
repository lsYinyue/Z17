using System;
using System.Linq;
using LinqToDB.Mapping;
using Z17.Core.Base;

namespace Z17.Core.Entities
{
    [Table("TS_RESOURCE")]
    [Serializable]
    public class TsResource : BoneEntity<TsResource, string>
    {
        /// <summary>
        /// 模块对应的类型
        /// </summary> 
        [NonSerialized]
        public Type ModuleDeclaringType;
        [Column("C_PID")]
        public virtual string CPId
        {
            get;
            set;
        }
        [Column("C_CODE")]
        public virtual string CCode
        {
            get;
            set;
        }
        [Column("C_NAME")]
        public virtual string CName
        {
            get;
            set;
        }
        [Column("C_RESOURCE_PATH")]
        public virtual string CResourcePath
        {
            get;
            set;
        }
        [Column("C_RESOURCE_SUB_PATH")]
        public virtual string CResourceSubPath
        {
            get;
            set;
        }
        [Column("C_URL")]
        public virtual string CUrl
        {
            get;
            set;
        }
        [Column("C_QUERY_STRING")]
        public virtual string CQueryString
        {
            get;
            set;
        }
        [Column("C_ENABLE")]
        public virtual string CEnable
        {
            get;
            set;
        }
        [Column("C_ORDER")]
        public virtual string COrder
        {
            get;
            set;
        }
        [Column("C_ICON")]
        public virtual int CIcon
        {
            get;
            set;
        }
        [Column("C_TYPE")]
        public virtual int CType
        {
            get;
            set;
        }
        [Column("C_TIMESTAMP")]
        public virtual DateTime? TimeStamp
        {
            get;
            set;
        }
        /// <summary>
        /// Shortcut + "-" + Modulename
        /// </summary>
        public virtual string MyModuleName
        {
            get
            {
                string text = string.IsNullOrEmpty(this.CName) ? "" : this.CName.TrimStart("||".ToArray<char>());
                return string.IsNullOrEmpty(this.CCode) ? text : (this.CCode + "-" + text);
            }
        }
        /// <summary>
        /// 是否为系统模块菜单
        /// </summary> 
        public bool IsSystemModuleResource()
        {
            return this.CResourceSubPath.StartsWith("BoneNet.Winforms");
        }
    }
}
