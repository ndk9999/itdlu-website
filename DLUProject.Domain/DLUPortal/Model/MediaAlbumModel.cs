/* FileName: MediaAlbumModel.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:53:07 AM
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
    /// Represents a MediaAlbumModel
    /// </summary>
    public partial class MediaAlbumModel
    {
        [Required]
[Display(Name = "AlbumID")]
        public int AlbumID { get; set; } 
[Display(Name = "CategoryID")]
        public int CategoryID { get; set; } 
[Display(Name = "Alias")]
        public string Alias { get; set; } 
        [Required, StringLength(50)]
[Display(Name = "Name")]
        public string Name { get; set; } 
[Display(Name = "Image")]
        public string Image { get; set; } 
        [StringLength(4000), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 
[Display(Name = "SortOrder")]
        public int SortOrder { get; set; } 
[Display(Name = "IsPublished")]
        public bool IsPublished { get; set; } 
[Display(Name = "CreatedByID")]
        public int CreatedByID { get; set; } 
[Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } 
[Display(Name = "MetaTitle")]
        public string MetaTitle { get; set; } 
[Display(Name = "MetaDescription")]
        public string MetaDescription { get; set; } 
[Display(Name = "MetaKeywords")]
        public string MetaKeywords { get; set; } 

    }
}

