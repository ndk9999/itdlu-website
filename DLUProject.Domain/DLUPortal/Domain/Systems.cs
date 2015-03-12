/* FileName: Systems.cs
Project Name: DLUProject
Date Created: 5/6/2014 8:10:21 AM
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
    /// Represents a Systems
    /// </summary>
    [TableName("Systems")]
    public class Systems
    {
        [Identity, PrimaryKey]
        public int SystemID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public bool IsEnabled { get; set; }
        // FK_WorkGroup_Systems
        [Association(ThisKey = "SystemID", OtherKey = "SystemID", CanBeNull = true)]
        public List<WorkGroup> WorkGroups { get; set; }

        [MapIgnore]
        public int CountWorkGroup { get; set; }
    }
}

