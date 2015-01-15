using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Resources;
using System.Windows;
using System.IO;

namespace SYFramework.Net.Common
{
    public static class ResourceHelper
    {
        /// <summary>
        /// 获取资源文件里面的图片
        /// </summary>
        /// <param name="uriString">pack://application:,,,/MPN.Resource;component/Images/图片名称</param>
        /// <returns></returns>
        public static Image GetResourceImage(string uriString)
        {
            return GetResourceImage(uriString, UriKind.RelativeOrAbsolute);
        }
        /// <summary>
        /// 获取资源文件里面的图片
        /// </summary>
        /// <param name="uriString">pack://application:,,,/MPN.Resource;component/Images/图片名称</param>
        /// <param name="uriKind"></param>
        /// <returns></returns>
        public static Image GetResourceImage(string uriString, UriKind uriKind)
        {
            return GetResourceImage(new Uri(uriString, uriKind));
        }
        /// <summary>
        /// 获取资源文件里面的图片
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Image GetResourceImage(Uri uri)
        {
            Image image = null;
            try
            {
                StreamResourceInfo sri = Application.GetResourceStream(uri);
                if (sri != null && sri.Stream != null)
                {
                    image = System.Drawing.Image.FromStream(sri.Stream);
                    sri.Stream.Dispose();
                    sri = null;
                }
            }
            catch
            { }
            return image;
        }
        /// <summary>
        /// 获取资源里面的文件流
        /// </summary>
        /// <param name="uriString">文件名称</param>
        /// <returns></returns>
        public static Stream GetResourceStream(string uriString)
        {
            return GetResourceStream(uriString, UriKind.RelativeOrAbsolute);
        }
        /// <summary>
        /// 获取资源里面的文件流
        /// </summary>
        /// <param name="uriString">文件名称</param>
        /// <param name="uriKind"></param>
        /// <returns></returns>
        public static Stream GetResourceStream(string uriString, UriKind uriKind)
        {
            return GetResourceStream(new Uri(uriString, uriKind));
        }

        /// <summary>
        /// 获取资源里面的文件流
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Stream GetResourceStream(Uri uri)
        {
            Stream stream = null;
            try
            {
                StreamResourceInfo sri = Application.GetResourceStream(uri);
                if (sri != null && sri.Stream != null)
                {
                    stream = sri.Stream;
                }
            }
            catch
            { }
            return stream;
        }
    }
}
