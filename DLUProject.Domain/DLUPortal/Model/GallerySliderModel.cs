/* FileName: GallerySliderModel.cs
Project Name: DLUProject
Date Created: 14/11/2014 11:05:10 AM
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
    /// Represents a GallerySliderModel
    /// </summary>
    public partial class GallerySliderModel
    {
        [Required]
[Display(Name = "GalleryID")]
        public int GalleryID { get; set; } 
[Required]
[Display(Name = "DisplayFlags")]
        public int DisplayFlags { get; set; } 
        [Required, StringLength(250)]
[Display(Name = "Name")]
        public string Name { get; set; } 
        [StringLength(4000), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 
[Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; } 
[Display(Name = "Url")]
        public string Url { get; set; } 
[Display(Name = "Attribute")]
        public string Attribute { get; set; } 
[Display(Name = "SortOrder")]
        public int SortOrder { get; set; } 
[Display(Name = "IsPublished")]
        public bool IsPublished { get; set; } 
[Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } 

    }
}

