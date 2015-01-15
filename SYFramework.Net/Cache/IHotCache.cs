using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Cache
{
    public interface IHotCache<TKey, TValue> where TValue : class
    {
        /// <summary>
        /// 缓存数量
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 间隔时间（秒）
        /// </summary>
        int DetectSpanInSecs { get; set; }
        /// <summary>
        /// 最大休眠时间（秒）
        /// </summary>
        int MaxMuteSpanInSecs { get; set; }

        /// <summary>
        /// 增加一个缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(TKey key, TValue value);
        /// <summary>
        /// 清除
        /// </summary>
        void Clear();
        /// <summary>
        /// 获取一个缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue Get(TKey key);
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<TValue> GetAll();
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        void Remove(TKey key);
    }
}
