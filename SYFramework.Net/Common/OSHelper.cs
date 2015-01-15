using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// OS辅助类
    /// </summary>
    public static class OSHelper
    {
        private static bool? runningOnWin7 = null;
        /// <summary>
        /// 用户操作系统是否是Win7以上
        /// </summary>
        public static bool RunningOnWin7
        {
            get
            {
                if (!runningOnWin7.HasValue)
                {
                    runningOnWin7 = new bool?((Environment.OSVersion.Version.Major > 6) || ((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor >= 1)));
                }
                return runningOnWin7.Value;
            }
        }
    }
}
