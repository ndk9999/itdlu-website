﻿/* FileName: ResourceCategory.cs
Project Name: DLUProject
Date Created: 2/3/2015 8:54:11 AM
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
    /// Represents a ResourceCategory
    /// </summary>
    [TableName("ResourceCategory")]
    public class ResourceCategory
    {
                public int CategoryID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public int ParentID { get; set; }
        [Nullable]
        public int SortOrder { get; set; }
        [Nullable]
        public bool IsPublished { get; set; }
        [Nullable]
        public string MetaTitle { get; set; }
        [Nullable]
        public string MetaDescription { get; set; }
        [Nullable]
        public string MetaKeywords { get; set; }

    }
}

