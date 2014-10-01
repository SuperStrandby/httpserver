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
        private static readonly string _rootCatalog = @"C:\temp\";

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
                var sw = new StreamWriter(ns) { AutoFlush = true };

                try
                {
                    string message = sr.ReadLine();


                    sw.Write(
                        "HTTP/1.0 200 Ok\r\n" +
                        "\r\n" +
                        "Hello World");

                    if (message != null)
                    {
                        SendRequestedFile(GetRequestedFilePath(message), sw);
                    }

                }
                finally
                {
                    ns.Close();
                    connectionSocket.Close();
                    serverSocket.Stop();
                }

            }

        }

        private string GetRequestedFilePath(string message)
        {
            string[] messageWords = message.Split(' ');

            return messageWords[1];
        }

        private void SendRequestedFile(string filePath, StreamWriter sw)
        {

            using (FileStream source = File.OpenRead(Path.Combine(_rootCatalog, filePath)))
            {
                byte[] bytes = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (source.Read(bytes, 0, bytes.Length) > 0)
                {
                    sw.Write(temp.GetString(bytes));
                    Console.WriteLine(temp.GetString(bytes));
                }
            }
        }
    }
}
