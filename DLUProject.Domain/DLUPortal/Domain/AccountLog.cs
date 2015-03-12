/* FileName: AccountLog.cs
Project Name: DLUProject
Date Created: 7/6/2014 9:15:56 AM
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
    /// Represents a AccountLog
    /// </summary>
    [TableName("AccountLog")]
    public class AccountLog
    {
        [Identity, PrimaryKey]
        public int LogID { get; set; }
        public int AccountID { get; set; }

        [MapField("LevelError")]
        public LevelErrorEnum LevelError { get; set; }
        [Nullable]
        public string Source { get; set; }
        [Nullable]
        public string ComputerName { get; set; }
        [Nullable]
        public string IPAddress { get; set; }
        [Nullable]
        public string MACAddress { get; set; }
        [Nullable]
        public string Url { get; set; }
        [Nullable]
        public string UserAgent { get; set; }
        [Nullable]
        public DateTime LoggedDate { get; set; }
        [Nullable]
        public string Detail { get; set; }

        [Association(ThisKey = "AccountID", OtherKey = "AccountID", CanBeNull = false)]
        public Account Account { get; set; }
    }
}

