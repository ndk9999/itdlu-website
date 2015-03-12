/* FileName: MenusModel.cs
Project Name: DLUProject
Date Created: 14/11/2014 10:45:28 PM
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
    /// Represents a MenusModel
    /// </summary>
    public partial class MenusModel
    {
        [Required]
[Display(Name = "MenuID")]
        public int MenuID { get; set; } 
        [Required, StringLength(128)]
[Display(Name = "Name")]
        public string Name { get; set; } 
[Display(Name = "Alias")]
        public string Alias { get; set; } 
[Display(Name = "Image")]
        public string Image { get; set; } 
[Display(Name = "Description")]
        public string Description { get; set; } 
[Display(Name = "Route")]
        public string Route { get; set; } 
[Display(Name = "Url")]
        public string Url { get; set; } 
[Display(Name = "ParentID")]
        public int ParentID { get; set; } 
[Required]
[Display(Name = "DisplayFlags")]
        public int DisplayFlags { get; set; } 
[Display(Name = "mu")]
        public int SortOrder { get; set; } 
[Display(Name = "IsPublished")]
        public bool IsPublished { get; set; } 
[Display(Name = "Params")]
        public string Params { get; set; } 

    }
}

