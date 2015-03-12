using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLUProject.Domain;

namespace DLUProjectMvc.ViewModels
{
    public class SingleNoticeViewModel
    {
        public Notice Item { get; set; }
        public List<Notice> ListRelated { get; set; }
        public List<Files> ListFiles { get; set; }
    }
}