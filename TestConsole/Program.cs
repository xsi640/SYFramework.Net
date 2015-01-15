using SYFramework.Net.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine(UtilHelper.GetRamdom(0, 100));
        }
    }
}
