using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLUProject.Domain;
namespace DLUProjectMvc.ViewModels
{
    public class ContentHomePageViewModel
    {
        public List<Content> ContentSliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Content> ListContent { get; set; }

        public List<Content> ListTopNew { get; set; }
        public List<Content> ListTopViews { get; set; }
    }
}