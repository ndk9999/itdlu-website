using ColorLife.Core.Helper;
using DLUProject.Domain;
using DLUProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace DLUProjectAPI.Controllers
{
     
    public class ApplicationController : ApiController
    {
        IServices<Menus> _menuService;
        IServices<Setting> _settingService;
        IServices<Category> _categoryService;
        IServices<GallerySlider> _galleryService;
        IServices<Content> _contentService;
        IServices<Notice> _noticeService;
        IServices<Department> _departmentService;
        IServices<AdvBanner> _advBannerService;
        IServices<Photo> _photoService;
        IServices<Video> _videoService;
        IServices<Document> _documentService;
        public ApplicationController(IServices<Menus> menuService, IServices<Setting> settingService,
            IServices<Category> categoryService, IServices<GallerySlider> galleryService,
            IServices<Content> contentService, IServices<Notice> noticeService,
            IServices<Department> departmentService, IServices<AdvBanner>bannerService,
            IServices<Photo> photoService, IServices<Video> videoService, IServices<Document>documentService)
        {
            this._menuService = menuService;
            this._settingService = settingService;
            this._categoryService = categoryService;
            this._galleryService = galleryService;
            this._contentService = contentService;
            this._noticeService = noticeService;
            this._departmentService = departmentService;
            this._advBannerService=bannerService;
            this._photoService = photoService;
            this._videoService = videoService;
            this._documentService = documentService;
        }
        [HttpGet]
        public string Get()
        {
            return "Hello world";
        }
        [HttpGet]
        public JsonResponse<Setting> Setting(string id)
        {
            var model = _settingService.All().FirstOrDefault(c => c.Name.Equals(id.Trim().ToUpper()));
            if (model != null)
            {
                return new JsonResponse<Setting> { Success = true, Data = model };
            }
            return new JsonResponse<Setting> { Success = false, Data = null };
        }
        #region MENU
        private string GenerateUL(List<Menus> menu, List<Menus> table, StringBuilder sb)
        {
            sb.AppendLine("<ul>");

            if (menu.Count > 0)
            {
                foreach (var item in menu)
                {
                    string line = string.Format(@"<li><a href=""{0}"">{1}</a>", item.Url, item.Name);
                    sb.Append(line);
                    var subMenu = table.Where(c => c.ParentID == item.MenuID);
                    if (subMenu.Count() > 0 && !item.MenuID.Equals(item.ParentID))
                    {
                        var subMenuBuilder = new StringBuilder();
                        sb.Append(GenerateUL(subMenu.ToList(), table, subMenuBuilder));
                    }
                    sb.Append("</li>");
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
        [HttpGet]
        public JsonResponse<string> GetDLUMainMenu(int id)
        {
            var data = _menuService.All().Where(c => c.IsPublished == true).Select(c => new Menus
            {
                MenuID = c.MenuID,
                Name = c.Name,
                ParentID = c.ParentID,
                Url = c.Url,
                DisplayFlags = c.DisplayFlags,
                SortOrder = c.SortOrder,
                Params=c.Params,
            }).OrderBy(c => c.SortOrder).ToList();
            var list = data.Where(c => c.IsDisplayFlag(DisplayFlagMenuEnum.MainMenu));
            var parentMenus = list.Where(c => c.ParentID == 0);
            var sb = new StringBuilder();
            string unorderedList = GenerateUL(parentMenus.ToList(), list.ToList(), sb);

            return new JsonResponse<string> { Success = true, Data = unorderedList.ToString() };
        }
        [HttpGet]
        public JsonResponse<Menus> GetAllMenuByFlag(string id)
        {
            var flags = ConvertType.ToEnum<DisplayFlagMenuEnum>(id);

            var list = _menuService.All().Where(c => c.IsPublished == true).Select(c => new Menus
            {
                Image=c.Image,
                 Alias=c.Alias,
                MenuID = c.MenuID,
                Name = c.Name,
                ParentID = c.ParentID,
                Url = c.Url,
                DisplayFlags = c.DisplayFlags,
                SortOrder = c.SortOrder,
                Params=c.Params,

            }).OrderBy(c => c.SortOrder).ToList();
            var model = list.Where(c => c.IsDisplayFlag(flags));
            return new JsonResponse<Menus> { Success = true, ListData = model.ToList() };
        }

        [HttpGet]
        public JsonResponse<string> GetMainMenu(int id)
        {
            var list = _menuService.Table.Where(c => c.IsPublished == true).Select(c => new Menus
            {
                MenuID = c.MenuID,
                Name = c.Name,
                ParentID = c.ParentID,
                Url = c.Url,
                DisplayFlags = c.DisplayFlags,
                SortOrder = c.SortOrder
            }).OrderBy(c => c.SortOrder).ToList();
            StringBuilder html = new StringBuilder();
            html.AppendLine(" <ul class=\"nav navbar-nav collapse navbar-collapse\">");
            foreach (var item in list.Where(c => c.ParentID == 0 && c.IsDisplayFlag(DisplayFlagMenuEnum.MainMenu)))
            {
                if (hasChild(list.ToList(), item))
                {
                    html.AppendLine("<li class=\"dropdown\">");
                    html.AppendLine("<a href='" + item.Url + "'>" + item.Name + "<i class=\"fa fa-angle-down\"></i></a>");
                    html.AppendLine("<ul role=\"menu\" class=\"sub-menu\">");
                    foreach (var item2 in _menuService.All().Where(c => c.ParentID.Equals(item.MenuID)))
                    {
                        html.AppendLine(" <li><a href='" + item2.Url + "'>" + item2.Name + "</a></li>");
                    }
                    html.AppendLine("</ul>");
                    html.AppendLine("</li>");
                }
                else
                    html.AppendLine("<li><a href='" + item.Url + "'>" + item.Name + "</a></li>");
            }
            html.AppendLine("</ul>");

            return new JsonResponse<string> { Data = html.ToString(), Success = true };
        }
        [HttpGet]
        public JsonResponse<string> GetMainDLUDeptMenu(int id)
        {
            var list = _menuService.Table.Where(c => c.IsPublished == true).Select(c => new Menus
            {
                MenuID = c.MenuID,
                Name = c.Name,
                ParentID = c.ParentID,
                Url = c.Url,
                DisplayFlags = c.DisplayFlags,
                SortOrder = c.SortOrder
            }).OrderBy(c => c.SortOrder).ToList();
            StringBuilder html = new StringBuilder();
           
            foreach (var item in list.Where(c => c.ParentID == 0 && c.IsDisplayFlag(DisplayFlagMenuEnum.MainMenu)))
            {
                html.AppendLine(" <div class=\"panel panel-default\">");
                if (hasChild(list.ToList(), item))
                {

                    html.AppendLine("<div class=\"panel-heading\">");
                    html.AppendLine("  <h4 class=\"panel-title\">");
                    html.AppendLine(" <a data-toggle=\"collapse\" data-parent=\"#accordion\" href=\"#collapseT"+item.MenuID+"\">");
                    html.AppendLine(item.Name);
                    html.AppendLine("<span class=\"pull-right\"><i class=\"fa fa-plus-square\"></i></span>");
                    html.AppendLine("</a>");
                    html.AppendLine("</h4></div>");

                    html.AppendLine("<div id=\"collapseT"+item.MenuID+"\" class=\"panel-collapse collapse\">");
                    html.AppendLine("<div class=\"list-group\">");
                    
                    foreach (var item2 in _menuService.All().Where(c => c.ParentID.Equals(item.MenuID)))
                    {
                        html.AppendLine("<a href='" + item2.Url + "' class=\"list-group-item\">" + item2.Name + "</a>");
                    }
                    html.AppendLine("</div>");
                    html.AppendLine("</div>");
                }
                else
                {
                    html.AppendLine("<div class=\"panel-heading\">");
                    html.AppendLine("  <h4 class=\"panel-title\">");
                    html.AppendLine("<a href='" + item.Url + "'>" + item.Name + "</a>");
                    html.AppendLine("</h4></div>");
                }
               //     html.AppendLine("<li><a href='" + item.Url + "'>" + item.Name + "</a></li>");
                html.AppendLine("</div>");
            }
           

            return new JsonResponse<string> { Data = html.ToString(), Success = true };
        }
        private bool hasChild(List<Menus> list, Menus item)
        {
            var a = list.Where(c => c.ParentID == item.MenuID);
            return a.Count() > 0;
        }

        #endregion
        private string GenerateULCategory(List<Category> menu, List<Category> table, StringBuilder sb)
        {
            sb.AppendLine("<ul>");

            if (menu.Count > 0)
            {
                foreach (var item in menu)
                {
                    string line = string.Format(@"<li><a href=""/san-pham/danh-muc/{0}/{1}"" class=""d_block f_size_large color_dark relative"">{2}</a>", item.Alias, item.CategoryID, item.Name);
                    sb.Append(line);
                    var subMenu = table.Where(c => c.ParentID == item.CategoryID);
                    if (subMenu.Count() > 0 && !item.CategoryID.Equals(item.ParentID))
                    {
                        var subMenuBuilder = new StringBuilder();
                        sb.Append(GenerateULCategory(subMenu.ToList(), table, subMenuBuilder));
                    }
                    sb.Append("</li>");
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
        [HttpGet]
        public JsonResponse<string> CategoryMenu(int id)
        {
            StringBuilder html = new StringBuilder();
            var list = _categoryService.All().Where(m => m.IsPublished == true).Select(c => new Category
            {
                CategoryID = c.CategoryID,
                ParentID = c.ParentID,
                Name = c.Name,
                Alias=c.Alias,
            }).ToList();


            var parentMenus = list.Where(c => c.ParentID == 0);
            var sb = new StringBuilder();
            string unorderedList = GenerateULCategory(parentMenus.ToList(), list.ToList(), sb);

            return new JsonResponse<string> { Success = true, Data = unorderedList.ToString() };
        }
        private bool hasChildCategory(List<Category> list, Category item)
        {
            var a = list.Where(c => c.ParentID == item.CategoryID);
            return a.Count() > 0;
        }
        #region GallerySlider
        [HttpGet]
        public JsonResponse<GallerySlider> GetSlider(string id)
        {
            var flags = ConvertType.ToEnum<DisplayFlagSlider>(id);
            var list = _galleryService.All().Where(c => c.IsPublished == true).Select(c => new GallerySlider
            {
                GalleryID = c.GalleryID,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                SortOrder = c.SortOrder,
                Url = c.Url,
                DisplayFlags = c.DisplayFlags,

            }).OrderBy(c => c.SortOrder).ToList();
            var model = list.Where(c => c.IsDisplayFlag(flags));
            return new JsonResponse<GallerySlider> { Success = false, ListData = model.ToList() };
        }
        #endregion
        #region AdvBanner
        [HttpGet]
        public JsonResponse<AdvBanner> GetImageBanner(string id)
        {
            var flags = ConvertType.ToEnum<DisplayFlagBannerPosition>(id);
            var list = _advBannerService.All().Where(c => c.BannerType== BannerTypeEnum.Image && c.IsPublished == true).Select(c => new AdvBanner
            {
                AdvID = c.AdvID,
                Name = c.Name,
                Link = c.Link,
                SortOrder = c.SortOrder,
                Image = c.Image,
                DisplayFlags = c.DisplayFlags,

            }).OrderBy(c => c.SortOrder).ToList();
            var model = list.Where(c => c.IsDisplayFlag(flags));
            return new JsonResponse<AdvBanner> { Success = false, ListData = model.ToList() };
        }
        [HttpGet]
        public JsonResponse<AdvBanner> GetTextLinkBanner(string id)
        {
            var flags = ConvertType.ToEnum<DisplayFlagBannerPosition>(id);
            var list = _advBannerService.All().Where(c => c.BannerType== BannerTypeEnum.TextLink && c.IsPublished == true).Select(c => new AdvBanner
            {
                AdvID = c.AdvID,
                Name = c.Name,
                Link = c.Link,
                SortOrder = c.SortOrder,
                Image = c.Image,
                DisplayFlags = c.DisplayFlags,

            }).OrderBy(c => c.SortOrder).ToList();
            var model = list.Where(c => c.IsDisplayFlag(flags));
            return new JsonResponse<AdvBanner> { Success = false, ListData = model.ToList() };
        }
        #endregion
        #region Content
        [HttpGet]
        public JsonResponse<Content> GetLastestNews(int id)
        {
            var list = _contentService.All().Where(c => c.IsPublished == true && c.IsDeleted == false).Take(id).Select(c => new Content
                {
                    ContentID = c.ContentID,
                    Alias = c.Alias.Trim(),
                    Name = c.Name.TruncateString(50),
                    Image = c.Image,
                    Source=c.Source,
                    Author=c.Author,
                    Description = c.Description.TruncateString(160)
                });


            return new JsonResponse<Content> { Success = false, ListData = list.ToList() };
        }

        [HttpGet]
        public JsonResponse<Content> GetNewsFrontPage(int id)
        {
            var list = _contentService.All().Where(c => c.IsDisplayFlag(DisplayFlagContent.FontPage)
                && c.IsPublished == true && c.IsDeleted == false).Take(id).Select(c => new Content
                {
                    ContentID = c.ContentID,
                    Alias=c.Alias.Trim(),
                    Name = c.Name,
                    Image = c.Image,
                    Author=c.Author,
                    Description = c.Description.TruncateString(160)
                });


            return new JsonResponse<Content> { Success = false, ListData = list.ToList() };
        }
        [HttpGet]
        public JsonResponse<Content> GetTopNewsHits(int id)
        {
            var list = _contentService.All().Where(c => c.IsDisplayFlag(DisplayFlagContent.FontPage)
                && c.IsPublished == true && c.IsDeleted == false).Take(id).Select(c => new Content
                {
                    ContentID = c.ContentID,
                    Name = c.Name,Alias=c.Alias.Trim(),
                    Image = c.Image,
                    Description = c.Description.TruncateString(160),
                     Source=c.Source,
                     Author=c.Author, 
                }).OrderByDescending(c=>c.Hits);

            return new JsonResponse<Content> { Success = false, ListData = list.ToList() };
        }
        [HttpGet]
        public JsonResponse<Content> GetContentByCategoryID(int id, int number)
        {
            var list = _contentService.All().Where(c => c.CategoryID.Equals(id) && c.IsPublished == true && !c.IsDeleted).Take(number)
                .Select(c => new Content
                {
                    ContentID = c.ContentID,
                    Name = c.Name,
                    Alias=c.Alias.Trim(),
                    Image = c.Image,
                    Source=c.Source,
                    CreatedById=c.CreatedById,
                    Author=c.Author,
                    Description = c.Description.TruncateString(160)
                });

            return new JsonResponse<Content> { Success = false, ListData = list.ToList() };
        }
        #endregion
        [HttpGet]
        public JsonResponse<Notice> GetLatestNotice(int id)
        {
            var list = _noticeService.All().Where(c => c.IsPublished == true && c.IsDeleted == false).Take(id).Select(c => new Notice
            {
                NoticeID = c.NoticeID,
                Name = c.Name,
                DateCreated = c.DateCreated,
                Alias = c.Alias,
                CreatedByID = c.CreatedByID,
                Description = c.Description,
                
            }).OrderByDescending(c => c.NoticeID).ThenBy(c => c.SortOrder);
            return new JsonResponse<Notice> { Success = false, ListData = list.ToList() };
        }

        [HttpGet]
        public JsonResponse<Notice> GetTop10NewNotice(int id)
        {
            var list = _noticeService.All().Where(c => c.IsPublished == true && c.IsDeleted == false).Take(id).Select(c => new Notice
                {
                    NoticeID = c.NoticeID,
                    Name = c.Name,
                    DateCreated = c.DateCreated,
                    Alias = c.Alias,
                    CreatedByID = c.CreatedByID,
                    Description = c.Description,
                }).OrderByDescending(c => c.IsPin).ThenBy(c => c.NoticeID).ThenBy(c=>c.SortOrder);
            return new JsonResponse<Notice> { Success = false, ListData = list.ToList() };
        }
        [HttpGet]
        public JsonResponse<Notice> GetTopNoticePined(int id)
        {
            var list = _noticeService.All().Where(c => c.IsPublished == true && c.IsDeleted == false && c.IsPin == true).Take(id).Select(c => new Notice
            {
                NoticeID = c.NoticeID,
                Name = c.Name,
                DateCreated = c.DateCreated,
                Alias = c.Alias,
                CreatedByID = c.CreatedByID,
                Description = c.Description,
            }).OrderByDescending(c => c.NoticeID);
            return new JsonResponse<Notice> { Success = false, ListData = list.ToList() };
        }

       
        [HttpGet]
        public JsonResponse<Document> GetLastestDocument(int id)
        {
            var list = _documentService.All().Take(id);
            return new JsonResponse<Document> { Success = false, ListData = list.ToList() };
        }

        [HttpGet]
        public JsonResponse<Photo> GetPhotoHomePage(int id)
        {
            var list = _photoService.All().Take(id).ToList();
            return new JsonResponse<Photo> { Success = false, ListData = list };
        }
        [HttpGet]
        public JsonResponse<Video> GetVideoHomePage(int id)
        {
            var list = _videoService.All().Take(id).ToList();
            return new JsonResponse<Video> { Success = false, ListData = list };
        }
        [HttpGet]
        public JsonResponse<string> GetDepartmentOnHomePage(int id)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder js = new StringBuilder();
            var list = _departmentService.Table.Select(c => new Department
            {
                ParentID = c.ParentID,
                DepartmentID = c.DepartmentID,
                Name = c.Name,
                Alias = c.Alias,
                SortOrder = c.SortOrder,
                Website = c.Website,
            }).OrderBy(c => c.DepartmentID).ThenBy(c => c.SortOrder).ToList();
           // var list = _departmentService.All();
            sb.AppendLine("  <ul class=\"ulofficer\">");
            foreach (var item in list.Where(c => c.ParentID == id))
            {
                sb.AppendLine("<li><a href=\"#\" id=\"" + item.DepartmentID + "_menu\">" + item.Name + "</a>");
                if (hasChildDepartment(list, item))
                {
                    sb.AppendLine("<div id=\"megamenu1\" class=\"megamenu\"><div class=\"childmenu\"> <ul>");
                    foreach (var item2 in list.Where(c=>c.ParentID==item.DepartmentID))
                    {
                        sb.AppendLine("<li><a href=\"/Organization/Single/" + item2.DepartmentID + "\">" + item2.Name + "</a></li>");
                    }
                    sb.AppendLine("</ul></div></div>");
                  //  sb.AppendLine(" <script type=\"text/javascript\">");
                    js.AppendLine("jkmegamenu.definemenu(\"" + item.DepartmentID + "_menu\", \"megamenu1\", \"mouseover\");");
                  //  sb.AppendLine("</script>");
                } 
                sb.AppendLine("</li>");
            }
            sb.AppendLine(" </ul>");

            return new JsonResponse<string> { Data = sb.ToString(), Success = true, Message = js.ToString() };
        }
        [HttpGet]
        public JsonResponse<string> GetDepartmentOnHomePage2(int id)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder js = new StringBuilder();
            var list = _departmentService.Table.Select(c => new Department
            {
                ParentID = c.ParentID,
                DepartmentID = c.DepartmentID,
                Name = c.Name,
                Alias = c.Alias,
                SortOrder = c.SortOrder,
                Website=c.Website 
            }).OrderBy(c => c.DepartmentID).ThenBy(c => c.SortOrder).ToList();
            // var list = _departmentService.All();
            sb.AppendLine("  <ul class=\"menu\">");
            foreach (var item in list.Where(c => c.ParentID == id))
            {
                sb.AppendLine("<li><a href=\"#\" id=\"" + item.DepartmentID + "_menu\">" + item.Name + "</a>");
                if (hasChildDepartment(list, item))
                {
                    sb.AppendLine(" <ul>");
                    foreach (var item2 in list.Where(c => c.ParentID == item.DepartmentID))
                    {
                        sb.AppendLine("<li><a href=\"/Organization/Single/" + item2.DepartmentID + "\">" + item2.Name + "</a></li>");
                    }
                    sb.AppendLine("</ul>");
                    
                }
                sb.AppendLine("</li>");
            }
            sb.AppendLine(" </ul>");

            return new JsonResponse<string> { Data = sb.ToString(), Success = true, Message = js.ToString() };
        }
        [HttpGet]
        public JsonResponse<string> GetDepartmentOnHomePageTabs(int id)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder js = new StringBuilder();
            var list = _departmentService.Table.Where(c=>c.IsPublished==true).Select(c => new Department
            {
                ParentID = c.ParentID,
                DepartmentID = c.DepartmentID,
                Name = c.Name,
                Alias = c.Alias,
                SortOrder = c.SortOrder,
                Website=c.Website 
            }).OrderBy(c => c.SortOrder).ToList();
            // var list = _departmentService.All();
            sb.AppendLine("  <ul class=\"tabs\">");
            foreach (var item in list.Where(c => c.ParentID == id  ))
            {
                sb.AppendLine("<li class=\"tab-link\" data-tab=\"tab-" + item.DepartmentID + "\">" + item.Name + "</li>");
            }
            sb.AppendLine("</ul>");
            foreach (var item in list.Where(c => c.ParentID == id  ))
            {
                sb.AppendLine("  <div id=\"tab-" + item.DepartmentID + "\" class=\"tab-content\">");
                if (hasChildDepartment(list, item))
                {
                    sb.AppendLine("<ol>");
                    foreach (var item2 in list.Where(c => c.ParentID == item.DepartmentID  ))
                    {


                        sb.AppendLine("<li><a href=\"" + item.Website + "\" target='_blank'>" + item2.Name + "</a></li>");


                    } sb.AppendLine("</ol>");
                }
                sb.AppendLine("</div>");
            }
            return new JsonResponse<string> { Data = sb.ToString(), Success = true, Message = js.ToString() };
        }
        private bool hasChildDepartment(List<Department> list, Department item)
        {
            var a = list.Where(c => c.ParentID == item.DepartmentID);
            return a.Count() > 0;
        }
    }
}