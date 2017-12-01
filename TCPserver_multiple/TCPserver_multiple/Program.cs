using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace TCPserver_multiple
{
    class Program
    {
        public static IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
        public static IPEndPoint ipepServer;
        public static Socket clientSocket;
        public static Socket listenerSocket;
        public static List<Entry> myTable = new List<Entry>();

        static void Main(string[] args)
        {
            string hostname = Dns.GetHostName();
            IPHostEntry IPhost = Dns.GetHostEntry(hostname);
            Console.WriteLine("Server started. Listening on " + IPhost.AddressList[0].ToString() + ":8080");

            Thread thdTCPServer = new Thread(new ThreadStart(listenerThread));
            thdTCPServer.Start();
        }

        private static void listenerThread()
        {
            ipepServer = new IPEndPoint(IPAddress.Any, 8080);
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(ipepServer);
            listenerSocket.Listen(50);
            
            while (true)
            {
                clientSocket = listenerSocket.Accept();
                if (clientSocket.Connected)
                {
                    Thread thread = new Thread(new ThreadStart(handlerThread));
                    thread.Start();
                }
            }
        }

        /*static void startTimer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer(500);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            aTimer.Elapsed += new ElapsedEventHandler(handlerThread);
            // Set the Interval to 500 milliseconds.
            //aTimer.Interval = 500;
        }*/

        private static void handlerThread()
        {
            byte[] recv = new byte[256];
            NetworkStream networkStream = new NetworkStream(clientSocket);
            int x = 0;
            try
            {
                while ((x = networkStream.Read(recv, 0, 256)) != 0)
                {
                    string recMessage = Encoding.ASCII.GetString(recv, 0, x);
                    char[] UserName = recMessage.ToCharArray();
                    char[] UserIP = clientSocket.RemoteEndPoint.ToString().ToCharArray();

                    Entry myEntry = new Entry();
                    myEntry.Name = new string(UserName);
                    myEntry.IP = new string(UserIP);

                    myTable.Add(myEntry);  // add users/ips to the table
                    
                    // show entries on console
                    if (clientSocket.Connected)
                    {
                        foreach (Entry feEntry in myTable)
                        {
                            byte[] myWriteBuffer = new byte[256];
                            Console.WriteLine(feEntry.ToString());
                            string rec = feEntry.ToString();
                            myWriteBuffer = Encoding.ASCII.GetBytes(rec);
                            networkStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                        }
                        Console.WriteLine("--------");
                    }
                }

            }
            catch
            {
                Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + "Client disconnected");
                //clientSocket.Close();
            }

            // Send buddy update to all connected clients
            //byte[] myWriteBuffer = new byte[256];
            TcpClient SclientSocket = new TcpClient("localhost", 8080);
            NetworkStream myNetworkStream = SclientSocket.GetStream();
  
            foreach (Entry feEntry in myTable)
            {
            }
        }
    }

    public class Entry
    {
        public string Name;
        public string IP;

        public override string ToString()
        {
            string returnStr = string.Empty;
            returnStr += Name + " ";
            returnStr += IP + " ";

            return returnStr;
        }
    }

}
