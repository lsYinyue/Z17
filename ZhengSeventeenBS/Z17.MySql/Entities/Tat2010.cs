using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using Z17.MySql.Base;

namespace Z17.MySql.Entities
{
    /// <summary>
    /// Tat2010
    /// </summary>
    [Serializable]
    [Description("Tat2010"), Table("tat_2010")]
    public partial class Tat2010 : BaseEntity<Tat2010, String>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        [Display(Name = "主键"), Column("ID"), LinqToDB.Mapping.PrimaryKey]
        public override String Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// CMtrlNo
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CMtrlNo"), Column("C_MTRL_NO")]
        public virtual string CMtrlNo { get; set; }

        /// <summary>
        /// CMtrlDesc
        /// </summary>
        [StringLength(1024)]
        [Display(Name = "CMtrlDesc"), Column("C_MTRL_DESC")]
        public virtual string CMtrlDesc { get; set; }

        /// <summary>
        /// CMtrlNm
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CMtrlNm"), Column("C_MTRL_NM")]
        public virtual string CMtrlNm { get; set; }

        /// <summary>
        /// CUnit
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CUnit"), Column("C_UNIT")]
        public virtual string CUnit { get; set; }

        /// <summary>
        /// CPageUnit
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CPageUnit"), Column("C_PAGE_UNIT")]
        public virtual string CPageUnit { get; set; }

        /// <summary>
        /// CCompId
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CCompId"), Column("C_COMP_ID")]
        public virtual string CCompId { get; set; }

        /// <summary>
        /// CTimestamp
        /// </summary>
        [Display(Name = "CTimestamp"), Column("C_TIMESTAMP")]
        public virtual DateTime? TimeStamp { get; set; }

    }
}