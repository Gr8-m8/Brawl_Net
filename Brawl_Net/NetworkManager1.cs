using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Brawl_Net
{
    class NetworkManager
    {
        TcpListener listener;
        TcpClient client;
        int port;


        public NetworkManager()
        {
            port = 7779;
            listener = new TcpListener(IPAddress.Any, port);
        }

        public string Recive()
        {
            string outstring;
            listener.Start();
            client = listener.AcceptTcpClient();

            byte[] byteMessage = new byte[256];
            int byteNum = client.GetStream().Read(byteMessage, 0, byteMessage.Length);
            outstring = Encoding.Unicode.GetString(byteMessage, 0, byteNum);

            client.Close();
            listener.Stop();

            return outstring;
        }

        public void Send(string ipDestination, string message)
        {
            IPAddress adress = IPAddress.Parse(ipDestination);
            client = new TcpClient();
            client.NoDelay = true;
            client.Connect(adress, port);

            if (client.Connected)
            {
                byte[] byteMessage = Encoding.Unicode.GetBytes(message);
                client.GetStream().Write(byteMessage, 0, byteMessage.Length);
                client.Close();
            }
        }

        public string getIP()
        {
            string ip = "";

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                ip = endPoint.Address.ToString();
            }

            return ip;
        }
    }
}
