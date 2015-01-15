using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// 热键帮助类
    /// </summary>
    public static class HotKeyHelper
    {
        private static Dictionary<uint, string> _specialKeys = null;

        static HotKeyHelper()
        {
            _specialKeys = new Dictionary<uint, string>();
            _specialKeys.Add(0x6b, "Num +");
            _specialKeys.Add(0x6a, "Num *");
            _specialKeys.Add(0xba, ";");
            _specialKeys.Add(220, @"\");
            _specialKeys.Add(0xde, "'");
            _specialKeys.Add(0xdd, "]");
            _specialKeys.Add(0xc0, ".");
            _specialKeys.Add(190, ".");
            _specialKeys.Add(0xdb, "[");
            _specialKeys.Add(0xbc, ",");
            _specialKeys.Add(0xbf, "/");
            _specialKeys.Add(0xbb, "=");
            _specialKeys.Add(0xbd, "-");
            _specialKeys.Add(0x6f, "Num /");
            _specialKeys.Add(110, "Num Del");
            _specialKeys.Add(0x6d, "Num -");
            _specialKeys.Add(0x60, "Num 0");
            _specialKeys.Add(0x61, "Num 1");
            _specialKeys.Add(0x62, "Num 2");
            _specialKeys.Add(0x63, "Num 3");
            _specialKeys.Add(100, "Num 4");
            _specialKeys.Add(0x65, "Num 5");
            _specialKeys.Add(0x66, "Num 6");
            _specialKeys.Add(0x67, "Num 7");
            _specialKeys.Add(0x68, "Num 8");
            _specialKeys.Add(0x69, "Num 9");
        }

        /// <summary>
        /// 将KeyCode和ModifierKey转换成字符串(90|6)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static string HotKeyToCode(Keys key, ModifierKeys mod)
        {
            return string.Concat((int)key, "|", (int)mod);
        }

        /// <summary>
        /// 将KeyCode和ModifierKey转换成字符串(Ctrl+Enter)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static string HotKeyToString(Keys key, ModifierKeys mod)
        {
            uint uKey = (uint)key;
            uint uMode = (uint)mod;

            string str = string.Empty;
            if ((uMode & 8) > 0)
            {
                str += "Win + ";
            }
            if ((uMode & 2) > 0)
            {
                str += "Ctrl + ";
            }
            if ((uMode & 1) > 0)
            {
                str += "Alt + ";
            }
            if ((uMode & 4) > 0)
            {
                str += "Shift + ";
            }
            if (uKey == (uint)Keys.LWin || uKey == (uint)Keys.RWin
                || uKey == (uint)Keys.LMenu || uKey == (uint)Keys.RMenu
                || uKey == (uint)Keys.LControlKey || uKey == (int)Keys.RControlKey
                || uKey == (uint)Keys.LShiftKey || uKey == (uint)Keys.RShiftKey
                || uKey == (uint)Keys.Apps || uKey == (uint)Keys.None)
            {
                return str;
            }
            else
            {
                str += HotKeyToString(uKey);
            }
            return str;
        }
        /// <summary>
        /// 将KeyCode转换成字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string HotKeyToString(uint key)
        {
            if (key == 0)
            {
                return string.Empty;
            }
            if (((key >= 0x30) && (key <= 0x39)) || ((key >= 0x41) && (key <= 90)))
            {
                char keyValue = (char)key;
                return keyValue.ToString();
            }
            if (_specialKeys.ContainsKey(key))
            {
                return _specialKeys[key];
            }
            return ((Keys)key).ToString();
        }
    }
}
