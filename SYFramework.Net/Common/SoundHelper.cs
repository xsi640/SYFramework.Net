using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace SYFramework.Net.Common
{
    public class SoundHelper
    {
        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="uri"></param>
        public static void PlaySound(string uri)
        {
            SoundPlayer player = new SoundPlayer(uri);
            try
            {
                player.Play();
            }
            catch (TimeoutException)
            {
            }
            catch (InvalidOperationException)
            {
            }
            catch (System.IO.FileNotFoundException)
            {
            }
        }
        /// <summary>
        /// 播放蜂鸣声音
        /// </summary>
        public static void PlayBeed()
        {
            SystemSounds.Beep.Play();
        }
        /// <summary>
        /// 播放系统声音
        /// </summary>
        public static void PlayExclamation()
        {
            SystemSounds.Exclamation.Play();
        }
    }
}
