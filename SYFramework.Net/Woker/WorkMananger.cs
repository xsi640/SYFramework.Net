using SYFramework.Net.Collection;
using SYFramework.Net.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Woker
{
    public class WorkMananger
    {
        private ThreadSafeQueue<IWorker> _Queue = new ThreadSafeQueue<IWorker>();
        private ThreadSafeList<IWorker> _RunningLists = new ThreadSafeList<IWorker>();
        private int _MaxWorkingCount = 5;

        public WorkMananger()
        { }

        public int MaxWorkingCount
        {
            get { return this._MaxWorkingCount; }
            set { this._MaxWorkingCount = value; }
        }

        public void Enqueue(IWorker worker)
        {
            if (worker == null)
                return;

            if (!this._Queue.Contains(new Match<IWorker>((w) =>
            {
                return worker.Contains(w);
            })))
            {
                worker.Status = EWorkerStatus.Queue;
                this._Queue.Enqueue(worker);
            }
        }

        private void Scheduling()
        {
            while (this._RunningLists.Count < this._MaxWorkingCount && this._Queue.Count > 0)
            {
                IWorker worker = this._Queue.Dequeue();
                worker.Status = EWorkerStatus.Started;
                worker.StatusChanged += worker_StatusChanged;
                worker.Start();
            }
        }

        void worker_StatusChanged(IWorker worker, EWorkerStatus status)
        {
            if (status == EWorkerStatus.Stopped)
            {
                worker.StatusChanged -= worker_StatusChanged;
                this._RunningLists.Remove(worker);
                this.Scheduling();
            }
        }
    }
}
