/* FileName: SettingModel.cs
Project Name: DLUProject
Date Created: 14/11/2014 1:30:15 PM
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
    /// Represents a SettingModel
    /// </summary>
    public partial class SettingModel
    {
        [Required]
[Display(Name = "SettingId")]
        public int SettingId { get; set; } 
        [Required, StringLength(250)]
[Display(Name = "Name")]
        public string Name { get; set; } 
[Display(Name = "Value")]
        public string Value { get; set; } 

    }
}

