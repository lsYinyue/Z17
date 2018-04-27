using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Z17.MySql.Base;

namespace Z17.MySql.Helpers
{
    /// <summary>
    /// 常见的加密解密散列等
    /// </summary>
    public class SimpleCipherHelper : BaseHelper<SimpleCipherHelper>
    {
        private string prefix = "encrypt:";
        public string SHA512Encrypt(string strSource)
        {
            string result;
            using (var sHA512Managed = new SHA512Managed())
            {
                byte[] inArray = sHA512Managed.ComputeHash(Encoding.Default.GetBytes(strSource));
                result = Convert.ToBase64String(inArray);
            }
            return result;
        }
        /// <summary>
        /// MD5加密
        /// </summary> 
        public string MD5Encrypt(string strSource)
        {
            var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            return BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(strSource))).Replace("-", "").ToUpper();
        }
        public string MD5Signature(FileInfo fileInfo)
        {
            if (fileInfo.Length > 20971520L)
            {
                return Guid.Empty.ToString("N");
            }

            var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] value = mD5CryptoServiceProvider.ComputeHash(fileStream);
                return BitConverter.ToString(value).Replace("-", "");
            }
        }
        /// <summary>
        /// MD5加密(加盐)
        /// </summary> 
        public string MD5EncryptWithSalt(string strSource, string salt = "lis")
        {
            return MD5Encrypt(SHA512Encrypt(strSource) + SHA512Encrypt(salt));
        }
        /// <summary>
        /// AES加密函数
        /// </summary>
        /// <param name="toEncrypt">待加密字符串</param>
        /// <returns>加密后字符串</returns>
        public string AesEncrypt(string toEncrypt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Get16ByteMd5("A234567890123456789012345678901B"));
            byte[] bytes2 = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
            byte[] array = cryptoTransform.TransformFinalBlock(bytes2, 0, bytes2.Length);
            return prefix + Convert.ToBase64String(array, 0, array.Length);
        }
        /// <summary>
        /// AES解密函数
        /// </summary>
        /// <param name="toDecrypt">待解密字符串</param>
        /// <returns>解密后字符串</returns>
        public string AesDecrypt(string toDecrypt)
        {
            if (!IsEncryptString(toDecrypt))
            {
                return toDecrypt;
            }

            toDecrypt = toDecrypt.Substring(prefix.Length);
            byte[] bytes = Encoding.UTF8.GetBytes(Get16ByteMd5("A234567890123456789012345678901B"));
            byte[] array = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
            byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
            return Encoding.UTF8.GetString(bytes2);
        }
        private string Get16ByteMd5(string str)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            return BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(str)), 4, 8).Replace("-", "").ToUpper();
        }
        /// <summary>
        /// 判断是否为加密字符串
        /// </summary> 
        public bool IsEncryptString(string str)
        {
            return str.StartsWith("encrypt:");
        }
    }
}
