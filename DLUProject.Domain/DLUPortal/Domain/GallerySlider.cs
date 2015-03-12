/* FileName: GallerySlider.cs
Project Name: DLUProject
Date Created: 14/11/2014 11:05:10 AM
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
    /// Represents a GallerySlider
    /// </summary>
    [TableName("GallerySlider")]
    public class GallerySlider
    {
        [PrimaryKey, Identity]
        public int GalleryID { get; set; }
        public int DisplayFlags { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public string ImageUrl { get; set; }
        [Nullable]
        public string Url { get; set; }
        [Nullable]
        public string Attribute { get; set; }
        [Nullable]
        public int SortOrder { get; set; }
        [Nullable]
        public bool IsPublished { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        public bool IsDisplayFlag(DisplayFlagSlider flag)
        {
            return (((int)flag) & DisplayFlags) == (int)flag;
        }
        
        public List<string> DisplayFlagsString()
        {
            List<string> s = new List<string>();
            foreach (var item in Enum.GetValues(typeof(DisplayFlagSlider)))
            {
                var x = (DisplayFlagSlider)Enum.Parse(typeof(DisplayFlagSlider), item.ToString(), true);
                if (this.IsDisplayFlag(x)) s.Add(item.ToString());
            }
            return s;
        }
    }
}
