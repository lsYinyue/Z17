﻿using System;
using LinqToDB.Mapping;
using Z17.Core.Base;

namespace Z17.Core.Entities
{
    [Table("TS_ROLE_PERMISSION")]
    [Serializable]
    public class TsRolePermission : BoneEntity<TsRolePermission, string>
    {
        [Column("C_ROLEID")]
        public virtual string CRoleId
        {
            get;
            set;
        }
        [Column("C_RESOURCE_ID")]
        public virtual string CResourceId
        {
            get;
            set;
        }
        [Column("C_RESOURCE_TYPE")]
        public virtual int CResourceType
        {
            get;
            set;
        }
    }
}
