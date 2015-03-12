/* FileName: Setting.cs
Project Name: DLUProject
Date Created: 14/11/2014 1:30:15 PM
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
    /// Represents a Setting
    /// </summary>
    [TableName("Setting")]
    public class Setting
    {
        [Identity, PrimaryKey]
        public int SettingId { get; set; }
[PrimaryKey]
        public string Name { get; set; }
        [Nullable]
        public string Value { get; set; }

    }
}

