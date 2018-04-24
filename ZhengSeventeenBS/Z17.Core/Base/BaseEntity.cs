using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Z17.Core.Base
{
    public abstract class BoneEntity
    {
        /// <summary>
        /// 选择
        /// </summary>
        [Display(Name = "选择")]
        public bool Selected { get; set; }
    }

    public abstract class BoneEntity<TEntity, TPK> : BoneEntity where TEntity : BoneEntity where TPK : class
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键"), Column("ID")]
        public virtual TPK Id { get; set; }
    }
}
