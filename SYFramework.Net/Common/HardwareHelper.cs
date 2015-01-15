using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace SYFramework.Net.Common
{
    public static class HardwareHelper
    {
        #region 获取cpu序列号 硬盘ID 网卡硬地址
        /// <summary>
        /// 获取cpu序列号
        /// </summary>
        /// <returns>string </returns>
        public static string GetCpuId()
        {
            string cpuId = "";
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection mocs = mc.GetInstances();
            foreach (ManagementObject mo in mocs)
            {
                cpuId = mo.Properties["ProcessorId"].Value.ToString();
                break;
            }
            if (mocs != null)
                mocs.Dispose();
            if (mc != null)
                mc.Dispose();
            return cpuId.ToString();
        }

        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns>string </returns>
        public static string GetHDid()
        {
            string hdId = "";
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection mocs = mc.GetInstances();
            foreach (ManagementObject mo in mocs)
            {
                hdId = mo.Properties["Model"].Value.ToString();
                break;
            }
            if (mocs != null)
                mocs.Dispose();
            if (mc != null)
                mc.Dispose();
            return hdId.ToString();
        }

        /// <summary>
        /// 获取网卡硬件地址
        /// </summary>
        /// <returns>string </returns>
        public static string GetMacAddress()
        {
            string address = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection mocs = mc.GetInstances();
            foreach (ManagementObject mo in mocs)
            {
                if (mo["IPEnabled"].ToString() == "true")
                {
                    address = mo["MacAddress"].ToString();
                    break;
                }
            }
            if (mocs != null)
                mocs.Dispose();
            if (mc != null)
                mc.Dispose();
            return address.ToString();
        }
        #endregion

        #region 获取网卡硬地址+CPU序列号+硬盘ID字符串
        /// <summary>
        /// 获取网卡硬地址+CPU序列号+硬盘ID字符串
        /// </summary>
        /// <returns></returns>
        public static string GetAll()
        {
            return GetCpuId() + GetMacAddress() + GetHDid();
        }
        #endregion
    }
}
