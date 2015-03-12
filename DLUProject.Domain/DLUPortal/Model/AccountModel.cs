/* FileName: AccountModel.cs
Project Name: DLUProject
Date Created: 7/6/2014 8:49:08 PM
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
    /// Represents a AccountModel
    /// </summary>
    public partial class AccountModel
    {
        [Required]
        public int AccountID { get; set; }
        [Required, StringLength(250)]
        public string Email { get; set; }
        [Required, StringLength(250)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public int LoginFailedCount { get; set; }
        public DateTime LastLoginDate { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

    }
}

