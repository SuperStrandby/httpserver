using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        /// <summary>
        /// Define our default port
        /// </summary>
        public static readonly int DefaultPort = 8888;
        /// <summary>
        /// The root folder of our files
        /// </summary>
        private static readonly string _rootCatalog = @"C:\temp\";

        /// <summary>
        /// Method to start our webserver
        /// </summary>
        public void StartServer()
        {
            while (true)
            {
                var serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), DefaultPort);
                serverSocket.Start();

                //creates a connectionSocket by accepting the connection request from the client
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated");
                Stream ns = connectionSocket.GetStream();

                var sr = new StreamReader(ns);
                var sw = new StreamWriter(ns) {AutoFlush = true};

                
                
                    try
                    {
                        string message = sr.ReadLine();
                        string[] words = message.Split(' ');
                        string filename = words[1];
                        string fullfilename = _rootCatalog + filename;

                        if (words.Length == 0)
                        {
                            throw new Exception("Document Empty");
                        }

                        sw.WriteLine("You have requested: {0}", filename);
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
                        serverSocket.Stop();
                    }

                    

                    
                    //sw.Write(
                    //    "HTTP/1.0 200 Ok\r\n" +
                    //    "\r\n" +
                    //    " Hello World ");

                
            }
        }
    }
}
