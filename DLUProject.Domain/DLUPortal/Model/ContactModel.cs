/* FileName: ContactModel.cs
Project Name: DLUProject
Date Created: 17/11/2014 10:08:11 AM
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
    /// Represents a ContactModel
    /// </summary>
    public partial class ContactModel
    {
        [Required]
        [Display(Name = "ContactID")]
        public int ContactID { get; set; }
        
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Họ tên bắt buộc nhập"), StringLength(50)]
        public string FullName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }
      
        [Display(Name = "Email")]
        [Required(ErrorMessage="Email bắt buộc nhập"), StringLength(50), DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required (ErrorMessage="Chủ đề bắt buộc nhập"), StringLength(50)]
        [Display(Name = "Tiêu đề")]
        public string Subject { get; set; }
        [Required(ErrorMessage="Nội dung bắt buộc nhập"), DataType(DataType.MultilineText)]
        [Display(Name = "Nội dung")]
        public string Body { get; set; }
        [Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "IsRead")]
        public bool IsRead { get; set; }

    }
}

