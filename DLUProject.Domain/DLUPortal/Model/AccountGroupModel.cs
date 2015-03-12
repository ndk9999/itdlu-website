/* FileName: AccountGroupModel.cs
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
namespace DLUProject.Model
{
	/// <summary>
    /// Represents a AccountGroupModel
    /// </summary>
    public partial class AccountGroupModel
    {
        [Required]
        public int GroupID { get; set; } 
        [Required, StringLength(50)]
        public string Name { get; set; } 

    }
}

