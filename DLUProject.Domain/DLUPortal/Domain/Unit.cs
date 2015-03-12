/* FileName: Unit.cs
Project Name: DLUProject
Date Created: 26/11/2014 9:21:11 AM
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
    /// Represents a Unit
    /// </summary>
    [TableName("Unit")]
    public class Unit
    {
        [Identity, PrimaryKey]
        public int UnitID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Description { get; set; }

    }
}

