/* FileName: MediaModel.cs
Project Name: DLUProject
Date Created: 27/11/2014 9:15:32 PM
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
    /// Represents a MediaModel
    /// </summary>
    public partial class MediaModel
    {
        [Required]
[Display(Name = "MediaID")]
        public int MediaID { get; set; } 
[Required]
[Display(Name = "AlbumID")]
        public int AlbumID { get; set; } 
[Display(Name = "Alias")]
        public string Alias { get; set; } 
[Display(Name = "Slug")]
        public string Slug { get; set; } 
        [Required, StringLength(250)]
[Display(Name = "Name")]
        public string Name { get; set; } 
[Display(Name = "MediaType")]
        public string MediaType { get; set; } 
[Display(Name = "MimeType")]
        public string MimeType { get; set; } 
[Display(Name = "Image")]
        public string Image { get; set; } 
[Display(Name = "FileUrl")]
        public string FileUrl { get; set; } 
        [StringLength(4000), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 
[Display(Name = "Caption")]
        public string Caption { get; set; } 
[Display(Name = "AltTag")]
        public string AltTag { get; set; } 
[Display(Name = "Copyright")]
        public string Copyright { get; set; } 
[Display(Name = "SortOrder")]
        public int SortOrder { get; set; } 
[Display(Name = "IsPublished")]
        public bool IsPublished { get; set; } 
[Display(Name = "CreatedByID")]
        public int CreatedByID { get; set; } 
[Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } 
[Display(Name = "ModifiedByID")]
        public int ModifiedByID { get; set; } 
[Display(Name = "DateModified")]
        public DateTime DateModified { get; set; } 
[Display(Name = "DatePublished")]
        public DateTime DatePublished { get; set; } 
[Display(Name = "Hits")]
        public int Hits { get; set; } 
[Display(Name = "IsDeleted")]
        public bool IsDeleted { get; set; } 

    }
}



