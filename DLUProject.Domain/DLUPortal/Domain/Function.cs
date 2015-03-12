/* FileName: Function.cs
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
    /// Represents a Function
    /// </summary>
    [TableName("Function")]
    public class Function
    {
        [Identity, PrimaryKey]
        public int FunctionID { get; set; }
        public int WorkGroupID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Image { get; set; }
        public string Url { get; set; }
        [DefaultValue(1)]
        public int SortOrder { get; set; }
        [DefaultValue(true)]
        public bool IsEnabled { get; set; }

        // FK_Function_WorkGroup
        [Association(ThisKey = "WorkGroupID", OtherKey = "WorkGroupID", CanBeNull = false)]
        public WorkGroup WorkGroup { get; set; }

        [MapIgnore]
        public string UrlFull { get { return string.Format("{0}/Default?controller={1}", WorkGroup.UrlFull, this.Url).ToLower(); } }

        [MapIgnore]
        public string Breadcrumb { get; set; }
    }
}

