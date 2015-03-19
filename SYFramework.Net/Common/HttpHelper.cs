using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace SYFramework.Net.Common
{
    public static class HttpHelper
    {
        public const int RESPONSE_STREAM_MAX_LENGTH = 3850;

        /// <summary>
        /// 下载文件并保存到本地目录。
        /// 下载安装文件用
        /// </summary>
        /// <param name="url">请求的URL，不要包含"?"或者其他参数</param>
        /// <param name="filePath">本地保存的文件路径</param>
        /// <param name="nvc">请求的参数</param>
        /// <param name="webProxy">代理设置</param>
        /// <returns></returns>
        public static bool DownloadFile(string url, string filePath, NameValueCollection nvc = null, IWebProxy webProxy = null)
        {
            bool flag = false;
            try
            {
                if (nvc != null && nvc.Count > 0)
                {
                    StringBuilder sb = new StringBuilder("?");
                    string[] keys = nvc.AllKeys;
                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (i == keys.Length - 1)
                            sb.Append(string.Format("{0}={1}", Uri.EscapeUriString(keys[i]), Uri.EscapeUriString(nvc[keys[i]])));
                        else
                            sb.Append(string.Format("{0}={1}&", Uri.EscapeUriString(keys[i]), Uri.EscapeUriString(nvc[keys[i]])));
                    }
                    url += sb.ToString();
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                request.Proxy = webProxy;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream rs = response.GetResponseStream())
                    {
                        string path = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        if (File.Exists(filePath))
                            File.Delete(filePath);
                        using (Stream fs = new FileStream(filePath, FileMode.Create))
                        {
                            byte[] bs = new byte[RESPONSE_STREAM_MAX_LENGTH];
                            int size = 0;
                            while ((size = rs.Read(bs, 0, RESPONSE_STREAM_MAX_LENGTH)) > 0)
                            {
                                fs.Write(bs, 0, size);
                            }
                            fs.Flush();
                        }
                    }
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public static bool UploadFile(string url, string filePath, NameValueCollection nvc = null, IWebProxy webProxy = null)
        {
            bool flag = false;
            try
            {
                if (nvc != null && nvc.Count > 0)
                {
                    StringBuilder sb = new StringBuilder("?");
                    string[] keys = nvc.AllKeys;
                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (i == keys.Length - 1)
                            sb.Append(string.Format("{0}={1}", Uri.EscapeUriString(keys[i]), Uri.EscapeUriString(nvc[keys[i]])));
                        else
                            sb.Append(string.Format("{0}={1}&", Uri.EscapeUriString(keys[i]), Uri.EscapeUriString(nvc[keys[i]])));
                    }
                    url += sb.ToString();
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                request.Proxy = webProxy;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream rs = response.GetResponseStream())
                    {
                        string path = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        if (File.Exists(filePath))
                            File.Delete(filePath);
                        using (Stream fs = new FileStream(filePath, FileMode.Create))
                        {
                            byte[] bs = new byte[RESPONSE_STREAM_MAX_LENGTH];
                            int size = 0;
                            while ((size = rs.Read(bs, 0, RESPONSE_STREAM_MAX_LENGTH)) > 0)
                            {
                                fs.Write(bs, 0, size);
                            }
                            fs.Flush();
                        }
                    }
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public static void UploadFile(string url, NameValueCollection nvc = null, string[] files = null, IWebProxy webProxy = null)
        {
            string boundary = "----------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
            StringBuilder sbParameter = new StringBuilder();
            if (nvc != null)
            {
                foreach (string key in nvc.AllKeys)
                {
                    string[] values = nvc.GetValues(key);
                    if (values != null)
                    {
                        foreach (string value in values)
                        {
                            sbParameter.AppendFormat("--{0}\r\n", boundary);
                            sbParameter.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", key, value);
                        }
                    }
                }
            }

            long fileLength = 0;
            List<FileItem> lists = null;
            if (files != null)
            {
                lists = new List<FileItem>();
                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        FileItem fileItem = GetFileItem(file, boundary);
                        lists.Add(fileItem);
                        fileLength += fileItem.Size;
                    }
                }
            }

            string foot = "\r\n--" + boundary + "--\r\n";

            byte[] bParamer = Encoding.UTF8.GetBytes(sbParameter.ToString());
            byte[] bFoot = Encoding.UTF8.GetBytes(foot);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            long contentLength = bParamer.Length + fileLength + bFoot.Length;
            webRequest.ContentLength = contentLength;
            webRequest.KeepAlive = false;
            webRequest.AllowWriteStreamBuffering = false;
            webRequest.Timeout = System.Threading.Timeout.Infinite;
            webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            if (webProxy != null)
                webRequest.Proxy = webProxy;

            using (Stream stream = webRequest.GetRequestStream())
            {
                stream.Write(bParamer, 0, bParamer.Length);
                byte[] buffer = new byte[4096];
                foreach (FileItem fileItem in lists)
                {
                    stream.Write(fileItem.Header, 0, fileItem.Header.Length);
                    using (FileStream fs = new FileStream(fileItem.FilePath, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite))
                    {
                        int readSize = 0;
                        do
                        {
                            readSize = fs.Read(buffer, 0, buffer.Length);
                            if (readSize > 0)
                            {
                                stream.Write(buffer, 0, readSize);
                            }
                        } while (readSize > 0);
                    }
                }
                stream.Write(bFoot, 0, bFoot.Length);
            }

            WebResponse webRespon = webRequest.GetResponse();
            using (Stream stream = webRespon.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string sReturnString = sr.ReadLine();
                }
            }
        }


        public static FileItem GetFileItem(string filePath, string boundary)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            string header = string.Format("--{0}\r\n", boundary);
            header += string.Format("Content-Disposition: form-data; name=\"file\"; filename=\"{1}\"\r\n", fileName, fileName);
            header += "Content-Type: application/octet-stream\r\n\r\n";

            FileItem result = new FileItem();
            result.FileName = fileName;
            result.FilePath = filePath;
            result.Header = Encoding.UTF8.GetBytes(header);
            result.Size = fileInfo.Length + result.Header.Length;
            return result;
        }

        public class FileItem
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public byte[] Header { get; set; }
            public long Size { get; set; }
        }
    }
}
