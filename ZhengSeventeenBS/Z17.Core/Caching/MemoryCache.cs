using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Z17.Core.Caching
{
    /// <summary>
	/// 内存缓存类
	/// </summary>
	public class MemoryCache : ICache
    {
        public List<string> GetKeys()
        {
            var list = new List<string>();
            var enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key != null)
                    list.Add(enumerator.Key.ToString());
            }
            return list;
        }
        public object Get(string key, Func<string, object> factory = null, int seconds = 7200)
        {
            if (this.GetKeys().Contains(key))
                return HttpRuntime.Cache.Get(key);

            if (factory != null)
                HttpRuntime.Cache.Insert(key, factory(key), null, DateTime.Now.AddSeconds(seconds), TimeSpan.Zero);
            return HttpRuntime.Cache.Get(key);
        }
        public void Set(string key, object value, int slidingExpireSeconds = 28800)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddSeconds((double)slidingExpireSeconds), TimeSpan.Zero);
        }
        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
        public void Clear()
        {
            var enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key != null)
                    HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }
        }
    }
}
