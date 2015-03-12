using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLUProject.Domain;
namespace DLUProject.Model
{
    public  class MenuPopupModel
    {
        public List<Pages> ListPage { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<NoticeCategory> ListNoticeCategory { get; set; }
    }
}
