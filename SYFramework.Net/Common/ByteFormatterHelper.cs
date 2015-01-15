using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// 字节格式化显示方式
    /// </summary>
    public static class ByteFormatterHelper
    {
        private const float KB = 1024.0f;
        private const float MB = KB * 1024.0f;
        private const float GB = MB * 1024.0f;

        private const string BFormatPattern = "{0} b";
        private const string KBFormatPattern = "{0,25:N} KB";
        private const string MBFormatPattern = "{0,25:N} MB";
        private const string GBFormatPattern = "{0,25:N} GB";

        public static string ToString(long size)
        {
            if (size < KB)
            {
                return String.Format(BFormatPattern, size);
            }
            else if (size >= KB && size < MB)
            {
                return String.Format(KBFormatPattern, size / KB).Trim();
            }
            else if (size >= MB && size < GB)
            {
                return String.Format(MBFormatPattern, size / MB).Trim();
            }
            else // size >= GB
            {
                return String.Format(GBFormatPattern, size / GB).Trim();
            }
        }
    }
}
