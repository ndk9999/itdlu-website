using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Net;

namespace ColorLife.Core.Helper
{
    public class MySelectItem {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class HtmlAgilityPackHelper
    {
        public static List<string> GetDataString(string url, string xpath)
        {
            List<string> list = new List<string>();
            HtmlDocument htmlDocument = new HtmlDocument();
            WebClient webClient = new WebClient();
            string s = System.Text.Encoding.UTF8.GetString(webClient.DownloadData(url));
            htmlDocument.LoadHtml(s);
            HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes(xpath);
            foreach (HtmlNode htmlNode in htmlNodeCollection)
            {
                list.Add(htmlNode.InnerText);
            }
            return list;
        }
        public static List<MySelectItem> GetDataSelectItem(string url, string xpath)
        {
            var items = new List<MySelectItem>();
            HtmlDocument htmlDocument = new HtmlDocument();
            WebClient webClient = new WebClient();
            string s = System.Text.Encoding.UTF8.GetString(webClient.DownloadData(url));
            htmlDocument.LoadHtml(s);
            var nodes = htmlDocument.DocumentNode.SelectNodes(xpath);
            foreach (HtmlNode node in nodes)
            {
                items.Add(new MySelectItem
                {
                    Value = node.Attributes["value"].Value,
                    Text = node.NextSibling.InnerText
                });
            }            
            return items;
        }
    }
}
