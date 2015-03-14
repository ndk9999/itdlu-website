using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLUProject.Domain;

namespace DLUProjectMvc.ViewModels
{
    public class SingleDocumentViewModel
    {
        public Document Item { get; set; }
        public List<Document> ListRelated { get; set; }
        public List<Files> ListFiles { get; set; }
    }
}