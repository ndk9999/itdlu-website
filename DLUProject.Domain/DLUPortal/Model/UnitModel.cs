/* FileName: UnitModel.cs
Project Name: DLUProject
Date Created: 26/11/2014 9:21:11 AM
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
    /// Represents a UnitModel
    /// </summary>
    public partial class UnitModel
    {
        [Required]
[Display(Name = "UnitID")]
        public int UnitID { get; set; } 
        [Required, StringLength(50)]
[Display(Name = "Name")]
        public string Name { get; set; } 
[Display(Name = "Description")]
        public string Description { get; set; } 

    }
}

