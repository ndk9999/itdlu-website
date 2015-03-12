/* FileName: Account.cs
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
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Data.Linq;

namespace DLUProject.Domain
{
    /// <summary>
    /// Represents a Account
    /// </summary>
    [TableName("Account")]
    public class Account
    {
        [Identity, PrimaryKey]
        public int AccountID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        [DefaultValue(true)]
        public bool IsApproved { get; set; }
        [DefaultValue(false)]
        public bool IsLockedOut { get; set; }
        [DefaultValue(0)]
        public int LoginFailedCount { get; set; }
        [NullDateTime]
        public DateTime LastLoginDate { get; set; }
        [NullDateTime]
        public DateTime DateCreated { get; set; }
        // FK_AccountLog_Account
        [Association(ThisKey = "AccountID", OtherKey = "AccountID", CanBeNull = true)]
        public List<AccountLog> AccountLogs { get; set; }
        [MapIgnore]
        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }
    }
}

