/* FileName: ResourceModel.cs
Project Name: DLUProject
Date Created: 2/3/2015 8:54:58 AM
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
    /// Represents a ResourceModel
    /// </summary>
    public partial class ResourceModel
    {
        [Required]
[Display(Name = "ResourceID")]
        public int ResourceID { get; set; } 
[Required]
[Display(Name = "CategoryID")]
        public int CategoryID { get; set; } 
[Display(Name = "Title")]
        public string Title { get; set; } 
        [StringLength(4000), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 
[Display(Name = "ResourceType")]
        public string ResourceType { get; set; } 
[Display(Name = "CreatedByID")]
        public int CreatedByID { get; set; } 
[Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } 
[Display(Name = "ModifiedByID")]
        public int ModifiedByID { get; set; } 
[Display(Name = "DateModified")]
        public DateTime DateModified { get; set; } 
[Display(Name = "IsPublished")]
        public bool IsPublished { get; set; } 

    }
}

