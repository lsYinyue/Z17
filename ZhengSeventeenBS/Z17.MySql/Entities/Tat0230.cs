using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;
using Z17.MySql.Base;

namespace Z17.MySql.Entities
{
	/// <summary>
	/// Tat0230
	/// </summary>
	[Serializable]
	[Description("Tat0230"), Table("tat_0230")]
	public partial class Tat0230 : BaseEntity<Tat0230, string>
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Required]
		[Display(Name = "主键"), Column("ID"), LinqToDB.Mapping.PrimaryKey]
		public override string Id { get => base.Id; set => base.Id = value; }

		/// <summary>
		/// CStoreHouse
		/// </summary>
		[StringLength(512)]
		[Display(Name = "CStoreHouse"), Column("C_STORE_HOUSE")]
		public virtual string CStoreHouse { get; set; }

		/// <summary>
		/// CStoreHouseDesc
		/// </summary>
		[StringLength(512)]
		[Display(Name = "CStoreHouseDesc"), Column("C_STORE_HOUSE_DESC")]
		public virtual string CStoreHouseDesc { get; set; }

		/// <summary>
		/// CStoreHouseUser
		/// </summary>
		[StringLength(255)]
		[Display(Name = "CStoreHouseUser"), Column("C_STORE_HOUSE_USER")]
		public virtual string CStoreHouseUser { get; set; }

		/// <summary>
		/// CStoreHousePhone
		/// </summary>
		[StringLength(512)]
		[Display(Name = "CStoreHousePhone"), Column("C_STORE_HOUSE_PHONE")]
		public virtual string CStoreHousePhone { get; set; }

		/// <summary>
		/// CStoreHouseNo
		/// </summary>
		[StringLength(512)]
		[Display(Name = "CStoreHouseNo"), Column("C_STORE_HOUSE_NO")]
		public virtual string CStoreHouseNo { get; set; }

		/// <summary>
		/// CPId
		/// </summary>
		[StringLength(32)]
		[Display(Name = "CPId"), Column("C_PID")]
		public virtual string CPId { get; set; }

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