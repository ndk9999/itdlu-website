/*  
 * Document http://cyber.law.harvard.edu/rss/rss.html#sampleFiles
 * Created by tuanitpro.com
 * Website: http://tuanitpro.com
 * Email: tuanitpro@gmail.com
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
namespace ColorLife.Core.Helper
{
    /*
    public class RssChannel
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }

        public string language { get; set; }
        public string copyright { get; set; }
        public string managingEditor { get; set; }
        public string webMaster { get; set; }
        public string pubDate { get; set; }
        public string lastBuildDate { get; set; }
        public string category { get; set; }
        public string generator { get; set; }
        public string docs { get; set; }
        public string cloud { get; set; }
        public string ttl { get; set; } // ttl stands for time to live. It's a number of minutes that indicates how long a channel can be cached before refreshing from the source
        public string image { get; set; }
        public string imageTitle { get; set; }
        public string imageURL { get; set; }
        public string imageWidth { get; set; }
        public string imageHeight { get; set; }
        public string imageLink { get; set; }
        public string rating { get; set; }
        public string textInput { get; set; }
        public string skipHours { get; set; }
        public string skipDays { get; set; }
        public string media { get; set; }
        
        public RssChannel() { }
    }
    public class RssItem
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }

        public string author { get; set; }
        public string category { get; set; }
        public string content { get; set; }
        public string comments { get; set; }
        public string enclosure { get; set; }
        public string guid { get; set; }
        public string pubDate { get; set; }
        public string source { get; set; }
        public string media { get; set; }
        public string mediaUrl { get; set; }
        public string thumbnailUrl { get; set; }
    }
    public class RssHelper
    {
        XmlDocument _rss = null;

        // Craete Channel Struct
        public string media = "http://search.yahoo.com/mrss/";
        public string atom = "";
        public string jwplayer = "http://developer.longtailvideo.com/trac/";

        public RssHelper()
        {
            _rss = new XmlDocument();
            XmlDeclaration xmlDeclaration = _rss.CreateXmlDeclaration("1.0", "utf-8", null);
            _rss.InsertBefore(xmlDeclaration, _rss.DocumentElement);

            XmlElement xmlEle = _rss.CreateElement("rss");
            XmlAttribute xmlAttrVersion = _rss.CreateAttribute("version");

            xmlAttrVersion.InnerText = "2.0";
            xmlEle.Attributes.Append(xmlAttrVersion);

            // For JWPlayer Media Playlist
            XmlAttribute xmlAttrMedia = _rss.CreateAttribute("xmlns:media");
            xmlAttrMedia.InnerText = media;
            xmlEle.Attributes.Append(xmlAttrMedia);


            XmlAttribute xmlAttrJwplayer = _rss.CreateAttribute("xmlns:jwplayer");
            xmlAttrJwplayer.InnerText = jwplayer;
            xmlEle.Attributes.Append(xmlAttrJwplayer);
            _rss.AppendChild(xmlEle);
        }

        public XmlDocument addRssChannel(XmlDocument xmlDocument, RssChannel channel)
        {
            XmlElement channelElement = xmlDocument.CreateElement("channel");
            XmlNode rssElement = xmlDocument.SelectSingleNode("rss");
            rssElement.AppendChild(channelElement);

            XmlElement titleElement = xmlDocument.CreateElement("title");
            titleElement.InnerText = channel.title;
            channelElement.AppendChild(titleElement);

            XmlElement linkElement = xmlDocument.CreateElement("link");
            linkElement.InnerText = channel.link;
            channelElement.AppendChild(linkElement);

            XmlElement descriptionElement = xmlDocument.CreateElement("description");
            descriptionElement.InnerText = channel.description;
            channelElement.AppendChild(descriptionElement);

            XmlElement generatorElement = xmlDocument.CreateElement("generator");
            generatorElement.InnerText = channel.generator;
            channelElement.AppendChild(generatorElement);

            XmlElement languageElement = xmlDocument.CreateElement("language");
            languageElement.InnerText = channel.language;
            channelElement.AppendChild(languageElement);

            XmlElement copyrightElement = xmlDocument.CreateElement("copyright");
            copyrightElement.InnerText = channel.copyright;
            channelElement.AppendChild(copyrightElement);

            XmlElement managingElement = xmlDocument.CreateElement("managingEditor");
            managingElement.InnerText = channel.managingEditor;
            //channelElement.AppendChild(managingElement);

            XmlElement webMasterElement = xmlDocument.CreateElement("webMaster");
            webMasterElement.InnerText = channel.webMaster;
            channelElement.AppendChild(webMasterElement);


            XmlElement categoryElement = xmlDocument.CreateElement("category");
            categoryElement.InnerText = channel.category;
            channelElement.AppendChild(categoryElement);



            XmlElement ttlElement = xmlDocument.CreateElement("ttl");
            ttlElement.InnerText = channel.ttl;
            //  channelElement.AppendChild(ttlElement);

            XmlElement docsElement = xmlDocument.CreateElement("docs");
            docsElement.InnerText = channel.docs;
            //  channelElement.AppendChild(docsElement);


            XmlElement pubDateElement = xmlDocument.CreateElement("pubDate");
            pubDateElement.InnerText = channel.pubDate;
            channelElement.AppendChild(pubDateElement);

            // XmlElement lastBuildDateElement = xmlDocument.CreateElement("lastBuildDate");
            //  lastBuildDateElement.InnerText = channel.lastBuildDate;
            //  channelElement.AppendChild(lastBuildDateElement);

            // XmlElement cloudElement = xmlDocument.CreateElement("cloud");
            //  cloudElement.InnerText = channel.cloud;
            // channelElement.AppendChild(cloudElement);

            //        XmlElement ratingElement = xmlDocument.CreateElement("rating");
            //      ratingElement.InnerText = channel.rating;
            //  channelElement.AppendChild(ratingElement);

            //    XmlElement textInputElement = xmlDocument.CreateElement("textInput");
            //  textInputElement.InnerText = channel.textInput;
            //   channelElement.AppendChild(textInputElement);

            // XmlElement skipHoursElement = xmlDocument.CreateElement("skipHours");
            // skipHoursElement.InnerText = channel.skipHours;
            //  channelElement.AppendChild(skipHoursElement);

            // XmlElement skipDaysElement = xmlDocument.CreateElement("skipDays");
            // skipDaysElement.InnerText = channel.skipDays;
            //  channelElement.AppendChild(skipDaysElement);



            XmlElement imageElement = xmlDocument.CreateElement("image");
            imageElement.InnerText = channel.image;
            XmlElement imageURL = xmlDocument.CreateElement("url");
            imageURL.InnerText = channel.imageURL;
            imageElement.AppendChild(imageURL);
            XmlElement imageTitle = xmlDocument.CreateElement("title");
            imageTitle.InnerText = channel.imageTitle;
            imageElement.AppendChild(imageTitle);
            XmlElement imageLink = xmlDocument.CreateElement("link");
            imageLink.InnerText = channel.imageLink;
            imageElement.AppendChild(imageLink);
            XmlElement imageWidth = xmlDocument.CreateElement("width");
            imageWidth.InnerText = channel.imageWidth;
            imageElement.AppendChild(imageWidth);

            XmlElement imageHeight = xmlDocument.CreateElement("height");
            imageHeight.InnerText = channel.imageHeight;
            imageElement.AppendChild(imageHeight);

            channelElement.AppendChild(imageElement);

            // XmlElement media = xmlDocument.CreateElement("media");
            

            return xmlDocument;
        }
        public XmlDocument addRssItem(XmlDocument xmlDocument, RssItem item)
        {
            XmlElement itemElement = _rss.CreateElement("item");
            XmlNode channelElement = _rss.SelectSingleNode("rss/channel");

            XmlElement titleElement = xmlDocument.CreateElement("title");
            titleElement.InnerText = item.title;
            itemElement.AppendChild(titleElement);

            XmlElement linkElement = xmlDocument.CreateElement("link");
            linkElement.InnerText = item.link;
            itemElement.AppendChild(linkElement);

            XmlElement descriptionElement = xmlDocument.CreateElement("description");
            descriptionElement.InnerText = item.description;
            itemElement.AppendChild(descriptionElement);

            XmlElement authorElement = xmlDocument.CreateElement("author");
            authorElement.InnerText = item.author;
            itemElement.AppendChild(authorElement);

            XmlElement categoryElement = xmlDocument.CreateElement("category");
            categoryElement.InnerText = item.category;
            itemElement.AppendChild(categoryElement);

            XmlElement contentElement = xmlDocument.CreateElement("content");
            contentElement.InnerText = item.content;
            itemElement.AppendChild(contentElement);

            XmlElement commentsElement = xmlDocument.CreateElement("comments");
            commentsElement.InnerText = item.comments;
            itemElement.AppendChild(commentsElement);

            // XmlElement enclosureElement = xmlDocument.CreateElement("enclosure");
            // enclosureElement.InnerText = item.enclosure;
            //  itemElement.AppendChild(enclosureElement);

            // XmlElement guidElement = xmlDocument.CreateElement("guid");
            // guidElement.InnerText = item.guid;
            //   itemElement.AppendChild(guidElement);

            XmlElement pubDateElement = xmlDocument.CreateElement("pubDate");
            pubDateElement.InnerText = item.pubDate;
            itemElement.AppendChild(pubDateElement);

            // XmlElement sourceElement = xmlDocument.CreateElement("source");
            // sourceElement.InnerText = item.source;
            //  itemElement.AppendChild(sourceElement);

            XmlElement mediaElement = xmlDocument.CreateElement("media", "content", media);


            XmlAttribute mediaUrlAttribute = xmlDocument.CreateAttribute("url");    // Set Attr
            mediaUrlAttribute.InnerText = item.mediaUrl;                               // Set Attr
            XmlAttribute mediaDurationAttribute = xmlDocument.CreateAttribute("duration");
            mediaDurationAttribute.InnerText = "33";
           // mediaElement.InnerText = item.media;
            mediaElement.Attributes.Append(mediaUrlAttribute);
            mediaElement.Attributes.Append(mediaDurationAttribute);
            itemElement.AppendChild(mediaElement);


            XmlElement thumbnailElement = xmlDocument.CreateElement("media", "thumbnail", media);
            thumbnailElement.SetAttribute("url", item.thumbnailUrl);   // Set Attr
            itemElement.AppendChild(thumbnailElement);
            channelElement.AppendChild(itemElement);
            return xmlDocument;
        }
        public void AddRssChannel(RssChannel channel)
        {
            _rss = addRssChannel(_rss, channel);
        }
        public void AddRssItem(RssItem item)
        {
            _rss = addRssItem(_rss, item);
        }
        public string RssDocument
        {
            get { return _rss.OuterXml; }
        }
        public XmlDocument RssXmlDocument
        {
            get { return _rss; }
        }
    }
}

    */

    class RssHelper { }
}