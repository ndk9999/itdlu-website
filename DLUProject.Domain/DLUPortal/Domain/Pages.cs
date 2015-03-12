/* FileName: Pages.cs
Project Name: DLUProject
Date Created: 14/11/2014 2:49:18 PM
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
    /// Represents a Pages
    /// </summary>
    [TableName("Pages")]
    public class Pages
    {
        [PrimaryKey, Identity]
        public int PageID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [Nullable]
        public string Slug { get; set; }
        [Nullable]
        public string Description { get; set; }
        public string FullText { get; set; }
        [Nullable]
        public string Image { get; set; }
         [DefaultValue(true)]
        public bool IsPublished { get; set; }
        [DefaultValue(true)]
        public bool IsFrontPage { get; set; }
        [NullDateTime]
        public DateTime DateCreated { get; set; }
        [NullDateTime]
        public DateTime DateModified { get; set; }
        [DefaultValue(0)]
        public int ParentID { get; set; }
        [Nullable]
        public bool IsDeleted { get; set; }
        [Nullable]
        public string MetaTitle { get; set; }
        [Nullable]
        public string MetaDescription { get; set; }
        [Nullable]
        public string MetaKeywords { get; set; }

    }
}

