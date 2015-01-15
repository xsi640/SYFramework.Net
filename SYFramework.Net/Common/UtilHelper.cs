using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Common
{
    public static class UtilHelper
    {
        private static Random _Random = null;
        private static object _Locker = new object();

        public static int GetRamdom(int minValue, int maxValue)
        {
            if (_Random == null)
            {
                lock (_Locker)
                {
                    if (_Random == null)
                    {
                        long tick = DateTime.Now.Ticks;
                        _Random = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                    }
                }
            }
            return _Random.Next(minValue, maxValue);
        }
    }
}
