using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SYFramework.Net.Network.NamedPipe
{
    public class NamePipeClient
    {
        private readonly string _PipeName;
        private NamePipeConnection _Connection;
        private bool _IsAutoReconnect = false;

        public event Action<NamePipeClient> Disconnected;
        public event Action<NamePipeClient> Connected;
        public event Action<NamePipeClient, Exception> Error;
        public event Action<NamePipeClient, byte[]> ReceiveData;

        public NamePipeClient(string pipeName)
        {
            this._PipeName = pipeName;
        }

        public void Connection()
        {
            new Task(this.Connecting, CancellationToken.None, TaskCreationOptions.LongRunning).Start();
        }

        private void Connecting(object stat)
        {
            try
            {
                NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", this._PipeName, PipeDirection.InOut, PipeOptions.Asynchronous | PipeOptions.WriteThrough);
                namedPipeClientStream.Connect();
                this._Connection = new NamePipeConnection(namedPipeClientStream);
                this._Connection.ReceiveMessage += _Connection_ReceiveMessage;
                this._Connection.Error += _Connection_Error;
                this._Connection.Disconnected += _Connection_Disconnected;
                this._Connection.Open();
            }
            catch (Exception ex)
            {
                if (this.Error != null)
                    this.Error(this, ex);
            }

            if (this.Connected != null)
                this.Connected(this);
        }

        public void PushMessage(byte[] data)
        {
            this._Connection.PushMessage(data);
        }

        public void Disconnect()
        {
            if(this._Connection != null)
            {
                this._Connection.Close();

                if (this.Disconnected != null)
                    this.Disconnected(this);
            }
        }

        void _Connection_Disconnected(NamePipeConnection connection)
        {
            if (this.Disconnected != null)
                this.Disconnected(this);

            if (this._IsAutoReconnect)
            {
                this.Connection();
            }
        }

        void _Connection_Error(NamePipeConnection connection, Exception ex)
        {
            if (this.Error != null)
                this.Error(this, ex);
        }

        void _Connection_ReceiveMessage(NamePipeConnection connection, byte[] data)
        {
            if (this.ReceiveData != null)
                this.ReceiveData(this, data);
        }
    }
}
