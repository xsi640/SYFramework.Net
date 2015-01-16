using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SYFramework.Net.Woker
{
    public class BasicWorker : IWorker
    {
        #region 变量
        private EWorkerStatus _Status = EWorkerStatus.Stopped;
        private Action _Action = null;
        private DateTime _StartTime = DateTime.Now;
        private Exception _LastException = null;
        #endregion

        public event Action<IWorker, EWorkerStatus> StatusChanged;

        #region 属性
        public Action Action
        {
            get { return this._Action; }
            set { this._Action = value; }
        }
        public EWorkerStatus Status
        {
            get { return this._Status; }
            set
            {
                if (this._Status == value)
                    return;
                this._Status = value;
                this.OnStatusChanged();
            }
        }
        public DateTime StartTime
        {
            get { return this._StartTime; }
        }
        public Exception LastException
        {
            get { return this._LastException; }
        }
        #endregion

        #region 方法
        public void Start()
        {
            if (this._Action != null)
            {
                this.Status = EWorkerStatus.Started;
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
            this.Status = EWorkerStatus.Stopped;
        }

        private void OnStatusChanged()
        {
            if (StatusChanged != null)
                this.StatusChanged(this, this.Status);
        }
        #endregion
    }
}
