/* FileName: PortalModel.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:47:55 AM
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
    /// Represents a PortalModel
    /// </summary>
    public partial class PortalModel
    {
        [Required]
[Display(Name = "ID")]
        public int PortalID { get; set; } 
        [Required, StringLength(250)]
[Display(Name = "Tên")]
        public string Name { get; set; } 
[Display(Name = "Url")]
        public string Url { get; set; } 
[Display(Name = "Host")]
        public string Host { get; set; } 
[Display(Name = "Logo")]
        public string LogoUrl { get; set; } 
[Display(Name = "Sử dụng SSL")]
        public bool SSLEnable { get; set; } 
[Display(Name = "Https Url")]
        public string SecureUrl { get; set; } 
[Display(Name = "Mặc định")]
        public bool IsDefault { get; set; } 

    }
}

