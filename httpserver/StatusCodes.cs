using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class StatusCodes
    {
        public string text = "";
        
        public string FileNotFound()
        {
            text += 
            "HTTP/1.0 404 Not found\r\n" +
            "\r\n" +
            "File not found";
            return text;
        }

        public string BadRequest()
        {
            text +=
                "HTTP/1.0 400 Bad Request\r\n" +
                "\r\n" +
                "Bad Request did not send a Get or Post request";
            return text;
        }

        public string InvalidProtocol()
        {
            text += 
                "HTTP/1.0 400 Bad Request\r\n" +
                "\r\n" +
                "The Http protocol you have chosen are invalid";
            return text;
        }
    }
}
