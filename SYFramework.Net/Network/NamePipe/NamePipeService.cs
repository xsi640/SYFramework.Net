using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SYFramework.Net.Network.NamedPipe
{
    public class NamePipeService
    {
        private const int MAX_CONNECTION = 100;
        private readonly string _PipeName;
        private List<NamePipeConnection> _NamePipeConnection = new List<NamePipeConnection>();
        private int _NextPipeId;
        private bool _IsRunning = false;

        public event Action<NamePipeConnection> ClientConnected;
        public event Action<NamePipeConnection> ClientDisconnected;
        public event Action<NamePipeConnection, byte[]> ReceiveData;
        public event Action<NamePipeConnection, Exception> Error;

        public NamePipeService(string pipeName)
        {
            this._PipeName = pipeName;
        }

        public bool IsRunning
        {
            get { return this._IsRunning; }
        }
        public List<NamePipeConnection> NamePipeConnection
        {
            get { return this._NamePipeConnection; }
        }

        public void Start()
        {
            if (this._IsRunning == false)
            {
                new Task(this.Run, CancellationToken.None, TaskCreationOptions.LongRunning).Start();
            }
        }

        public void Stop()
        {
            this._IsRunning = false;
        }

        private void Run(object stat)
        {
            this._IsRunning = true;
            while (this._IsRunning)
            {
                WaitForConnection();
            }
            this._IsRunning = false;
        }

        private void WaitForConnection()
        {
            try
            {
                this._NextPipeId++;
                NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream(this._PipeName, PipeDirection.InOut, MAX_CONNECTION, PipeTransmissionMode.Byte, PipeOptions.Asynchronous | PipeOptions.WriteThrough);
                namedPipeServerStream.WaitForConnection();
                NamePipeConnection connection = new NamePipeConnection(namedPipeServerStream, this._NextPipeId);
                connection.Disconnected += connection_Disconnected;
                connection.Error += connection_Error;
                connection.ReceiveMessage += connection_ReceiveMessage;
                connection.Open();

                lock (this._NamePipeConnection)
                {
                    this._NamePipeConnection.Add(connection);
                }
                this.OnClientConnected(connection);
            }
            catch (Exception ex)
            {
                this.OnError(null, ex);
            }
        }

        void connection_ReceiveMessage(NamePipeConnection connection, byte[] data)
        {
            this.OnReceiveData(connection, data);
        }

        void connection_Error(NamePipeConnection connection, Exception ex)
        {
            this.OnError(connection, ex);
        }

        void connection_Disconnected(NamePipeConnection connection)
        {
            lock (this._NamePipeConnection)
            {
                this._NamePipeConnection.Remove(connection);
            }

            this.OnClientDisconnected(connection);
        }

        private void OnClientConnected(NamePipeConnection connection)
        {
            if (this.ClientConnected != null)
                this.ClientConnected(connection);
        }

        private void OnClientDisconnected(NamePipeConnection connection)
        {
            if (this.ClientDisconnected != null)
                this.ClientDisconnected(connection);
        }

        private void OnReceiveData(NamePipeConnection connection, byte[] data)
        {
            if (this.ReceiveData != null)
                this.ReceiveData(connection, data);
        }

        private void OnError(NamePipeConnection connection, Exception ex)
        {
            if (this.Error != null)
                this.Error(connection, ex);
        }
    }
}
