using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SYFramework.Net.Engines
{
    /// <summary>
    /// 基本的循环工作引擎
    /// </summary>
    public class BasicCircleEngine : ICircleEngine
    {
        #region 变量
        private bool _IsStarted = false;
        private bool _stopping = false;
        private Exception _LastException = null;
        private int _DetectSpanInSecs = 10;
        public event Action DoWork;
        #endregion

        #region 属性
        public bool IsStarted
        {
            get { return this._IsStarted; }
        }

        public int DetectSpanInSecs
        {
            get { return this._DetectSpanInSecs; }
            set { this._DetectSpanInSecs = value; }
        }
        public Exception LastException
        {
            get { return this._LastException; }
        }
        #endregion

        #region 方法
        public void Start()
        {
            new Action(this.DoWorking).BeginInvoke(null, null);
            this._IsStarted = true;
        }

        public void Stop()
        {
            this._stopping = true;
            this._IsStarted = false;
        }

        private void DoWorking()
        {
            while (true)
            {
                try
                {
                    this.DoWork();
                    Thread.Sleep(this._DetectSpanInSecs);
                    if (!this._stopping)
                        break;
                }
                catch (Exception ex)
                {
                    this._LastException = ex;
                }
            }
            this._stopping = false;
        }
        #endregion
    }
}
