/* FileName: AdvBannerModel.cs
Project Name: DLUProject
Date Created: 24/11/2014 12:48:05 PM
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
    /// Represents a AdvBannerModel
    /// </summary>
    public partial class AdvBannerModel
    {
        [Required]
        [Display(Name = "AdvID")]
        public int AdvID { get; set; }
        [Required, StringLength(250)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Required, StringLength(250)]
        [Display(Name = "Link")]
        public string Link { get; set; }
        [Display(Name = "Width")]
        public int Width { get; set; }
        [Display(Name = "Height")]
        public int Height { get; set; }
        [StringLength(4000), DataType(DataType.MultilineText)]
        [Display(Name = "FullText")]
        public string FullText { get; set; }

        [Display(Name = "DisplayFlags")]
        public int DisplayFlags { get; set; }
        [Display(Name = "SortOrder")]
        public int SortOrder { get; set; }
        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
        [Display(Name = "Hits")]
        public int Hits { get; set; }
        [Display(Name = "Click")]
        public int Click { get; set; }
        [Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "DateEnded")]
        public DateTime DateEnded { get; set; }

    }
}

