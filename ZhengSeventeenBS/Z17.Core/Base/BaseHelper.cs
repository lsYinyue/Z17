using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Base
{
    public abstract class BoneHelper
    {
        public BoneHelper()
        {

        }
    }

    /// <summary>
    /// 辅助器基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BoneHelper<T> : BoneHelper where T : BoneHelper, new()
    {
        private static T _instance = null;
        public static T Instance => _instance ?? (_instance = new T());

    }
}
