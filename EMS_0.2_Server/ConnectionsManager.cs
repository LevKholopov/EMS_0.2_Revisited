﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using EMS_Library;
using EMS_Library.Network;

namespace EMS_Server
{
    internal class ConnectionsManager
    {
        static List<MyConnection> connections=new List<MyConnection>();
        public static void Listen()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(Config.ServerIP), Config.ServerPort);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() =>
                {
                    MyConnection connection = new MyConnection(client);
                    connection.ClientFinished += OnClientFinished;

                    connections.Add(connection);
                    connection.ReadData();
                });
            }

        }
        static void OnClientFinished(object sender, EventArgs e)
        {
            if (sender is MyConnection)
            {
                MyConnection[] arr = connections.FindAll(x => x._tcpClient.Client!=null && (sender as MyConnection)._tcpClient.Client.RemoteEndPoint.ToString().Split(':')[0] == x._tcpClient.Client.RemoteEndPoint.ToString().Split(":")[0]).ToArray();
                foreach (MyConnection connection in arr)
                    connection.Terminate();
                return;
            }
        }





        class MyConnection
        {
            public bool _busy = false;
            public TcpClient _tcpClient;
            protected DataPacket _request;
            protected DataPacket _responce;
            protected NetworkStream _stream;
           //protected System.Timers.Timer _timer;

            public event EventHandler ClientFinished;

            public MyConnection(TcpClient client)
            {
                EMS_ServerMainScreen.serverForm.WriteToServerConsole("Accepted client: " + client.Client.RemoteEndPoint);
                _tcpClient = client;
                _stream = _tcpClient.GetStream();
                /*
                _timer = new System.Timers.Timer(60000);
                _timer.Elapsed += OnClientFinished;
                _timer.Start();
                */
            }

            public void ReadData()
            {
                _busy = true;
                _request = new DataPacket(_stream);
                EMS_ServerMainScreen.serverForm.WriteToServerConsole("Recieved request: "+_request);
                switch (_request.StringData.ToLower())
                {
                    case "ping": { DataPacket responce = new DataPacket("ping"); _stream.Write(responce.Write(), 0, responce.GetTotalSize()); Thread.Sleep(10); OnClientFinished(this, EventArgs.Empty); return; }
                    case "done":
                        {
                            //EMS_ServerMainScreen.serverForm.WriteToServerConsole("");
                            DataPacket responce = new DataPacket("terminated");
                            _stream.Write(responce.Write(), 0, responce.GetTotalSize());
                            Thread.Sleep(10);
                            ClientFinished?.Invoke(this, EventArgs.Empty);
                            return;
                        }
                    default:
                        {
                            _busy = true;
                            _responce = new MyRouter().Router(_request);
                            _stream.Write(_responce.Write(), 0, _responce.GetTotalSize());
                            return;
                        }
                }
            }

            public void Terminate()
            {
                EMS_ServerMainScreen.serverForm.WriteToServerConsole("Terminating: "+_tcpClient.Client.RemoteEndPoint);
                _stream.Dispose();
                _tcpClient.Dispose();
            }

        }
    }
}