using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Common
{
    public class StaticRamdom
    {
        private static Random _Random = null;
        private static object _Locker = new object();

        public static Random Random
        {
            get
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
                return _Random;
            }
        }

        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public static long Next(long minValue, long maxValue)
        {
            byte[] buf = new byte[8];
            Random.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (Math.Abs(longRand % (maxValue - minValue)) + minValue);
        }
    }
}
