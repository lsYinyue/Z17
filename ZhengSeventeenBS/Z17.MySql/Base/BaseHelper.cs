using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.MySql.Base
{
    public abstract class BaseHelper
    {
        public BaseHelper()
        {

        }
    }

    /// <summary>
    /// 辅助器基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseHelper<T> : BaseHelper where T : BaseHelper, new()
    {
        private static T _instance = null;
        public static T Instance => _instance ?? (_instance = new T());

    }
}
