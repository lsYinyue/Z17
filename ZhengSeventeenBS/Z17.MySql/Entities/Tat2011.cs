using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using Z17.MySql.Base;

namespace Z17.MySql.Entities
{
    /// <summary>
    /// Tat2011
    /// </summary>
    [Serializable]
    [Description("Tat2011"), Table("tat_2011")]
    public partial class Tat2011 : BaseEntity<Tat2011, string>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        [Display(Name = "主键"), Column("ID"), LinqToDB.Mapping.PrimaryKey]
        public override string Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// CCertNo
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CCertNo"), Column("C_CERT_NO")]
        public virtual string CCertNo { get; set; }

        /// <summary>
        /// CCertNm
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CCertNm"), Column("C_CERT_NM")]
        public virtual string CCertNm { get; set; }

        /// <summary>
        /// CCertValue
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CCertValue"), Column("C_CERT_VALUE")]
        public virtual string CCertValue { get; set; }

        /// <summary>
        /// CMtrlNo
        /// </summary>
        [StringLength(255)]
        [Display(Name = "CMtrlNo"), Column("C_MTRL_NO")]
        public virtual string CMtrlNo { get; set; }

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