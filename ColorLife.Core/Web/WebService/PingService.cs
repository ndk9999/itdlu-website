#region Using

using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Text;

#endregion

/// <summary>
/// Pings various blog ping services.
/// </summary>
/// 
namespace ColorLife.Core.WebService
{
    public static class PingService
    {
        /// <summary>
        /// Sends a ping to various ping services.
        /// </summary>
        public static void Send()
        {
            Execute("http://ping.feedburner.com");
            Execute("http://rpc.pingomatic.com/RPC2");
        }

        /// <summary>
        /// Creates a web request and with the RPC-XML code in the stream.
        /// </summary>
        private static void Execute(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "text/xml";
                request.Timeout = 3000;

                AddXmlToRequest(request);
                request.GetResponse();
            }
            catch (Exception)
            {
                // Log the error.
            }
        }

        /// <summary>
        /// Adds the XML to web request. The XML is the standard
        /// XML used by RPC-XML requests.
        /// </summary>
        private static void AddXmlToRequest(HttpWebRequest request)
        {
            Stream stream = (Stream)request.GetRequestStream();
            using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.ASCII))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("methodCall");
                writer.WriteElementString("methodName", "weblogUpdates.ping");
                writer.WriteStartElement("params");
                writer.WriteStartElement("param");
                // Add the name of your website here
                writer.WriteElementString("value", "The name of your website");
                writer.WriteEndElement();
                writer.WriteStartElement("param");
                // The absolute URL of your website - not the updated or new page
                writer.WriteElementString("value", "http://www.example.com");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

    }
}