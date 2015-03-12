/* FileName: StaffModel.cs
Project Name: DLUProject
Date Created: 22/11/2014 9:07:03 PM
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
    /// Represents a StaffModel
    /// </summary>
    public partial class StaffModel
    {
        [Required]
[Display(Name = "ID")]
        public int StaffID { get; set; } 
        [Required, StringLength(50)]
[Display(Name = "Mã số")]
        public string StaffNoID { get; set; } 
[Display(Name = "Ông / bà")]
        public string Title { get; set; } 
        [Required, StringLength(250)]
[Display(Name = "Họ tên lót")]
        public string FirstName { get; set; } 
        [Required, StringLength(250)]
[Display(Name = "Tên")]
        public string LastName { get; set; } 
[Display(Name = "Ngày sinh")]
        public DateTime DOB { get; set; } 
[Display(Name = "Điện thoại")]
        public string Phone { get; set; } 
[Display(Name = "Fax")]
        public string Fax { get; set; } 
[Display(Name = "Di động")]
        public string Mobile { get; set; } 
[Display(Name = "Email")]
        public string Email { get; set; } 
[Display(Name = "Hình ảnh")]
        public string Image { get; set; } 
[Display(Name = "Học vấn")]
        public string Degree { get; set; } 
[Display(Name = "Chức vụ")]
        public string Position { get; set; } 
        [StringLength(4000), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 

    }
}

