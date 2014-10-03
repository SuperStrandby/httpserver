using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
        /// Logging
        /// </summary>
        readonly EventLog _mylog = new EventLog();

        /// <summary>
        /// Method to start the TcpListener on the defined DefaultPort and IPAddress.
        /// </summary>
        /// <remarks>Queues the specified work to run on the ThreadPool and returns a task </remarks>
        public void StartServer()
        {
            _mylog.Source = "my webserver";
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
            _mylog.WriteEntry("Server started", EventLogEntryType.Information, 1);
            Console.WriteLine("Client connected on thread " + Thread.CurrentThread.GetHashCode());
            Stream ns = connectionSocket.GetStream();

            var sr = new StreamReader(ns); // Makes a new StreamReader in the variable sr
            var sw = new StreamWriter(ns) { AutoFlush = true }; // Makes a new StreamWriter in the variable sw
            
                    try
                    {
                        _mylog.WriteEntry("Request accepted", EventLogEntryType.Information, 2);
                        
                        string requestLine = sr.ReadLine();
                        Request req = new Request(requestLine);
                        
                        
                        string fileName = req.Uri;
                        string fullfilename = _rootCatalog + fileName;
                        string extensions = Path.GetExtension(fullfilename);
                        ContentHandler type = new ContentHandler(extensions);
                        StatusCodes status = new StatusCodes();
                       
                        
                        
                        
                        if (req.Protocol != "HTTP/1.0" && req.Protocol != "HTTP/1.1") //Performs a check to see if its the correct protocol
                        {
                            sw.Write(status.InvalidProtocol()); 
                        }

                        if (req.Method != "GET" && req.Method != "POST") //Performs a check to see if its the correct method
                        {
                            sw.Write(status.BadRequest());
                        }

                        if (File.Exists(fullfilename)) //Performs a check to see if file exists
                        {
                            sw.Write(
                                "HTTP/1.0 200 Ok\r\n" +
                                "\r\n" +
                                "Server version 1.0\r\n" +
                                "You have requested: {1}" + "\r\n" +
                                "Date: {0}\r\n", DateTime.Now, fileName);

                        }
                        else
                        {
                            sw.Write(status.FileNotFound()); //if file is not found then writes 404 status code
                            
                        }
                       

                        
                        Console.WriteLine("You have requested: " + req.Uri);
                        Console.WriteLine(type.Exstensiontype());
                        FileStream fs = new FileStream(fullfilename, FileMode.Open, FileAccess.Read); 
                        fs.CopyTo(sw.BaseStream);

                    }

                    catch (Exception)
                    {
                        _mylog.WriteEntry("Error", EventLogEntryType.Error, 3);
                        Console.WriteLine("Error");
                    }

                    finally
                    {
                        _mylog.WriteEntry("Server shutdown", EventLogEntryType.Information, 4);
                        ns.Close();
                        connectionSocket.Close();
                    }

            
  
            }

        

      }// end of class
} //end of namespace

