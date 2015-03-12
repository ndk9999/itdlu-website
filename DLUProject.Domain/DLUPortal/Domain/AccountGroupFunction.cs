/* FileName: AccountGroupFunction.cs
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
    /// Represents a AccountGroupFunction
    /// </summary>
    [TableName("AccountGroupFunction")]
    public class AccountGroupFunction
    {
                public int GroupID { get; set; }
        public int FunctionID { get; set; }

    }
}

