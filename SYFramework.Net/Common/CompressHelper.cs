using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// 数据压缩辅助类
    /// </summary>
    public static class CompressHelper
    {
        #region GZip
        private const int BUFFER_LENGTH = 2048;
        /// <summary>
        /// GZip压缩字符串
        /// </summary>
        /// <param name="text">待压缩字符串</param>
        /// <returns>已压缩字符串</returns>
        public static string GZipCompress(string text)
        {
            string result = string.Empty;
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] dData = GZipCompress(data);
            result = Convert.ToBase64String(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }

        /// <summary>
        /// GZip解压缩字符串
        /// </summary>
        /// <param name="text">待解压缩字符串</param>
        /// <returns>已解压缩字符串</returns>
        public static string GZipDeCompress(string text)
        {
            string result = string.Empty;
            byte[] data = Convert.FromBase64String(text);
            byte[] dData = GZipDeCompress(data);
            result = Encoding.UTF8.GetString(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }

        /// <summary>
        /// GZip压缩字节数组
        /// </summary>
        /// <param name="data">待压缩字节数组</param>
        /// <param name="isClearData">压缩完成后，是否清除待压缩字节数组里面的内容</param>
        /// <returns>已压缩字节数组</returns>
        public static byte[] GZipCompress(byte[] data, bool isClearData = true)
        {
            byte[] result = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zip.Write(data, 0, data.Length);
                    zip.Flush();
                }
                result = ms.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return result;
        }

        /// <summary>
        /// GZip解压缩字节数组
        /// </summary>
        /// <param name="data">待解压缩字节数组</param>
        /// <param name="isClearData">解压缩完成后，是否清除待解压缩字节数组里面的内容</param>
        /// <returns>已解压缩字节数组</returns>
        public static byte[] GZipDeCompress(byte[] data, bool isClearData = true)
        {
            byte[] result = null;
            using (MemoryStream o = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                    {
                        zip.Flush();
                        int size = 0;
                        byte[] buffer = new byte[BUFFER_LENGTH];
                        while ((size = zip.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            o.Write(buffer, 0, size);
                        }
                    }
                }
                result = o.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return result;
        }
        /// <summary>
        /// GZip方式压缩单个文件
        /// </summary>
        /// <param name="srcFilePath">源文件路径</param>
        /// <param name="desFilePath">压缩后的文件路径</param>
        public static void GZipCompressFile(string srcFilePath, string desFilePath)
        {
            if (!File.Exists(srcFilePath))
                return;
            using (FileStream inFile = new FileStream(srcFilePath, FileMode.Open))
            {
                using (FileStream outFile = new FileStream(desFilePath, FileMode.Create))
                {
                    using (GZipStream zip = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        inFile.CopyTo(zip);
                    }
                }
            }
        }
        /// <summary>
        /// GZip方式解压单个文件
        /// </summary>
        /// <param name="srcFilePath">源文件路径</param>
        /// <param name="desFilePath">解压后的文件路径</param>
        public static void GZipDeCompressFile(string srcFilePath, string desFilePath)
        {
            if (!File.Exists(srcFilePath))
                return;
            using (FileStream inFile = new FileStream(srcFilePath, FileMode.Open))
            {
                using (FileStream outFile = File.Create(desFilePath))
                {
                    using (GZipStream zip = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        zip.CopyTo(outFile);
                    }
                }
            }

        }
        #endregion

        #region Deflate
        /// <summary>
        /// Deflate压缩字符串
        /// </summary>
        /// <param name="text">待压缩字符串</param>
        /// <returns>已压缩字符串</returns>
        public static string DeflateCompress(string text)
        {
            string result = string.Empty;
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] dData = DeflateCompress(data);
            result = Convert.ToBase64String(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }

        /// <summary>
        /// Deflate解压缩字符串
        /// </summary>
        /// <param name="text">待解压缩字符串</param>
        /// <returns>已解压缩字符串</returns>
        public static string DeflateDeCompress(string text)
        {
            string result = string.Empty;
            byte[] data = Convert.FromBase64String(text);
            byte[] dData = DeflateDeCompress(data);
            result = Encoding.UTF8.GetString(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }

        /// <summary>
        /// Deflate压缩字节数组
        /// </summary>
        /// <param name="data">待压缩字节数组</param>
        /// <param name="isClearData">解压缩完成后，是否清除待解压缩字节数组里面的内容</param>
        /// <returns>已压缩字节数组</returns>
        public static byte[] DeflateCompress(byte[] data, bool isClearData = true)
        {
            byte[] result = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (DeflateStream deflate = new DeflateStream(ms, CompressionMode.Compress, true))
                {
                    deflate.Write(data, 0, data.Length);
                    deflate.Flush();
                }
                result = ms.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return result;
        }

        /// <summary>
        /// Deflate解压缩字节数组
        /// </summary>
        /// <param name="data">待解压缩字节数组</param>
        /// <param name="isClearData">解压缩完成后，是否清除待解压缩字节数组里面的内容</param>
        /// <returns>已解压缩字节数组</returns>
        public static byte[] DeflateDeCompress(byte[] data, bool isClearData = true)
        {
            byte[] result = null;
            using (MemoryStream o = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (DeflateStream deflate = new DeflateStream(ms, CompressionMode.Decompress))
                    {
                        deflate.Flush();
                        int size = 0;
                        byte[] buffer = new byte[BUFFER_LENGTH];
                        while ((size = deflate.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            o.Write(buffer, 0, size);
                        }
                    }
                }
                result = o.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return result;
        }
        #endregion
    }
}
