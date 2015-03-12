/* FileName: AccountInGroup.cs
Project Name: ColorSalesProject
Date Created: 28/7/2014 10:16:20 AM
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
    /// Represents a AccountInGroup
    /// </summary>
    [TableName("AccountInGroup")]
    public class AccountInGroup
    {
        [Identity, PrimaryKey]
        public int AccountInGroupID { get; set; }
        public int AccountID { get; set; }
        public int GroupID { get; set; }
        // FK_AccountInGroup_AccountGroup
        [Association(ThisKey = "GroupID", OtherKey = "GroupID", CanBeNull = false)]
        public AccountGroup AccountGroup { get; set; }

    }
}

