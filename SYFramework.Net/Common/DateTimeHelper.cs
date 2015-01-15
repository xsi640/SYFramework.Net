using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 获得当天的最大时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToUpper(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }
        /// <summary>
        /// 获得当天的最小时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToLower(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }
        /// <summary>
        /// 获得月份的中文大写
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetChineseCapitalMonth(DateTime dt)
        {
            switch (dt.Month)
            {
                case 1:
                    return "一";
                case 2:
                    return "二";
                case 3:
                    return "三";
                case 4:
                    return "四";
                case 5:
                    return "五";
                case 6:
                    return "六";
                case 7:
                    return "七";
                case 8:
                    return "八";
                case 9:
                    return "九";
                case 10:
                    return "十";
                case 11:
                    return "十一";
                case 12:
                    return "十二";
            }
            return string.Empty;
        }

        #region IsOnTime
        /// <summary>
        /// IsOnTime 时间val与requiredTime之间的差值是否在maxToleranceInSecs范围之内。
        /// </summary>        
        public static bool IsOnTime(DateTime requiredTime, DateTime val, int maxToleranceInSecs)
        {
            TimeSpan span = val - requiredTime;
            double spanMilliseconds = span.TotalMilliseconds < 0 ? (-span.TotalMilliseconds) : span.TotalMilliseconds;

            return (spanMilliseconds <= (maxToleranceInSecs * 1000));
        }

        /// <summary>
        /// IsOnTime 对于循环调用，时间val与startTime之间的差值(>0)对cycleSpanInSecs求余数的结果是否在maxToleranceInSecs范围之内。
        /// </summary>        
        public static bool IsOnTime(DateTime startTime, DateTime val, int cycleSpanInSecs, int maxToleranceInSecs)
        {
            TimeSpan span = val - startTime;
            double spanMilliseconds = span.TotalMilliseconds;
            double residual = spanMilliseconds % (cycleSpanInSecs * 1000);

            return (residual <= (maxToleranceInSecs * 1000));
        }
        #endregion

        /// <summary>
        /// 返回问候语
        /// </summary>
        /// <returns></returns>
        public static string Greeting()
        {
            string greeting = "凌晨好";
            if (DateTime.Now.Hour > 5 && DateTime.Now.Hour <= 8)
                greeting = "早晨好";
            else if (DateTime.Now.Hour > 8 && DateTime.Now.Hour <= 11)
                greeting = "上午好";
            else if (DateTime.Now.Hour > 11 && DateTime.Now.Hour <= 13)
                greeting = "中午好";
            else if (DateTime.Now.Hour > 13 && DateTime.Now.Hour <= 18)
                greeting = "下午好";
            else if (DateTime.Now.Hour > 18 && DateTime.Now.Hour <= 23)
                greeting = "晚上好";
            return greeting;
        }

    }
}
