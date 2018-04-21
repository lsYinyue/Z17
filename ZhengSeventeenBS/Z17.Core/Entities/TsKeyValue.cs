using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using Z17.Core.Base;

namespace Z17.Core.Entities
{
    /// <summary>
    /// TsKeyValue
    /// </summary> 
    [Description("TsKeyValue"), Table("TS_KEY_VALUE")]
    [Serializable]
    public class TsKeyValue : BaseEntity<TsKeyValue, string>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required, Display(Name = "主键"), Column("ID"), PrimaryKey]
        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }
        /// <summary>
        /// 父编码
        /// </summary> 
        [StringLength(512), Display(Name = "父编码"), Column("C_PCODE")]
        public virtual string CPCode
        {
            get;
            set;
        }
        /// <summary>
        /// 编码
        /// </summary> 
        [StringLength(512), Display(Name = "编码"), Column("C_CODE")]
        public virtual string CCode
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary> 
        [StringLength(512), Display(Name = "名称"), Column("C_NAME")]
        public virtual string CName
        {
            get;
            set;
        }
        /// <summary>
        /// 扩展值
        /// </summary> 
        [StringLength(4000), Display(Name = "扩展值"), Column("C_VALUE")]
        public virtual string CValue
        {
            get;
            set;
        }
        /// <summary>
        /// 描述
        /// </summary> 
        [StringLength(512), Display(Name = "描述"), Column("C_DESCRIPTION")]
        public virtual string CDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 分组
        /// </summary> 
        [StringLength(512), Display(Name = "分组"), Column("C_GROUP")]
        public virtual string CGroup
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary> 
        [Required, Display(Name = "排序"), Column("C_ORDER")]
        public virtual long COrder
        {
            get;
            set;
        }
        /// <summary>
        /// 启用
        /// </summary> 
        [StringLength(512), Display(Name = "启用"), Column("C_ENABLE")]
        public virtual string CEnable
        {
            get;
            set;
        }
        /// <summary>
        /// 扩展1
        /// </summary> 
        [StringLength(512), Display(Name = "扩展1"), Column("C_SW01")]
        public virtual string CSw01
        {
            get;
            set;
        }
        /// <summary>
        /// 扩展2
        /// </summary> 
        [StringLength(512), Display(Name = "扩展2"), Column("C_SW02")]
        public virtual string CSw02
        {
            get;
            set;
        }
        /// <summary>
        /// 扩展3
        /// </summary> 
        [StringLength(512), Display(Name = "扩展3"), Column("C_SW03")]
        public virtual string CSw03
        {
            get;
            set;
        }
    }
}
