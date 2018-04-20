using Newtonsoft.Json.Linq;
using Service.Library.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Library
{
    public interface ITS1010Service : ISerice
    {
        JArray GetAllModules(UserInfo UserInfo);

        JObject AddModule(JObject Module, UserInfo UserInfo);

        JObject EditModuleById(JObject Module, UserInfo UserInfo);

        JObject DelModuleById(string ModuleId, UserInfo UserInfo);

        JArray GetModuleType(UserInfo UserInfo);

        JArray GetModuleById(string ModuleId, UserInfo UserInfo);
    }
}
