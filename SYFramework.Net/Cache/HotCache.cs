using SYFramework.Net.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Cache
{
    public class HotCache<TKey, TValue> : IHotCache<TKey, TValue> where TValue : class
    {
        #region 变量
        private IDictionary<TKey, CachePackage<TKey, TValue>> _Dictionary = new Dictionary<TKey, CachePackage<TKey, TValue>>();
        private int _DetectSpanInSecs = 10;
        private int _MaxMuteSpanInSecs = 5;
        private BasicCircleEngine _BasicCircleEngine = new BasicCircleEngine();
        private object _locker = new object();
        #endregion

        #region 构造函数
        public HotCache()
        { }
        #endregion

        #region 属性
        public int Count
        {
            get { return this._Dictionary.Count; }
        }

        public int DetectSpanInSecs
        {
            get { return this._DetectSpanInSecs; }
            set
            {
                if (this._DetectSpanInSecs == 0)
                    return;
                this._DetectSpanInSecs = value;
                this._BasicCircleEngine.DetectSpanInSecs = this._DetectSpanInSecs;
            }
        }
        public int MaxMuteSpanInSecs
        {
            get { return this._MaxMuteSpanInSecs; }
            set { this._MaxMuteSpanInSecs = value; }
        }
        #endregion

        #region 方法
        public void Add(TKey key, TValue value)
        {
            lock (this._locker)
            {
                if (this._Dictionary.ContainsKey(key))
                {
                    this._Dictionary.Remove(key);
                }
                this._Dictionary.Add(key, new CachePackage<TKey, TValue>(key, value));
            }
        }

        public void Clear()
        {
            lock (this._locker)
            {
                this._Dictionary.Clear();
            }
        }

        public TValue Get(TKey key)
        {
            TValue result = null;
            lock (this._locker)
            {
                if (this._Dictionary.ContainsKey(key))
                    result = this._Dictionary[key].Value;
            }
            return result;
        }

        public IList<TValue> GetAll()
        {
            IList<CachePackage<TKey, TValue>> lists = this._Dictionary.Values.ToList();
            IList<TValue> result = new List<TValue>(lists.Count);
            foreach (CachePackage<TKey, TValue> value in lists)
                result.Add(value.Value);
            return result;
        }

        public void Initialize()
        {
            if (this._DetectSpanInSecs > 0)
            {
                this._BasicCircleEngine.DetectSpanInSecs = this._DetectSpanInSecs;
                this._BasicCircleEngine.DoWork -= BasicCircleEngine_DoWork;
                this._BasicCircleEngine.DoWork += BasicCircleEngine_DoWork;
                this._BasicCircleEngine.Start();
            }
        }

        private void BasicCircleEngine_DoWork()
        {
            lock (this._locker)
            {
                DateTime now = DateTime.Now;
                IList<TKey> keyLists = new List<TKey>();
                foreach (CachePackage<TKey, TValue> package in this._Dictionary.Values)
                {
                    TimeSpan span = (TimeSpan)(now - package.LastAccessTime);
                    if (span.TotalSeconds > this._MaxMuteSpanInSecs)
                    {
                        keyLists.Add(package.Key);
                    }
                }
                foreach (TKey key in keyLists)
                {
                    this._Dictionary.Remove(key);
                }
            }
        }

        public void Remove(TKey key)
        {
            lock (_locker)
            {
                if (this._Dictionary.ContainsKey(key))
                    this._Dictionary.Remove(key);
            }
        }
        #endregion
    }
}
