using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DLUProject.Domain
{
    public enum OrderSatusEnum
    {
        [Description("Đang chờ")]
        Pending = 0,
        [Description("Chưa hoàn thành")]
        Incomplete = 1,
        [Description("Đã trả lại")]
        Returned = 2,
        [Description("Hoàn thành")]
        Completed = 3,
        [Description("Đã hủy")]
        Canceled = 4,
        [Description("Đang xử lý")]
        Processed = 5,
        [Description("Đã chuyển một phần")]
        PartiallyShipped = 6,
        [Description("Đang chuyển đi")]
        Shipping = 7,
        [Description("Đã chuyển")]
        Shipped = 8,
        [Description("Trả lại một phần")]
        PartiallyReturned = 9,
    }
    public enum SalesOrderState
    {
        [Description("Specifies that the sales order (order) is active.")]
        Active = 0,     // New, Pending
        [Description("Specifies that the sales order (order) is canceled.")]
        Canceled = 2,  // No Money
        [Description("Specifies that the sales order (order) is fulfilled.")]
        Fulfilled = 3,  // Complete ,Partial
        [Description("Specifies that the sales order (order) is invoiced.")]
        Invoiced = 4, //  Invoiced
        [Description("Specifies that the sales order (order) is submitted.")]
        Submitted = 1 // In Progress
    }
}
