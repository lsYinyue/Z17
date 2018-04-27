using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace Z17.MySql.Base
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 选择
        /// </summary>
        [Display(Name = "选择")]
        public bool Selected { get; set; }
    }

    public abstract class BaseEntity<TEntity, TPK> : BaseEntity where TEntity : BaseEntity where TPK : class
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键"), Column("ID")]
        public virtual TPK Id { get; set; }
    }
}
