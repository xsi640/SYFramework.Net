using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Cache
{
    public class StaticCache<TKey, TValue> : IStaticCache<TKey, TValue> where TValue : class
    {
        private IDictionary<TKey, TValue> _Dictionary = new Dictionary<TKey, TValue>();
        private object _Locker = new object();

        public int Count
        {
            get { return this._Dictionary.Count; }
        }

        public void Add(TKey key, TValue value)
        {
            lock (this._Locker)
            {
                if (this._Dictionary.ContainsKey(key))
                    this._Dictionary.Remove(key);
                this._Dictionary.Add(key, value);
            }
        }

        public IList<TValue> GetAll()
        {
            return this._Dictionary.Values.ToList();
        }

        public void Remove(TKey key)
        {
            lock (this._Locker)
            {
                if (this._Dictionary.ContainsKey(key))
                    this._Dictionary.Remove(key);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue result = null;
                if (this._Dictionary.ContainsKey(key))
                    result = this._Dictionary[key];
                return result;
            }
        }
    }
}
