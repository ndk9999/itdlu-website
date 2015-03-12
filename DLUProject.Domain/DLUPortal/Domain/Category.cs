/* FileName: Category.cs
Project Name: DLUProject
Date Created: 11/11/2014 2:46:35 PM
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
    /// Represents a Category
    /// </summary>
    [TableName("Category")]
    public class Category
    {
        [Identity, PrimaryKey]
        public int CategoryID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string Description { get; set; }
        [DefaultValue(0)]
        public int ParentID { get; set; }
        [DefaultValue(0)]
        public int SortOrder { get; set; }
        [DefaultValue(true)]
        public bool IsPublished { get; set; }
        [Nullable]
        public string MetaTitle { get; set; }
        [Nullable]
        public string MetaDescription { get; set; }
        [Nullable]
        public string MetaKeywords { get; set; }
        

        // FK_Product_Category
        [Association(ThisKey = "ParentID", OtherKey = "ParentID", CanBeNull = true)]
        public Category Parent { get; set; }

        [MapIgnore]
        public string Breadcrumb { get; set; }
    }
}

