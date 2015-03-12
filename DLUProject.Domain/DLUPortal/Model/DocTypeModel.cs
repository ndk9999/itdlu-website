/* FileName: DocTypeModel.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:34:59 PM
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
    /// Represents a DocTypeModel
    /// </summary>
    public partial class DocTypeModel
    {
        [Required]
[Display(Name = "DocTypeID")]
        public int DocTypeID { get; set; } 
        [Required, StringLength(50)]
[Display(Name = "Name")]
        public string Name { get; set; } 
        [StringLength(400), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 

    }
}

