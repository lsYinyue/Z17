using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminWeb.MyClasses
{
    public class LoginUserItem
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public string Company { get; set; }

        public string CompanyName { get; set; }

        public DateTime DateTime { get; set; }
    }
}