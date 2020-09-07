using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LIU.Framework.Common
{
    /// <summary>
    /// 加密解密帮助类
    /// </summary>
    public class CryptoHelper
    {
        #region AES

        /// <summary>
        /// 公共秘钥
        /// </summary>
        public const string commonKey = "liuhaojie_940502";

        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key = null)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            if (string.IsNullOrWhiteSpace(key))
                key = commonKey;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">明文（待解密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key = null)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            if (string.IsNullOrWhiteSpace(key))
                key = commonKey;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion
    }
}
