using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLUProject.Domain;
namespace DLUProjectMvc.ViewModels
{
    public class SingleContentViewModel
    {
        public Content Item { get; set; }
        public List<Content> LatestContent { get; set; }
        public List<Content> RelatedContent { get; set; }
        public List<Content> ListTopNew { get; set; }
        public List<Content> ListTopViews { get; set; }
    }
}