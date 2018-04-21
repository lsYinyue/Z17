using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Z17.Core.Extensions
{
    /// <summary>
    /// string类型扩展
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 将字符串右对齐，截短或者填充到指定的长度（默认不填充）
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="paddingChar">需要补齐的字符串</param>
        /// <returns>截短后的字符串</returns>
        public static string TakeRight(this string s, int length, char paddingChar = '\0')
        {
            string text = (s.Length > length) ? s.Substring(s.Length - length, length) : s;
            bool flag = paddingChar > '\0';
            if (flag)
            {
                text = text.PadLeft(length, paddingChar);
            }
            return text;
        }

        /// <summary>
        /// 将字符串左对齐，截短或者填充到指定的长度（默认不填充）
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="paddingChar">需要补齐的字符串</param>
        /// <returns>截短后的字符串</returns>
        public static string TakeLeft(this string s, int length, char paddingChar = '\0')
        {
            string text = (s.Length > length) ? s.Substring(0, length) : s;
            bool flag = paddingChar > '\0';
            if (flag)
            {
                text = text.PadRight(length, paddingChar);
            }
            return text;
        }

        /// <summary>
        /// 字符串右边补空字符串，汉字算2位，字符1位
        /// </summary>
        /// <param name="dataitem">源字符串</param>
        /// <param name="length">固定长度</param> 
        public static string PadRightByByte(this string dataitem, int length)
        {
            byte[] array = new byte[length];
            byte[] bytes = Encoding.Default.GetBytes(dataitem);
            bool flag = bytes.Length > length;
            if (flag)
            {
                throw new Exception("源字符串超长!");
            }
            for (int i = 0; i < length; i++)
            {
                bool flag2 = i < bytes.Length;
                if (flag2)
                {
                    array[i] = bytes[i];
                }
                else
                {
                    array[i] = 32;
                }
            }
            return Encoding.Default.GetString(array);
        }

        /// <summary>
        /// 将字符串转化为bool类型值，可以识别[y,yes,true]字符
        /// </summary> 
        public static bool IsTrue(this string s)
        {
            return s != null && new string[] { "y", "true", "yes" }.Contains(s.Trim().ToLower());
        }

        /// <summary>
        /// 判断本地路径是否为一个目录
        /// </summary> 
        public static bool IsDirectory(this string filepath)
        {
            var fileInfo = new FileInfo(filepath);
            return (fileInfo.Attributes & FileAttributes.Directory) > (FileAttributes)0;
        }

        /// <summary>
        /// 判断字符串是否为数字
        /// </summary> 
        public static bool IsNumeric(this string str)
        {
            return decimal.TryParse(str, out decimal num);
        }

        /// <summary>
        /// 是否包含中文字符
        /// </summary> 
        public static bool IsChinese(this string s)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+");//正则表达式，表示汉字范围
            return s.ToCharArray().ToList().Any(x => regex.IsMatch(x.ToString()));
        }

        /// <summary>
        /// 是否符合通配符匹配(*,?)
        /// </summary> 
        public static bool IsLike(this string str, string pattern)
        {
            return new Regex("^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(str);
        }
    }
}
