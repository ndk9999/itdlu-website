/* FileName: Media.cs
Project Name: DLUProject
Date Created: 27/11/2014 9:15:32 PM
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
    /// Represents a Media
    /// </summary>
    [TableName("Media")]
    public class Media
    {
        [Identity, PrimaryKey]
        public int MediaID { get; set; }
        public int AlbumID { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [Nullable]
        public string Slug { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string MediaType { get; set; }
        [Nullable]
        public string MimeType { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string FileUrl { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public string Caption { get; set; }
        [Nullable]
        public string AltTag { get; set; }
        [Nullable]
        public string Copyright { get; set; }
        [Nullable]
        public int SortOrder { get; set; }
        [Nullable]
        public bool IsPublished { get; set; }
        [Nullable]
        public int CreatedByID { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public int ModifiedByID { get; set; }
        [Nullable]
        public DateTime DateModified { get; set; }
        [Nullable]
        public DateTime DatePublished { get; set; }
        [Nullable]
        public int Hits { get; set; }
        [Nullable]
        public bool IsDeleted { get; set; }

    }
}

