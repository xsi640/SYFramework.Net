using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYFramework.Net.Engines
{
    /// <summary>
    /// 循环引擎接口
    /// </summary>
    public interface ICircleEngine
    {
        #region event
        /// <summary>
        /// 循环工作的任务
        /// </summary>
        event Action DoWork;
        #endregion

        #region property
        /// <summary>
        /// 是否已经开始
        /// </summary>
        bool IsStarted { get; }
        /// <summary>
        /// 工作间隔时间
        /// </summary>
        int DetectSpanInSecs { get; set; }
        /// <summary>
        /// 最后发生的异常
        /// </summary>
        Exception LastException { get; }
        #endregion

        #region function
        /// <summary>
        /// 开始
        /// </summary>
        void Start();
        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
        #endregion
    }
}