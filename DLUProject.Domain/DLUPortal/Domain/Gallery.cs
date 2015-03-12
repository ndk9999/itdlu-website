/* FileName: Gallery.cs
Project Name: DLUProject
Date Created: 28/11/2014 2:03:13 PM
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
    /// Represents a Gallery
    /// </summary>
    [TableName("Gallery")]
    public class Gallery
    {
        [Identity, PrimaryKey]
        public int GalleryID { get; set; }
        [Nullable]
        public int CategoryID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [MapField("GalleryType")]
        public GalleryTypeEnum GalleryType { get; set; }
        [Nullable]
        public string DataSource { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public int SortOrder { get; set; }
        [Nullable]
        public bool IsPublished { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public string MetaTitle { get; set; }
        [Nullable]
        public string MetaDescription { get; set; }
        [Nullable]
        public string MetaKeywords { get; set; }

        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID")]
        public GalleryCategory Category { get; set; }
        [Association(ThisKey = "GalleryID", OtherKey = "GalleryID", CanBeNull = true)]
        public List<Photo> Photos { get; set; }
        [MapIgnore]
        public int CountPhotos { get; set; }


    }
}

