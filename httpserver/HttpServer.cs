using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        /// <summary>
        /// Define default port
        /// </summary>
        public static readonly int DefaultPort = 8888;
        /// <summary>
        /// The root folder for files
        /// </summary>
        private static readonly string _rootCatalog = @"C:\temp\";
        /// <summary>
        /// Used to define if the server is active
        /// </summary>
        private bool _serverActive;

        /// <summary>
        /// Method to start the TcpListener on the defined DefaultPort and IPAddress.
        /// </summary>
        /// <remarks>Queues the specified work to run on the ThreadPool and returns a task </remarks>
        public void StartServer()
        {
            _serverActive = true;

            TcpListener serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), DefaultPort);
            serverSocket.Start();

            List<Task> serverThreads = new List<Task>();

            while (_serverActive)
            {
                //creates a connectionSocket by accepting the connection request from the client
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                serverThreads.Add(Task.Run(()=>Connection(connectionSocket)));
            }
            serverSocket.Stop();
            
        }

        /// <summary>
        /// Deactivates the server
        /// </summary>
        public void _deactivateServer()
        {
            _serverActive = false;
        }

        /// <summary>
        /// This methods handles the incoming connections on the default port.
        /// </summary>
        /// <param name="connectionSocket"></param>
        public void Connection(TcpClient connectionSocket)
        {
                Console.WriteLine("Client connected on thread " + Thread.CurrentThread.GetHashCode());
                Stream ns = connectionSocket.GetStream();

                var sr = new StreamReader(ns); // Makes a new StreamReader in the variable sr
                var sw = new StreamWriter(ns) { AutoFlush = true }; // Makes a new StreamWriter in the variable sw
            
                    try
                    {
                        string message = sr.ReadLine();
                        string[] words = message.Split(' ');
                        string filename = words[1].Trim('/');
                        string fullfilename = _rootCatalog + filename;

                        if (words.Length == 0)
                        {
                            throw new Exception("Document Empty");
                        }
                        
                        sw.Write(
                            "HTTP/1.0 200 Ok\r\n" +
                            "\r\n" +
                            "Server version 1.0\r\n" +
                            "Date: {0}\r\n", DateTime.Now);

                        sw.Write("You have requested: {0}" + "\r\n", filename);
                        Console.WriteLine("You have requested: " + words[1]);
                        FileStream fs = new FileStream(fullfilename, FileMode.Open, FileAccess.Read);
                        fs.CopyTo(sw.BaseStream);

                    }

                    catch (Exception)
                    {
                        Console.WriteLine("Error");
                    }

                    finally
                    {
                        ns.Close();
                        connectionSocket.Close();
                    }

  
            }
      }
}

