using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server online");
            var server1 = new HttpServer();
            server1.StartServer();
        }
    }
}
