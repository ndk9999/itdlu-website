/* FileName: ResourceFileAttachmentModel.cs
Project Name: DLUProject
Date Created: 2/3/2015 8:54:39 AM
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
    /// Represents a ResourceFileAttachmentModel
    /// </summary>
    public partial class ResourceFileAttachmentModel
    {
        [Required]
[Display(Name = "ResourceID")]
        public int ResourceID { get; set; } 
[Required]
[Display(Name = "FileID")]
        public int FileID { get; set; } 

    }
}

