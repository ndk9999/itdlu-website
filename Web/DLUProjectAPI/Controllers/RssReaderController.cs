using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ServiceModel.Syndication;
using ColorLife.Core.Helper;
using System.Xml;
namespace DLUProjectAPI.Controllers
{
    [Serializable]
    public class BlogModel
    {
        public SyndicationFeed BlogFeed { get; set; }
    }
    public class RssReaderController : ApiController
    {
         [HttpGet]
       
        public BlogModel Get(string url, int take)
        {
            var model = new BlogModel();
            using (XmlReader reader = XmlReader.Create(url))
            {
                SyndicationFeed rssData = SyndicationFeed.Load(reader);
                model.BlogFeed = rssData;
            }
            return model;
        }
    }
}
