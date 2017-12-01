using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;

namespace WindowsApplication1
{
    class Variables
    {
        public static string var = "hey!";
        public static IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
        //public static IPEndPoint ipepServer;
        //public static Socket clientSocket;
        //public static Socket listenerSocket;
        public static TcpClient clientTCP = new TcpClient("localhost", 8080);
        public static NetworkStream myNetworkStream = clientTCP.GetStream();
        
    }
}
