using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// 类型转换类
    /// </summary>
    public static class TypeParseHelper
    {
        /// <summary>
        /// 将字符串转换成Int32数据类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static int StrToInt32(string str)
        {
            int result = 0;
            Int32.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将对象转换成Int32数据类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        public static int StrToInt32(object obj)
        {
            return StrToInt32(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成UInt32数据类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static uint StrToUInt32(string str)
        {
            uint result = 0;
            UInt32.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将对象转换成UInt32数据类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        public static uint StrToUInt32(object obj)
        {
            return StrToUInt32(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成Int64数据类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static long StrToInt64(string str)
        {
            long result = 0;
            Int64.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将对象转换成Int64数据类型
        /// </summary>
        /// <param name="str">要转换的对象</param>
        /// <returns></returns>
        public static long StrToInt64(object obj)
        {
            return StrToInt64(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成UInt64数据类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static ulong StrToUInt64(string str)
        {
            ulong result = 0;
            UInt64.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将对象转换成UInt64数据类型
        /// </summary>
        /// <param name="str">要转换的对象</param>
        /// <returns></returns>
        public static ulong StrToUInt64(object obj)
        {
            return StrToUInt64(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成Double数据类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static double StrToDouble(string str)
        {
            double result = 0;
            Double.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将对象转换成Double数据类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        public static double StrToDouble(object obj)
        {
            return StrToDouble(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成DateTime数据类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime StrToDateTime(string str)
        {
            DateTime result = DateTime.MinValue;
            DateTime.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将字符串转换成DateTime数据类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime StrToDateTime(object obj)
        {
            return StrToDateTime(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成Boolean数据类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean StrToBoolean(string str)
        {
            bool result = false;
            Boolean.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将字符串转换成Boolean数据类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Boolean StrToBoolean(object obj)
        {
            return StrToBoolean(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成Decimal数据类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal StrToDecimal(string str)
        {
            decimal result = 0;
            Decimal.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将字符串转换成Decimal数据类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Decimal StrToDecimal(object obj)
        {
            return StrToDecimal(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成Guid数据类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid StrToGuid(string str)
        {
            Guid result = Guid.Empty;
            Guid.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将字符串转换成Guid数据类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid StrToGuid(object obj)
        {
            if (obj == null)
                return Guid.Empty;
            return StrToGuid(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成IPAddress类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IPAddress StrToIPAddress(string str)
        {
            IPAddress result = IPAddress.Any;
            IPAddress.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 将字符串转换成IPAddress类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IPAddress StrToIPAddress(object obj)
        {
            if (obj == null)
                return IPAddress.Any;
            return StrToIPAddress(obj.ToString());
        }
        /// <summary>
        /// 将字符串转换成T数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T StrToT<T>(string str)
        {
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(str, typeof(T));
            object[] parameters = new object[] { str, default(T) };
            Type type = typeof(T);
            MethodInfo methodInfo = type.GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string), type.MakeByRefType() }, null);
            object result = methodInfo.Invoke(null, BindingFlags.Static | BindingFlags.Public, null, parameters, null);
            if ((bool)result)
            {
                return (T)parameters[1];
            }
            return default(T);
        }
    }
}
