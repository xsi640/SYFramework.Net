using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SYFramework.Net.Basic
{
    /// <summary>
    /// 消息报文加密、解密器
    /// </summary>
    public static class EncryptHelper
    {
        private static readonly SymmetricAlgorithm _CryptoService;
        private static readonly byte[] _Key;
        private static readonly byte[] _IV;
        private const int COUNT = 1024;

        static EncryptHelper()
        {
            _CryptoService = new AesManaged();

            string sKey = "E2ghj*Ghg7!rNifb&95GUY86GfahUb#w";//长度固定为32位byte[]
            string sIV = "ASDF7031KL4j01^&";//长度固定为16位byte[]

            //以下代码是判断Key和IV的长度 如果过长则截取 过短则补足 满足key为32位byte[] iv为16位byte[]
            _CryptoService.GenerateKey();
            int keyLength = _CryptoService.Key.Length;
            if (sKey.Length > keyLength)
                sKey = sKey.Substring(0, keyLength);
            else if (sKey.Length < keyLength)
                sKey = sKey.PadRight(keyLength, ' ');
            _Key = ASCIIEncoding.ASCII.GetBytes(sKey);

            _CryptoService.GenerateIV();
            int ivLength = _CryptoService.IV.Length;
            if (sIV.Length > ivLength)
                sIV = sIV.Substring(0, ivLength);
            else if (sIV.Length < ivLength)
                sIV = sIV.PadRight(ivLength, ' ');
            _IV = ASCIIEncoding.ASCII.GetBytes(sIV);

            _CryptoService.Key = _Key;
            _CryptoService.IV = _IV;
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="text">明文字符串</param>
        /// <returns>密文字符串</returns>
        public static string Encrypt(string text)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text))
                return result;
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] eData = Encrypt(data);
            if (eData != null)
            {
                result = Convert.ToBase64String(eData);
                Array.Clear(eData, 0, eData.Length);
            }
            return result;
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="text">密文字符串</param>
        /// <returns>明文字符串</returns>
        public static string Decrypt(string text)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text))
                return result;
            byte[] data = Convert.FromBase64String(text);
            byte[] eData = Decrypt(data);
            if (eData != null)
            {
                result = Encoding.UTF8.GetString(eData);
                Array.Clear(eData, 0, eData.Length);
            }
            return result;
        }
        /// <summary>
        /// 加密字节
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="isClearData">加密后是否清除明文里面的数据</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] data, bool isClearData = true)
        {
            byte[] result = null;
            if (data == null)
                return result;
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    using (ICryptoTransform encrypto = _CryptoService.CreateEncryptor())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write))
                        {
                            cs.Write(data, 0, data.Length);

                            cs.FlushFinalBlock();
                        }
                    }
                }
                catch (CryptographicException)
                {//wyp 根据下面的Decrypt方法企业宝运行时会出现错误，所以这里也加上以防万一。
                    return result;
                }
                result = ms.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return result;
        }

        /// <summary>
        /// 解密字节
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="isClearData">解密后是否清除密文里面的数据</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] data, bool isClearData = true)
        {
            byte[] result = null;
            if (data == null)
                return result;
            using (MemoryStream o = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(data, 0, data.Length))
                {
                    try
                    {
                        using (ICryptoTransform encrypto = _CryptoService.CreateDecryptor())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read))
                            {
                                cs.Flush();
                                int size = 0;
                                byte[] buffer = new byte[COUNT];
                                while ((size = cs.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    o.Write(buffer, 0, size);
                                }
                            }
                        }
                    }
                    catch (CryptographicException)
                    {//wyp 企业宝使用过程中会报着个错，然后程序就自动退出了。所以加此try...catch...判断
                        return null;
                    }
                }
                result = o.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return result;
        }
    }
}
