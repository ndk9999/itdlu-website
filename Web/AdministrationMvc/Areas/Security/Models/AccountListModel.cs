using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLUProjectFramework.Mvc;
using System.Web.Mvc;
namespace DLUProjectMvc.Areas.Security.Models
{
    public class AccountListModel : BaseModel
    {
        [AllowHtml]
        public string SearchCategoryName { get; set; }
    }
}