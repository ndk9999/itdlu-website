/* FileName: GroupFunctionModel.cs
Project Name: DLUProject
Date Created: 8/5/2014 7:29:00 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn
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
    /// Represents a GroupFunctionModel
    /// </summary>
    public partial class GroupFunctionModel
    {
        [Required]
        public int GroupID { get; set; }
        [Required]
        public int FunctionID { get; set; }

    }
}

