/* FileName: SystemsModel.cs
Project Name: DLUProject
Date Created: 5/6/2014 8:10:21 AM
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
namespace DLUProject.Domain
{
	/// <summary>
    /// Represents a SystemsModel
    /// </summary>
    public partial class SystemsModel
    {
        [Required]
        public int SystemID { get; set; } 
        [Required, StringLength(250)]
        public string Name { get; set; } 
        [Required, StringLength(250)]
        public string Url { get; set; } 
        public string Description { get; set; } 
        public string Image { get; set; } 
        public bool IsEnabled { get; set; } 

    }
}

