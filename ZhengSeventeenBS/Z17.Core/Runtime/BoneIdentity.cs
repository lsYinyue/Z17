﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Z17.Core.Enums;

namespace Z17.Core.Runtime
{
    public class BoneIdentity : IBoneIdentity, IIdentity, ILoginedInfo
    {
        public string UserID { get; set; }

        public string UserName { get; set; }

        public UserType UserType { get; set; }

        public string Password { get; set; }

        public string Department { get; set; }

        public string DepartmentDesc { get; set; }

        public string Name => this.UserName;

        public string AuthenticationType => "token";

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(this.UserID);

        public string Session { get; set; }

        public bool OnlyOneClient { get; set; }

        public string LoginIp { get; set; }

        public string LoginMachine { get; set; }

        public DateTime? SessionUpdateTime { get; set; }

        public DateTime? LoginTime { get; set; }
    }
}
