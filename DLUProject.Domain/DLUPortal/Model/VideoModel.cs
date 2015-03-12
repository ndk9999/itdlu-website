/* FileName: VideoModel.cs
Project Name: DLUProject
Date Created: 17/12/2014 2:57:27 PM
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
    /// Represents a VideoModel
    /// </summary>
    public partial class VideoModel
    {
        [Required]
        [Display(Name = "VideoID")]
        public int VideoID { get; set; }
        [Required]
        [Display(Name = "GalleryID")]
        public int GalleryID { get; set; }
        [Display(Name = "DataSource")]
        public int DataSource { get; set; }

        public string Name { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
        public string VideoUrl { get; set; }
        [Display(Name = "Caption")]
        public string Caption { get; set; }
        [StringLength(1000), DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
