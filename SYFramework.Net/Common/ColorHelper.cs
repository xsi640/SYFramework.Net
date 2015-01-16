using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SYFramework.Net.Common
{
    public static class ColorHelper
    {
        /// <summary>
        /// 获得Color对象 包含透明度
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetColor(string color)
        {
            if (RegexHelper.IsWpfColor(color))
            {
                string a = color.Substring(1, 2);
                string r = color.Substring(3, 2);
                string g = color.Substring(5, 2);
                string b = color.Substring(7, 2);

                Color c = new Color();
                c.A = Byte.Parse(a, NumberStyles.AllowHexSpecifier);
                c.R = Byte.Parse(r, NumberStyles.AllowHexSpecifier);
                c.G = Byte.Parse(g, NumberStyles.AllowHexSpecifier);
                c.B = Byte.Parse(b, NumberStyles.AllowHexSpecifier);
                return c;
            }
            return Color.FromArgb(255, 255, 255, 255);
        }
        /// <summary>
        /// 获得Color对象,无透明度
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetColorNoAlpha(string color)
        {
            if (RegexHelper.IsHtmlColor(color))
            {
                string r = color.Substring(1, 2);
                string g = color.Substring(3, 2);
                string b = color.Substring(5, 2);

                Color c = new Color();
                c.A = Byte.Parse("FF", NumberStyles.AllowHexSpecifier);
                c.R = Byte.Parse(r, NumberStyles.AllowHexSpecifier);
                c.G = Byte.Parse(g, NumberStyles.AllowHexSpecifier);
                c.B = Byte.Parse(b, NumberStyles.AllowHexSpecifier);
                return c;
            }
            return Color.FromArgb(255, 255, 255, 255);
        }
        /// <summary>
        /// 转换DrawingColorToMediaColor颜色
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ConvertColorByDrawColor(System.Drawing.Color color)
        {
            if (color == null)
                return Colors.Transparent;
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        /// <summary>
        /// 获得Color颜色，带透明度
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string GetStringColor(Color color)
        {
            string a = color.A.ToString("X");
            string r = color.R.ToString("X");
            string g = color.G.ToString("X");
            string b = color.B.ToString("X");

            if (a.Length == 1)
                a = "0" + a;
            if (r.Length == 1)
                r = "0" + r;
            if (g.Length == 1)
                g = "0" + g;
            if (b.Length == 1)
                b = "0" + b;
            return string.Format("#{0}{1}{2}{3}", a, r, g, b);
        }
        /// <summary>
        /// 获得Color颜色，无透明度
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string GetStringColorNoAlpha(Color color)
        {
            string r = color.R.ToString("X");
            string g = color.G.ToString("X");
            string b = color.B.ToString("X");

            if (r.Length == 1)
                r = "0" + r;
            if (g.Length == 1)
                g = "0" + g;
            if (b.Length == 1)
                b = "0" + b;
            return string.Format("#{0}{1}{2}", r, g, b);
        }
    }
}
