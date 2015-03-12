/* FileName: Content.cs
Project Name: DLUProject
Date Created: 18/11/2014 10:53:56 AM
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
    /// Represents a Content
    /// </summary>
    [TableName("Content")]
    public class Content
    {
        [Identity, PrimaryKey]
        public int ContentID { get; set; }
        [Nullable]
        public int CategoryID { get; set; }
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [Nullable]
        public string Slug { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public string MetaTitle { get; set; }
        [Nullable]
        public string MetaDescription { get; set; }
        [Nullable]
        public string MetaKeywords { get; set; }
        public string FullText { get; set; }
        [Nullable]
        public string Image { get; set; }
        public int CreatedById { get; set; }
        [Nullable]
        public string ModifiedBy { get; set; }
        [Nullable]
        public int Hits { get; set; }
        [DefaultValue(true)]
        public bool IsPublished { get; set; }
        [DefaultValue(false)]
        public bool IsCommentEnable { get; set; }
        [Nullable]
        public int State { get; set; }
        [Nullable]
        public int DisplayFlags { get; set; }
        [Nullable]
        public string CheckoutBy { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public DateTime DateModified { get; set; }
        [Nullable]
        public DateTime DateCheckout { get; set; }
        [Nullable]
        public DateTime DatePublished { get; set; }
        [Nullable]
        public string ContentType { get; set; }
        [DefaultValue(1)]
        public int SortOrder { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public string Author { get; set; }
        public string Source { get; set; }

        // FK_Product_Category
        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = false)]
        public Category Category { get; set; }
        public bool IsDisplayFlag(DisplayFlagContent flag)
        {
            return (((int)flag) & DisplayFlags) == (int)flag;
        }
        public List<string> DisplayFeaturedString()
        {
            List<string> s = new List<string>();
            foreach (var item in Enum.GetValues(typeof(DisplayFlagContent)))
            {
                var x = (DisplayFlagContent)Enum.Parse(typeof(DisplayFlagContent), item.ToString(), true);
                if (this.IsDisplayFlag(x)) s.Add(item.ToString());
            }
            return s;
        }
    }
}

