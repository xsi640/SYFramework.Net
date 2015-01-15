using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Collection
{
    /// <summary>
    /// 线程安全的键、值对集合
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    /// <typeparam name="TListType"></typeparam>
    public class ThreadSafeLookup<TIndex, TListType> : Lockable
    {
        public Dictionary<TIndex, TListType> _Lookup = new Dictionary<TIndex, TListType>();
        public TListType[] ArrayOfItems { get { return AllItems.ToArray(); } }
        public int Count { get { return _Lookup.Count; } }

        public bool ContainsKey(TIndex key) { return _Lookup.ContainsKey(key); }
        public bool TryGetValue(TIndex key, out TListType items) { return _Lookup.TryGetValue(key, out items); }

        public List<TListType> AllItems
        {
            get
            {
                List<TListType> items = new List<TListType>(_Lookup.Count);

                AquireLock();
                {
                    foreach (KeyValuePair<TIndex, TListType> kvp in _Lookup)
                        items.Add(kvp.Value);
                }
                ReleaseLock();

                return items;
            }
        }

        public bool Add(TIndex key, TListType item)
        {
            bool found = true;

            AquireLock();
            {
                if (!_Lookup.ContainsKey(key))
                {
                    _Lookup.Add(key, item);
                    found = false;
                }
            }
            ReleaseLock();

            return found;
        }

        public void AddOrSet(TIndex key, TListType item)
        {
            if (_Lookup.ContainsKey(key))
            {
                _Lookup[key] = item;
            }
            else
            {
                Add(key, item);
            }
        }

        public TListType this[TIndex key]
        {
            get
            {
                TListType item;

                // Not totally safe for lookups that frequently add/remove items.
                // For thread safety, surround the following line with AquireLock(); / ReleaseLock();
                if (!_Lookup.TryGetValue(key, out item)) item = default(TListType);

                return item;
            }
        }

        public bool Remove(TIndex key, TListType item)
        {
            bool removed = false;

            if (!_Lookup.ContainsKey(key))
                return false;

            AquireLock();
            {
                if (_Lookup.ContainsKey(key))
                {
                    _Lookup.Remove(key);
                    removed = true;
                }
            }
            ReleaseLock();

            return removed;
        }

        public bool Remove(TIndex key)
        {
            bool removed = false;

            if (!_Lookup.ContainsKey(key))
                return false;

            AquireLock();
            {
                if (_Lookup.ContainsKey(key))
                {
                    _Lookup.Remove(key);
                    removed = true;
                }
            }
            ReleaseLock();

            return removed;
        }

        public int Remove(Match<TListType> match)
        {
            int removed = 0;
            AquireLock();
            {
                List<TIndex> removeList = new List<TIndex>();
                foreach (KeyValuePair<TIndex, TListType> kvp in _Lookup)
                {
                    if (match(kvp.Value))
                    {
                        removeList.Add(kvp.Key);
                    }
                }
                if (removeList.Count > 0)
                {
                    foreach (TIndex index in removeList)
                    {
                        if (_Lookup.ContainsKey(index))
                        {
                            _Lookup.Remove(index);
                            removed++;
                        }
                    }
                }
            }
            ReleaseLock();
            return removed;
        }

        public void Clear()
        {
            AquireLock();
            {
                _Lookup.Clear();
            }
            ReleaseLock();
        }

        public TListType Find(Match<TListType> match)
        {
            TListType item = default(TListType);
            AquireLock();
            {
                foreach (KeyValuePair<TIndex, TListType> kvp in _Lookup)
                {
                    if (match(kvp.Value))
                    {
                        item = kvp.Value;
                        break;
                    }
                }
            }
            ReleaseLock();

            return item;
        }

        public List<TListType> FindMultiple(Match<TListType> match)
        {
            List<TListType> items = new List<TListType>();
            AquireLock();
            {
                foreach (KeyValuePair<TIndex, TListType> kvp in _Lookup)
                {
                    if (match(kvp.Value))
                    {
                        items.Add(kvp.Value);
                    }
                }
            }
            ReleaseLock();

            return items;
        }
    }
}
