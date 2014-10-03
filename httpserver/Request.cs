using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class Request
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public string Protocol { get; set; }

        public Request(String request)
        {
            
        }
    }
}
