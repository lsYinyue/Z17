using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Z17.Core.Dtos;
using Z17.Core.Services;

namespace Service.Library
{
    public class PerService : ILoginService
    {
        public JObject LoginCheck(string username, string password)
        {
            PermissionService.Proxy.GetUserMenuItems(username);
            throw new NotImplementedException();
        }
        public List<MenuItemDto> GetUserMenuItems(string username)
        {
            var ret = PermissionService.Proxy.GetUserMenuItems(username);
            return ret;
        }
    }
}
