using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ColorLife.Core.Mvc;
using DLUProject.Domain;

namespace DLUProjectMvc.ViewModels
{
    public class ContentCategoryViewModel
    {
        public Category Item { get; set; }
        public PagedList<Content> PagedListContent { get; set; }

        public List<Content> ListTopNew { get; set; }
        public List<Content> ListTopViews { get; set; }
    }
}