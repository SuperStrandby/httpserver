using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class ContentHandler
    {
        private readonly string _exstensions;
        public ContentHandler(string extensions)
        {
            _exstensions = extensions;
        }
        public string Exstensiontype()
        {
            const string output = "content type: ";

            if (_exstensions == ".txt")
            {
                return output + "Text/txt";
            }
            if (_exstensions == ".html")
            {
                return output + "Text/html";
            }
            if (_exstensions == ".doc")
            {
                return output + "Application/msword";
            }
            if (_exstensions == ".gif")
            {
                return output + "Image/gif";
            }
            if (_exstensions == ".jpg")
            {
                return output + "Image/jpeg";
            }
            if (_exstensions == ".pdf")
            {
                return output + "Application/pdf";
            }
            if (_exstensions == ".css")
            {
                return output + "Text/css";
            }
            if (_exstensions == ".Xml")
            {
                return output + "text/xml";
            }
            if (_exstensions == ".jar")
            {
                return output + "application/x-java-archive";
            }

            return output + "application/octet-stream";
        }
    }
}

