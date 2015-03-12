/* FileName: WorkGroup.cs
Project Name: DLUProject
Date Created: 5/6/2014 8:10:22 AM
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
    /// Represents a WorkGroup
    /// </summary>
    [TableName("WorkGroup")]
    public class WorkGroup
    {
        [Identity, PrimaryKey]
        public int WorkGroupID { get; set; }
        public int SystemID { get; set; }
        public string Name { get; set; }
        [DefaultValue(0)]
        public int ParentID { get; set; }
        public string Url { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public int SortOrder { get; set; }
        public int DisplayFlags { get; set; }
        [DefaultValue(true)]
        public bool IsEnabled { get; set; }
        // FK_WorkGroup_Systems
        [Association(ThisKey = "SystemID", OtherKey = "SystemID", CanBeNull = false)]
        public Systems Systems { get; set; }
        // FK_Function_WorkGroup
        [Association(ThisKey = "WorkGroupID", OtherKey = "WorkGroupID", CanBeNull = true)]
        public List<Function> Functions { get; set; }
        [MapIgnore]
        public int CountFunction { get; set; }

        [MapIgnore]
        public string UrlFull { get { return Systems.Url + "/" + this.Url; } }
        
        [MapIgnore]
        public string Breadcrumb { get; set; }
        public bool IsDisplayFlag(DisplayFlagMenu flag)
        {
            return (((int)flag) & DisplayFlags) == (int)flag;
        }

        

        public List<string> DisplayFlagsString()
        {
            List<string> s = new List<string>();
            foreach (var item in Enum.GetValues(typeof(DisplayFlagMenu)))
            {
                var x = (DisplayFlagMenu)Enum.Parse(typeof(DisplayFlagMenu), item.ToString(), true);
                if (this.IsDisplayFlag(x)) s.Add(item.ToString());
            }
            return s;
        }
    }
}

