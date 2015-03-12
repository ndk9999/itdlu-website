/* FileName: AccountLogModel.cs
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
    /// Represents a AccountLogModel
    /// </summary>
    public partial class AccountLogModel
    {
        [Required]
        public int LogID { get; set; } 
[Required]
        public int AccountID { get; set; } 
[Required]
        public DateTime? DateLogin { get; set; } 
        public DateTime? DateLogout { get; set; } 
        public string IPAddress { get; set; } 
        public string MACAddress { get; set; } 

    }
}

