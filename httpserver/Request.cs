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
           
            var words = request.Split(' ');
            Method = words[0];
            Uri = words[1].Trim('/');
            Protocol = words[2];

        }
    }
}
