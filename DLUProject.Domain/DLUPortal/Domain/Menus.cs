/* FileName: Menus.cs
Project Name: DLUProject
Date Created: 14/11/2014 10:45:28 PM
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
    /// Represents a Menus
    /// </summary>
    [TableName("Menus")]
    public class Menus
    {
        [Identity, PrimaryKey]
        public int MenuID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public string Route { get; set; }
        [Nullable]
        public string Url { get; set; }
        [DefaultValue(0)]
        public int ParentID { get; set; }
        public int DisplayFlags { get; set; }
       [DefaultValue(1)]
        public int SortOrder { get; set; }
        [DefaultValue(true)]
        public bool IsPublished { get; set; }
        [Nullable]
        public string Params { get; set; }
        [MapIgnore]
        public string Breadcrumb { get; set; }
        public bool IsDisplayFlag(DisplayFlagMenuEnum flag)
        {
            return (((int)flag) & DisplayFlags) == (int)flag;
        }
         
        public List<string> DisplayFlagsString()
        {
            List<string> s = new List<string>();
            foreach (var item in Enum.GetValues(typeof(DisplayFlagMenuEnum)))
            {
                var x = (DisplayFlagMenuEnum)Enum.Parse(typeof(DisplayFlagMenuEnum), item.ToString(), true);
                if (this.IsDisplayFlag(x)) s.Add(item.ToString());
            }
            return s;
        }
    }
}

