/* FileName: PhotoModel.cs
Project Name: DLUProject
Date Created: 28/11/2014 2:03:05 PM
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
    /// Represents a PhotoModel
    /// </summary>
    public partial class PhotoModel
    {
        [Required]
[Display(Name = "PhotoID")]
        public int PhotoID { get; set; } 
[Required]
[Display(Name = "GalleryID")]
        public int GalleryID { get; set; } 
[Display(Name = "Image")]
        public string Image { get; set; } 
[Display(Name = "Caption")]
        public string Caption { get; set; } 
        [StringLength(1000), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 

    }
}

