using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Z17.Core.Base;
using Z17.Core.Entities;
using Z17.Core.Helpers;
using Z17.Core.Services;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new Db())
            //{
            //    var users = db.GetTable<TsUser>().ToList();
            //    var result = JsonHelper.Instance.ToJson(users);
            //    Console.WriteLine(result);
            //}

            {
                Console.WriteLine("用户名：");
                var userid = Console.ReadLine();

                Console.WriteLine("密码：");
                var password = Console.ReadLine();

                AccessTokenService.Proxy.Login(userid, password);

                var userMenus = PermissionService.Proxy.GetUserMenuItems(userid);
                var userMenusJson = JsonHelper.Instance.ToJson(userMenus);
                Console.WriteLine(userMenusJson);
            }

            Console.Read();
        }
    }
}
