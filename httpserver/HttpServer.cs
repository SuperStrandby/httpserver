using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        public static readonly int DefaultPort = 8888;

        public void StartServer()
        {
            while (true)
            {
                var serverSocket = new TcpListener(DefaultPort);
                serverSocket.Start();

                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated");

                Stream ns = connectionSocket.GetStream();
                var sr = new StreamReader(ns);
                var sw = new StreamWriter(ns) {AutoFlush = true};



                try
                {
                    string message = sr.ReadLine();
                    string[] words = message.Split(' ');
                    string message1 = words[1].Replace("/", "");

                    sw.Write(
                        "HTTP/1.0 200 Ok\r\n" +
                        "\r\n" + 
                        "You have requested file: {0}", message1);
                    
                    
                }
                finally
                {
                    ns.Close();
                    connectionSocket.Close();
                    serverSocket.Stop();
                }

                

                
            }

        }
    }
}
