using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Woker
{
    public interface IWorker
    {
        Action Action { get; set; }
        EWorkerStatus Status { get; }
        DateTime StartTime { get; }
        Exception LastException { get; }
        void Start();
        void Stop();
    }
}
