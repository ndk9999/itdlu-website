/* FileName: MediaAlbum.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:53:07 AM
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
    /// Represents a MediaAlbum
    /// </summary>
    [TableName("MediaAlbum")]
    public class MediaAlbum
    {
        [Identity, PrimaryKey]
        public int AlbumID { get; set; }
        [Nullable]
        public int CategoryID { get; set; }
        [Nullable]
        public string Alias { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public int SortOrder { get; set; }
        [Nullable]
        public bool IsPublished { get; set; }
        [Nullable]
        public int CreatedByID { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public string MetaTitle { get; set; }
        [Nullable]
        public string MetaDescription { get; set; }
        [Nullable]
        public string MetaKeywords { get; set; }

        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID")]
        public MediaCategory Category { get; set; }

        [Association(ThisKey = "AlbumID", OtherKey = "AlbumID", CanBeNull = true)]
        public List<Media> Medias { get; set; }
        [MapIgnore]
        public int CountMedias { get; set; }
    }
}

