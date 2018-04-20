using Newtonsoft.Json.Linq;
using Service.Library.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Library
{
    public interface IMainService : ISerice
    {
        JArray GetUserCompany(UserInfo userInfo);
        JArray GetUserModule(UserInfo userInfo);
    }
}
