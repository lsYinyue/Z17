using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Interfaces
{
    /// <summary>
    /// 序列发生器接口
    /// </summary>
    public interface ISequnceService
    {
        /// <summary>
        /// 按序列名称获取下一个序列值
        /// </summary>
        /// <param name="seqName">序列名称</param>
        /// <returns>序列值</returns>
        ulong NextVal(string seqName = "default");

        /// <summary>
		/// 生成主键(10位):yyMMddHH+2位本地流水(适用于一小时产生100条数据的频率)
		/// </summary>  
        string GenerateId();

        /// <summary>
		/// 生成主键(18位):yyMMddHHmmss+6位本地流水(适用于一秒产生100万条数据的频率)
		/// </summary> 
		/// <param name="length">序列位数</param>
		/// <param name="format">时间格式化字符串</param> 
        string GenerateLocalId(int length = 6, string format = "yyMMddHHmmss");

        /// <summary>
		/// 获取有序GUID
		/// </summary> 
        Guid NewGuid();
    }
}
