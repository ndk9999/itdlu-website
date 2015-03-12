using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLUProject.Domain
{     
    public enum ProductStatusEnum
    {
        [Description("Có sẵn")]
        Active = 0,
        [Description("Đã bán")]
        Sold = 1,
        [Description("Sắp về")]
        ComingSoon = 2,
        [Description("Đã ngừng bán")]
        Deleted = 3
    }
    public enum ProductFeatured
    {
        HOT = 1,
        NEW = 2,
        SALE = 4,
        OLD = 8,
        CHEAP = 16
    }
}
