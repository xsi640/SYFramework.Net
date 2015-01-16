using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Cache
{
    public interface IStaticCache<TKey, TValue> where TValue : class
    {
        /// <summary>
        /// 已缓存数量
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(TKey key, TValue value);
        /// <summary>
        /// 获取所有缓存数据
        /// </summary>
        /// <returns></returns>
        IList<TValue> GetAll();
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        void Remove(TKey key);
    }
}
