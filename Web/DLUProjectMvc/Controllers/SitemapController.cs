using SimpleMvcSitemap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Services;
using DLUProject.Domain;

namespace DLUProjectMvc.Controllers
{
    public class SitemapController : Controller
    {

        private IServices<Pages> _pageService;
        private IServices<Content> _contentService;
      
        private IServices<Category> _categoryService;
        private IServices<Document> _documentService;
      
        public SitemapController(IServices<Pages>pageService, IServices<Content>contentService, 
            IServices<Category>categoryService
            , IServices<Document> documentService
            )
        {
            this._pageService = pageService;
            this._categoryService = categoryService;
            
            this._contentService = contentService;
            this._documentService = documentService;
        }
        private List<SitemapNode> Nodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            nodes.Add(new SitemapNode(Url.Action("Index", "Home"))
            {
                ChangeFrequency = ChangeFrequency.Weekly,
                LastModificationDate = DateTime.UtcNow,
                Priority = 1.0M,

            });
            nodes.Add(new SitemapNode("/lien-he"));

            foreach (var item in _pageService.All())
            {
                nodes.Add(new SitemapNode("/trang/" + item.Alias)
                {
                    ChangeFrequency = ChangeFrequency.Weekly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.6M,
                });
            }
            

            nodes.Add(new SitemapNode("/tin-tuc"));
            foreach (var item in _contentService.All().Where(c => c.IsPublished == true))
            {

                nodes.Add(new SitemapNode("/tin-tuc/" + item.Alias + "/" + item.ContentID)
                {
                    ChangeFrequency = ChangeFrequency.Weekly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.6M,
                    Images = new List<SitemapImage>{
                        new SitemapImage(item.Image){ Url = item.Image, Caption = item.Name, Title = item.Name  }
                    },
                });
            }
            nodes.Add(new SitemapNode("/van-ban"));
            foreach (var item in _documentService.All().Where(c => c.IsPublished == true))
            {

                nodes.Add(new SitemapNode("/van-ban/" + item.Alias + "/" + item.DocumentID)
                {
                    ChangeFrequency = ChangeFrequency.Weekly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.6M,
                    
                });
            }
            return nodes;
        }
        // GET: Sitemap
        [Route("sitemap.xml")]
        public ActionResult Index()
        {

            //List<SitemapNode> nodes = new List<SitemapNode>
            //{
            //    new SitemapNode(Url.Action("Index","Home")){
            //    ChangeFrequency = ChangeFrequency.Weekly,
            //     LastModificationDate = DateTime.UtcNow,
            //Priority = 1.0M,

            //    },
            //    new SitemapNode(Url.Action("About","Home")),
            //    //other nodes
            //};

            return new SitemapProvider().CreateSitemap(HttpContext, Nodes());
        }
    }
}