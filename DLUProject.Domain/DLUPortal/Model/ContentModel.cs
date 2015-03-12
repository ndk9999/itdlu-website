/* FileName: ContentModel.cs
Project Name: DLUProject
Date Created: 18/11/2014 10:53:56 AM
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
    /// Represents a ContentModel
    /// </summary>
    public partial class ContentModel
    {
        [Required]
[Display(Name = "ContentID")]
        public int ContentID { get; set; } 
[Display(Name = "CategoryID")]
        public int CategoryID { get; set; }
public int DepartmentID { get; set; }
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
[Display(Name = "MetaTitle")]
        public string MetaTitle { get; set; } 
[Display(Name = "MetaDescription")]
        public string MetaDescription { get; set; } 
[Display(Name = "MetaKeywords")]
        public string MetaKeywords { get; set; } 
[Required]
[Display(Name = "FullText")]
        public string FullText { get; set; } 
[Display(Name = "Image")]
        public string Image { get; set; } 
[Required]
[Display(Name = "CreatedById")]
        public int CreatedById { get; set; } 
[Display(Name = "ModifiedBy")]
        public string ModifiedBy { get; set; } 
[Display(Name = "Hits")]
        public int Hits { get; set; } 
[Display(Name = "IsPublished")]
        public bool IsPublished { get; set; } 
[Display(Name = "IsCommentEnable")]
        public bool IsCommentEnable { get; set; } 
[Display(Name = "State")]
        public int State { get; set; } 
[Display(Name = "DisplayFlags")]
        public int DisplayFlags { get; set; } 
[Display(Name = "CheckoutBy")]
        public string CheckoutBy { get; set; } 
[Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } 
[Display(Name = "DateModified")]
        public DateTime DateModified { get; set; } 
[Display(Name = "DateCheckout")]
        public DateTime DateCheckout { get; set; } 
[Display(Name = "DatePublished")]
        public DateTime DatePublished { get; set; } 
[Display(Name = "ContentType")]
        public string ContentType { get; set; } 
[Display(Name = "SortOrder")]
        public int SortOrder { get; set; } 
[Display(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

public string Author { get; set; }
public string Source { get; set; }
    }
}

