using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Library
{
    public class ServiceManager<T> where T : ISerice
    {
        public static T Get()
        {
            var serviceType = ServiceIOC.ServiceDic[typeof(T).Name];
            return (T)Activator.CreateInstance(serviceType);
        }
    }

    public class ServiceIOC
    {
        public static Dictionary<string, Type> ServiceDic = new Dictionary<string, Type>();
        static ServiceIOC()
        {
            var type = "cloud";
            if ("cloud" == type)
            {
                ServiceDic.Add(nameof(IUserService), typeof(CloudService1));
                ServiceDic.Add(nameof(IRoleService), typeof(RoleCouldService));
                //以上是demo
                ServiceDic.Add(nameof(ILoginService), typeof(LoginCloudService));
                ServiceDic.Add(nameof(IMainService), typeof(MainCloudService));
                ServiceDic.Add(nameof(ITS1010Service), typeof(TS1010CloudService));
            }
        }
    }
}
