using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Cache
{
    /// <summary>
    /// 缓存包实体类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class CachePackage<TKey, TValue>
    {
        #region 变量
        private TKey _Key;
        private TValue _Value;
        private DateTime _LastAccessTime;
        #endregion

        #region 构造函数
        public CachePackage(TKey key, TValue value)
        {
            this._Key = key;
            this._Value = value;
            this._LastAccessTime = DateTime.Now;
        }
        #endregion

        #region 属性
        public TKey Key
        {
            get { return this._Key; }
        }
        public TValue Value
        {
            get
            {
                this._LastAccessTime = DateTime.Now;
                return this._Value;
            }
        }
        public DateTime LastAccessTime
        {
            get { return this._LastAccessTime; }
        }
        #endregion
    }
}
