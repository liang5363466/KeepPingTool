using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace KeepPingTool
{
    public class NetMessage
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="content"></param>
        public static void Send(string ip,int port)
        {

            byte[] buffer = Encoding.UTF8.GetBytes("0");
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(IPAddress.Parse(ip), port);
            socket.Send(buffer);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="content"></param>
        public static void KeepSend(string ip, int port)
        {

            byte[] buffer = Encoding.UTF8.GetBytes("0");
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            connect_ip:
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(IPAddress.Parse(ip), port);
                while (true)
                {
                    socket.Send(buffer);
                }
            }
            catch
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                goto connect_ip;
            }
        }
    }
}
