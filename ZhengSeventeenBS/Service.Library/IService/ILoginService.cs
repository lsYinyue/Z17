using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z17.Core.Dtos;

namespace Service.Library
{
    public interface ILoginService : ISerice
    {
        JObject LoginCheck(string username, string password);
    }
}
