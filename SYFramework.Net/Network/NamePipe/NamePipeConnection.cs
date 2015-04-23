using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SYFramework.Net.Network.NamedPipe
{
    public class NamePipeConnection : IDisposable
    {
        private readonly int _Id;
        private readonly string _Name;
        private PipeStream _PipeStream;
        private Queue<byte[]> _Queue = new Queue<byte[]>(50);
        private readonly AutoResetEvent _WriteResetEvent = new AutoResetEvent(false);

        public event Action<NamePipeConnection, byte[]> ReceiveMessage;
        public event Action<NamePipeConnection> Disconnected;
        public event Action<NamePipeConnection, Exception> Error;

        public NamePipeConnection(PipeStream pipeStream)
        {
            this._PipeStream = pipeStream;
        }

        public NamePipeConnection(PipeStream pipeStream, int id)
        {
            this._PipeStream = pipeStream;
            this._Id = id;
        }

        public void Close()
        {
            if (this._PipeStream != null)
            {
                this._PipeStream.Close();
                this._WriteResetEvent.Set();
                this.OnDisconnected();
            }
        }

        public bool IsConnected
        {
            get
            {
                bool result = false;
                if (this._PipeStream != null && this._PipeStream.IsConnected)
                    result = true;
                return result;
            }
        }

        public void Open()
        {
            new Task(this.ReadMessage, CancellationToken.None, TaskCreationOptions.LongRunning).Start();
            new Task(this.WriteMessage, CancellationToken.None, TaskCreationOptions.LongRunning).Start();
        }

        public void PushMessage(byte[] data)
        {
            while (this._Queue.Count > 50)
                Thread.Sleep(1);
            this._Queue.Enqueue(data);
            this._WriteResetEvent.Set();
        }

        private void ReadMessage()
        {
            while (this.IsConnected && this._PipeStream.CanRead)
            {
                const int size = sizeof(int);
                byte[] bLength = new byte[size];
                int read = this._PipeStream.Read(bLength, 0, size);
                if (read == size)
                {
                    int length = BitConverter.ToInt32(bLength, 0);
                    if (length > 0)
                    {
                        byte[] data = new byte[length];
                        this._PipeStream.Read(data, 0, data.Length);
                        this.OnReceiveMessage(data);
                    }
                }
                if (read == 0)
                {
                    this.Close();
                }
            }
        }

        private void WriteMessage()
        {
            while (this.IsConnected && this._PipeStream.CanWrite)
            {
                this._WriteResetEvent.WaitOne();

                while (this._Queue.Count > 0)
                {
                    byte[] data = this._Queue.Dequeue();
                    byte[] bLength = BitConverter.GetBytes(data.Length);
                    this._PipeStream.Write(bLength, 0, bLength.Length);
                    this._PipeStream.Write(data, 0, data.Length);
                    this._PipeStream.Flush();
                    this._PipeStream.WaitForPipeDrain();
                }
            }
        }

        private void OnReceiveMessage(byte[] data)
        {
            if (this.ReceiveMessage != null)
                this.ReceiveMessage(this, data);
        }

        private void OnDisconnected()
        {
            if (this.Disconnected != null)
                this.Disconnected(this);
        }

        private void OnError(Exception ex)
        {
            if (this.Error != null)
                this.Error(this, ex);
        }

        public void Dispose()
        {
            if (this._PipeStream != null)
            {
                this._PipeStream.Close();
                this._PipeStream = null;
            }
            if (this._Queue != null && this._Queue.Count > 0)
                this._Queue.Clear();
        }
    }
}
