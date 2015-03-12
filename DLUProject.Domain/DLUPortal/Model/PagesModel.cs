/* FileName: PagesModel.cs
Project Name: DLUProject
Date Created: 14/11/2014 2:49:18 PM
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
    /// Represents a PagesModel
    /// </summary>
    public partial class PagesModel
    {
        [Required]
[Display(Name = "PageID")]
        public int PageID { get; set; } 
        [Required, StringLength(250)]
[Display(Name = "Name")]
        public string Name { get; set; } 
[Display(Name = "Alias")]
        public string Alias { get; set; } 
[Display(Name = "Slug")]
        public string Slug { get; set; } 
        [StringLength(4000), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 
        [Required, StringLength(1073741823)]
[Display(Name = "FullText")]
        public string FullText { get; set; } 
[Display(Name = "Image")]
        public string Image { get; set; } 
[Display(Name = "IsPublished")]
        public bool IsPublished { get; set; } 
[Display(Name = "IsFrontPage")]
        public bool IsFrontPage { get; set; } 
[Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } 
[Display(Name = "DateModified")]
        public DateTime DateModified { get; set; } 
[Display(Name = "ParentID")]
        public int ParentID { get; set; } 
[Display(Name = "IsDeleted")]
        public bool IsDeleted { get; set; } 
[Display(Name = "MetaTitle")]
        public string MetaTitle { get; set; } 
[Display(Name = "MetaDescription")]
        public string MetaDescription { get; set; } 
[Display(Name = "MetaKeywords")]
        public string MetaKeywords { get; set; } 

    }
}

