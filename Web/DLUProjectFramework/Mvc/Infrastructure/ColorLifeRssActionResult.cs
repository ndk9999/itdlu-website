using ColorLife.Core.Helper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DLUProjectFramework.Mvc
{
    public class ColorLifeRssActionResult : ActionResult
    {
        public List<SyndicationFeedItem> Items { get; set; }
        public SyndicationFeedChannel RssChanel { get; set; }
        public ColorLifeRssActionResult(List<SyndicationFeedItem> items)
        {
            this.Items = items;
        }
        public ColorLifeRssActionResult(SyndicationFeedChannel chanel, List<SyndicationFeedItem> items)
        {
            this.RssChanel = chanel;
            this.Items = items;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";

            /*
            SyndicationFeedChannel channel = new SyndicationFeedChannel();
            channel.title = Setting.String("SITE_NAME");
            channel.link = Setting.String("SITE_URL");
            channel.description = Setting.String("SITE_METADESC");
            channel.copyright = "Copyright " + Setting.String("SITE_NAME"); ;
            channel.pubDate = "";
            channel.generator = "tuanitpro@gmail.com";

            channel.managingEditor = "tuannitpro";
            channel.webMaster = "tuanitpro@gmail.com";
            channel.language = "vi-VN";
            channel.ttl = "60";
            channel.category = "";
            channel.docs = Setting.String("SITE_METADESC");
            channel.imageURL = Setting.String("SITE_URL") + Setting.String("SITE_LOGO");
            rss.AddRssChannel(channel);
             */

            SyndicationFeedHelper rss = new SyndicationFeedHelper(RssChanel, Items);

            //foreach (var item in Items)
            //{
            //    //RssItem _item = new RssItem();               
            //    rss.AddRssItem(item);
            //}

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.Write(rss.RssDocument);
        }
    }
}