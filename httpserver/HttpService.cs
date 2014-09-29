using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    internal class HttpService
    {
        private TcpClient connectionSocket;

        public HttpService(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }

        internal void SocketHandler()
        {
            Stream ns = connectionSocket.GetStream();
            var sr = new StreamReader(ns);
            var sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            while (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Client: " + message);
                string answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();

            }
            connectionSocket.Close();
        }
    }
}
