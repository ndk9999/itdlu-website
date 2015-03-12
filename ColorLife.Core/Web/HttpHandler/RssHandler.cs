using System.Web;
using ColorLife.Core.Helper;
namespace ColorLife.Core.HttpHandler
{
    public class RssHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            int chanelID;
            string type = context.Request.QueryString["Type"];
            if (context.Request.QueryString["ChanelID"] != null)
                chanelID = ConvertType.ToInt(context.Request.QueryString["ChanelID"]);
            else chanelID = -1;
            switch (type)
            {
                default:
                    DisplayRssCourse(context, -1);
                    break;
                case "Content":
                    DisplayRssCourse(context, chanelID);
                    break;
                case "Service":
                    DisplayRssCourse(context, chanelID);
                    break;
            }
        }
     
        void DisplayRssCourse(HttpContext context,int categoryID)
        {
            SyndicationFeedHelper rss = new SyndicationFeedHelper();
            SyndicationFeedChannel channel = new SyndicationFeedChannel();
            channel.title = "Dalat Cooking";
            channel.link = "http://localhost" ;
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
             * */
            context.Response.Clear();
            context.Response.ContentType = "application/rss+xml";
            context.Response.Write(rss.RssDocument);
            context.Response.End();
        }
    }
}
