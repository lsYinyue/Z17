using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z17.Core.IOC;

namespace Z17.Core.Caching
{
    /// <summary>
    /// 缓存管理器
    /// </summary>
    public class CacheManager
    {
        private static MemoryCache _cache;

        /// <summary>
        /// 获取缓存组件
        /// </summary>
        /// <returns></returns>
        public static ICache GetCache()
        {
            return IocManager.Instance.IsRegistered<ICache>() ? IocManager.Instance.Resolve<ICache>() : MemoryCache;
        }

        /// <summary>
        /// 获取内存缓存
        /// </summary> 
        public static ICache MemoryCache
        {
            get
            {
                if (_cache == null) _cache = new MemoryCache();
                return _cache;
            }
        }
    }
}
