using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Enums
{
    public enum YN
    {
        /// <summary>
        /// Y
        /// </summary>
        [Display(Name = "Y")]
        Y = 0,

        /// <summary>
        /// N
        /// </summary>
        [Display(Name = "N")]
        N = 1
    }
}
