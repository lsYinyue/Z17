using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Caching
{
    /// <summary>
	/// 缓存对象
	/// </summary>
	public interface ICache
    {
        /// <summary>
        /// 获取一个缓存对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="factory">初始化缓存表达式</param>
        /// <param name="seconds">默认缓存时间，单位秒</param>
        /// <returns>缓存值</returns>
        object Get(string key, Func<string, object> factory = null, int seconds = 7200);
        /// <summary>
        /// 设置一个缓存对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="slidingExpireSeconds">多少秒后过期</param>
        void Set(string key, object value, int slidingExpireSeconds = 28800);
        /// <summary>
        /// 获取所有的缓存键
        /// </summary> 
        List<string> GetKeys();
        /// <summary>
        /// 移除指定的缓存对象
        /// </summary> 
        void Remove(string key);
        /// <summary>
        /// 清空所有的缓存
        /// </summary> 
        void Clear();
    }
}
