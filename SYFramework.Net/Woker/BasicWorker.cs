using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SYFramework.Net.Woker
{
    public class BasicWorker : IWorker
    {
        private EWorkerStatus _Status = EWorkerStatus.Stopped;
        private Action _Action = null;
        private DateTime _StartTime = DateTime.Now;

        public Action Action
        {
            get { return this._Action; }
            set { this._Action = value; }
        }

        public EWorkerStatus Status
        {
            get { return this._Status; }
        }
        public DateTime StartTime
        {
            get { return this._StartTime; }
        }

        public void Start()
        {
            if (this._Action != null)
            {
                this._Status = EWorkerStatus.Started;
                this._StartTime = DateTime.Now;
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    this._Action();
                }));
            }
        }

        public void Stop()
        {
            this._Status = EWorkerStatus.Stopped;
        }
    }
}
