using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Woker
{
    public interface IWorker
    {
        event Action<IWorker, EWorkerStatus> StatusChanged;
        Action Action { get; set; }
        EWorkerStatus Status { get; set; }
        DateTime StartTime { get; }
        Exception LastException { get; }
        void Start();
        void Stop();

        bool Contains(IWorker worker);
    }
}
