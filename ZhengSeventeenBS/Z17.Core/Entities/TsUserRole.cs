﻿using LinqToDB.Mapping;
using System;
using Z17.Core.Base;

namespace Z17.Core.Entities
{
    [Table("TS_USER_ROLE")]
    [Serializable]
    public class TsUserRole : BoneEntity<TsUserRole, string>
    {
        [Column("C_USERID")]
        public virtual string CUserId
        {
            get;
            set;
        }
        [Column("C_ROLEID")]
        public virtual string CRoleId
        {
            get;
            set;
        }
    }
}
