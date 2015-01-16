using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Collection
{
    public class ThreadSafeQueue<T> where T : class
    {
        private Queue<T> _Queue = new Queue<T>();
        private object _Locker = new object();

        public int Count
        {
            get { return this._Queue.Count; }
        }

        public void Enqueue(T worker)
        {
            lock (this._Locker)
            {
                if (!this._Queue.Contains(worker))
                    this._Queue.Enqueue(worker);
            }
        }

        public T Dequeue()
        {
            T result = default(T);
            lock (this._Locker)
            {
                result = this._Queue.Dequeue();
            }
            return result;
        }

        public bool Contains(T t)
        {
            return this._Queue.Contains(t);
        }

        public bool Contains(Match<T> match)
        {
            bool result = false;
            lock(this._Locker)
            {
                foreach(T t in this._Queue)
                {
                    if (match(t))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
