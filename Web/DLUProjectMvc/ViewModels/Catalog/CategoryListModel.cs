using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLUProject.Domain;
namespace DLUProjectMvc.ViewModels
{
    public class CategoryListModel
    {
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}