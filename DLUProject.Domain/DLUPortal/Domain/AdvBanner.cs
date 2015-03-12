/* FileName: AdvBanner.cs
Project Name: DLUProject
Date Created: 24/11/2014 12:48:05 PM
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
    /// Represents a AdvBanner
    /// </summary>
    [TableName("AdvBanner")]
    public class AdvBanner
    {
        [Identity, PrimaryKey]
        public int AdvID { get; set; }
        public string Name { get; set; }
        [MapField("BannerType")]
        public BannerTypeEnum BannerType { get; set; }
        [Nullable]
        public string Image { get; set; }
        public string Link { get; set; }
        [Nullable]
        public int Width { get; set; }
        [Nullable]
        public int Height { get; set; }
        [Nullable]
        public string FullText { get; set; }
        
        [Nullable]
        public int DisplayFlags { get; set; }
        [Nullable]
        public int SortOrder { get; set; }
        [Nullable]
        public bool IsPublished { get; set; }
        [Nullable]
        public int Hits { get; set; }
        [Nullable]
        public int Click { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public DateTime DateEnded { get; set; }

        public bool IsDisplayFlag(DisplayFlagBannerPosition flag)
        {
            return (((int)flag) & DisplayFlags) == (int)flag;
        }

        public List<string> DisplayFlagsString()
        {
            List<string> s = new List<string>();
            foreach (var item in Enum.GetValues(typeof(DisplayFlagBannerPosition)))
            {
                var x = (DisplayFlagBannerPosition)Enum.Parse(typeof(DisplayFlagBannerPosition), item.ToString(), true);
                if (this.IsDisplayFlag(x)) s.Add(item.ToString());
            }
            return s;
        }

    }
}

