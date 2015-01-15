using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Common
{
    public static class UtilHelper
    {

        public static int GetRamdom(int minValue, int maxValue)
        {
            return StaticRamdom.Next(minValue, maxValue);
        }

        public static long GetRamdom(long minValue, long maxValue)
        {
            return StaticRamdom.Next(minValue, maxValue);
        }
    }
}
