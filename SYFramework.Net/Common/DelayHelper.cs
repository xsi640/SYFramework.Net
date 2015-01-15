using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// 延迟方法
    /// </summary>
    public static class DelayHelper
    {
        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <param name="millisecond">延迟毫秒数</param>
        /// <param name="action">延迟执行方法</param>
        public static void DelayAction(int millisecond, Action action)
        {
            if (action == null)
                return;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Tick += new EventHandler((sender, e) =>
            {
                if (timer.IsEnabled)
                {
                    action();
                }
                timer.Stop();
            });
            timer.Start();
        }

        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="millisecond">延迟毫秒数</param>
        /// <param name="action">延迟执行方法</param>
        /// <param name="obj">参数</param>
        public static void DelayAction<T>(int millisecond, Action<T> action, T obj)
        {
            if (action == null)
                return;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Tick += new EventHandler((sender, e) =>
            {
                if (timer.IsEnabled)
                {
                    action(obj);
                }
                timer.Stop();
            });
            timer.Start();
        }

        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <typeparam name="T1">参数类型1</typeparam>
        /// <typeparam name="T2">参数类型2</typeparam>
        /// <param name="millisecond">延迟毫秒数</param>
        /// <param name="action">延迟执行方法</param>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        public static void DelayAction<T1, T2>(int millisecond, Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action == null)
                return;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Tick += new EventHandler((sender, e) =>
            {
                if (timer.IsEnabled)
                {
                    action(arg1, arg2);
                }
                timer.Stop();
            });
            timer.Start();
        }

        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <typeparam name="T1">参数类型1</typeparam>
        /// <typeparam name="T2">参数类型2</typeparam>
        /// <typeparam name="T3">参数类型3</typeparam>
        /// <param name="millisecond">延迟毫秒数</param>
        /// <param name="action">延迟执行方法</param>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        /// <param name="arg3">参数3</param>
        public static void DelayAction<T1, T2, T3>(int millisecond, Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (action == null)
                return;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Tick += new EventHandler((sender, e) =>
            {
                if (timer.IsEnabled)
                {
                    action(arg1, arg2, arg3);
                }
                timer.Stop();
            });
            timer.Start();
        }

        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <param name="millisecond">延迟毫秒数</param>
        /// <param name="func">延迟执行方法</param>
        /// <param name="result">返回值</param>
        public static void DelayFunc<TResult>(int millisecond, Func<TResult> func, TResult result)
        {
            if (func == null)
                return;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Tick += new EventHandler((sender, e) =>
            {
                if (timer.IsEnabled)
                {
                    result = func();
                }
                timer.Stop();
            });
            timer.Start();
        }

        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="TResult">方法返回类型</typeparam>
        /// <param name="millisecond">延迟毫秒数</param>
        /// <param name="func">延迟执行方法</param>
        /// <param name="arg">参数</param>
        /// <param name="result">返回值</param>
        public static void DelayFunc<T, TResult>(int millisecond, Func<T, TResult> func, T arg, TResult result)
        {
            if (func == null)
                return;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Tick += new EventHandler((sender, e) =>
            {
                if (timer.IsEnabled)
                {
                    result = func(arg);
                }
                timer.Stop();
            });
            timer.Start();
        }

        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <typeparam name="T1">参数类型1</typeparam>
        /// <typeparam name="T2">参数类型2</typeparam>
        /// <typeparam name="TResult">方法返回类型</typeparam>
        /// <param name="millisecond">延迟毫秒数</param>
        /// <param name="func">延迟执行方法</param>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        /// <param name="result">返回值</param>
        public static void DelayFunc<T1, T2, TResult>(int millisecond, Func<T1, T2, TResult> func, T1 arg1, T2 arg2, TResult result)
        {
            if (func == null)
                return;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Tick += new EventHandler((sender, e) =>
            {
                if (timer.IsEnabled)
                {
                    result = func(arg1, arg2);
                }
                timer.Stop();
            });
            timer.Start();
        }

        /// <summary>
        /// 延时执行方法
        /// </summary>
        /// <typeparam name="T1">参数类型1</typeparam>
        /// <typeparam name="T2">参数类型2</typeparam>
        /// <typeparam name="TResult">方法返回类型</typeparam>
        /// <param name="millisecond">延迟毫秒数</param>
        /// <param name="func">延迟执行方法</param>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        /// <param name="arg3">参数3</param>
        /// <param name="result">返回值</param>
        public static void DelayFunc<T1, T2, T3, TResult>(int millisecond, Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3, TResult result)
        {
            if (func == null)
                return;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Tick += new EventHandler((sender, e) =>
            {
                if (timer.IsEnabled)
                {
                    result = func(arg1, arg2, arg3);
                }
                timer.Stop();
            });
            timer.Start();
        }
    }
}
