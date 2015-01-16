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
        private Exception _LastException = null;

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
        public Exception LastException
        {
            get { return this._LastException; }
        }

        public void Start()
        {
            if (this._Action != null)
            {
                this._Status = EWorkerStatus.Started;
                this._StartTime = DateTime.Now;
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    try
                    {
                        this._Action();
                    }
                    catch (Exception ex)
                    {
                        this._LastException = ex;
                    }
                }));
            }
        }

        public void Stop()
        {
            this._Status = EWorkerStatus.Stopped;
        }
    }
}
