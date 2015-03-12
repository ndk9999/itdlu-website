using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ColorLife.Core.Helper;
using System.ServiceModel.Syndication;
using System.Text;
namespace DLUProjectFramework.Mvc
{
    public class RssActionResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }

        private readonly SyndicationFeedFormatter feed;
        public SyndicationFeedFormatter Feed{
            get { return feed; }
        }

        public RssActionResult(SyndicationFeedFormatter feed)
        {
            this.feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/rss+xml";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (feed != null)
                using (var xmlWriter = new XmlTextWriter(response.Output))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    feed.WriteTo(xmlWriter);
                }

            /*
            context.HttpContext.Response.ContentType = "application/rss+xml";

            RssHelper rss = new RssHelper();
            RssChannel channel = new RssChannel();
            channel.title = "Dalat Cooking";
            channel.link = "http://localhost";
            channel.description = "Mo ta";
            channel.copyright = "Copyright " + "Dalat Cooking";
            channel.pubDate = "";
            channel.generator = "tuanitpro@gmail.com";

            channel.managingEditor = "tuannitpro";
            channel.webMaster = "tuanitpro@gmail.com";
            channel.language = "vi-VN";
            channel.ttl = "60";
            channel.category = "";
            channel.docs = "Mo ta";
            channel.imageURL = "Hình Logo";
            rss.AddRssChannel(channel);
            int numofitem = 20;
            /*
           List<Course> list;
           list = CourseManager.FindAll().FindAll(c => c.IsActive == true);
           foreach (var _new in list)
           {
               RssItem _item = new RssItem();
               _item.title = _new.Name;

               _item.link = "localhost/lienhe" ;

               _item.description = "<![CDATA[" + _new.Description + "]]>";
               _item.pubDate = String.Format("{0:r}", _new.DateCreated);
               _item.author = _new.CreatedBy;
               _item.category = CategoryManager.Single(_new.CategoryId).Name;
               // _item.media = "media xxx";
               rss.AddRssItem(_item);
           }
           
            context.HttpContext.Response.Clear();            
            context.HttpContext.Response.Write(rss.RssDocument);
            */
        }
    }
}