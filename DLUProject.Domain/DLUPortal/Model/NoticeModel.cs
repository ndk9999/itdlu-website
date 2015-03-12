/* FileName: NoticeModel.cs
Project Name: DLUProject
Date Created: 18/11/2014 6:54:19 PM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DLUProject.Model
{
    /// <summary>
    /// Represents a NoticeModel
    /// </summary>
    public partial class NoticeModel
    {
        [Required]
        [Display(Name = "NoticeID")]
        public int NoticeID { get; set; }
        [Display(Name = "Thể loại")]
        public int CategoryID { get; set; }
        [Display(Name = "Phòng ban")]
        public int DepartmentID { get; set; }
        [Required, StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string Name { get; set; }
        [Display(Name = "Alias")]
        public string Alias { get; set; }
       
        [StringLength(4000), DataType(DataType.MultilineText)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Nội dung")]
        public string FullText { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Display(Name = "Người thông báo")]
        public string  CreatedByID { get; set; }
        [Display(Name = "MetaTitle")]
        public string MetaTitle { get; set; }
        [Display(Name = "MetaKeywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "MetaDescription")]
        public string MetaDescription { get; set; }
        [Display(Name = "Hiển thị")]
        public bool IsPublished { get; set; }
         [Display(Name = "Sắp xếp")]
        public int SortOrder { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Ngày chỉnh sửa")]
        public DateTime DateModified { get; set; }
        [Display(Name = "Đã xóa")]
        public bool IsDeleted { get; set; }
        [Display(Name = "Lượt xem")]
        public int Hits { get; set; }
        [Display(Name = "Liên kết ngoài")]
        public string Url { get; set; }

        public bool IsPin { get; set; }

    }
}

