using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// 正则表达式辅助类
    /// </summary>
    public static class RegexHelper
    {
        public const string WPFCOLOR = @"^#?([a-f]|[A-F]|[0-9]){4}(([a-f]|[A-F]|[0-9]){4})?$";
        public const string HTMLCOLOR = @"^#?([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$";
        public const string NUMBER = @"^[0-9]+$";
        public const string ENGLISH = @"^[A-Za-z0-9]+$";
        public const string EMAIL = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
        public const string IP = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
        public const string URL = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
        public const string CHINESE = @"^[\u4e00-\u9fa5]{2,}$";
        public const string URLPORT = @"^(.*)://([0-9,a-z,A-Z,-,.]+):([0-9]+)?";
        public const string DOMAIN = @"([0-9A-Za-z]{2,}\.[0-9A-Za-z]{2,3}|[0-9A-Za-z]{2,}\.[0-9A-Za-z]{2,3})$";
        public const string FILENAME = "\\/:*?\"<>|";
        public const RegexOptions OPTIONS = RegexOptions.IgnoreCase | RegexOptions.Compiled;

        /// <summary>
        /// 判断是否网页颜色 如:#ffffffff 包含透明度
        /// </summary>
        /// <param name="strColor"></param>
        /// <returns></returns>
        public static bool IsWpfColor(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(WPFCOLOR, OPTIONS);
            return r.IsMatch(str);
        }
        /// <summary>
        /// 判断是否网页颜色 如:#ffffff 不包含透明度
        /// </summary>
        /// <param name="strColor"></param>
        /// <returns></returns>
        public static bool IsHtmlColor(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(HTMLCOLOR, OPTIONS);
            return r.IsMatch(str);
        }
        /// <summary>
        /// 判断是否为数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(NUMBER, OPTIONS);
            return r.IsMatch(str);
        }
        /// <summary>
        /// 判断是否为电子邮件
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(EMAIL, OPTIONS);
            return r.IsMatch(str);
        }
        /// <summary>
        /// 判断是否为IP地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIP(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(IP, OPTIONS);
            return r.IsMatch(str);
        }
        /// <summary>
        /// 判断是否为URL
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUrl(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(URL, OPTIONS);
            return r.IsMatch(str);
        }
        /// <summary>
        /// 判断是否为中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinese(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(CHINESE, OPTIONS);
            return r.IsMatch(str);
        }
        /// <summary>
        /// 判断是否为英文字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAbc(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(ENGLISH, OPTIONS);
            return r.IsMatch(str);
        }
        /// <summary>
        /// 通过URL获得协议、IP地址、端口号
        /// </summary>
        /// <param name="str"></param>
        /// <param name="proctocol"></param>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public static void ConvertUrl(string str, out string proctocol, out string ipaddress, out int port)
        {
            proctocol = string.Empty;
            ipaddress = string.Empty;
            port = 0;

            if (Regex.IsMatch(str, URLPORT, OPTIONS))
            {
                Match m = Regex.Match(str, URLPORT, OPTIONS);
                if (m.Groups.Count == 4)
                {
                    proctocol = m.Groups[1].Value;
                    ipaddress = m.Groups[2].Value;
                    port = TypeParseHelper.StrToInt32(m.Groups[3].Value);
                }
            }
        }

        public static bool IsFileName(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                char[] arr = FILENAME.ToCharArray();
                char[] array = str.ToCharArray();
                foreach (char c in array)
                {
                    foreach (char ch in arr)
                    {
                        if (c == ch)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsDomain(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            Regex r = new Regex(DOMAIN, OPTIONS);
            return r.IsMatch(str);
        }
    }
}
