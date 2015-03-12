/* FileName: Notice.cs
Project Name: DLUProject
Date Created: 18/11/2014 6:54:18 PM
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
    /// Represents a Notice
    /// </summary>
    [TableName("Notice")]
    public class Notice
    {
        [Identity, PrimaryKey]
        public int NoticeID { get; set; }
        
        public int CategoryID { get; set; }
       
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Alias { get; set; }
        
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public string FullText { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string  CreatedByID { get; set; }
        [Nullable]
        public string MetaTitle { get; set; }
        [Nullable]
        public string MetaKeywords { get; set; }
        [Nullable]
        public string MetaDescription { get; set; }
        [DefaultValue(true)]
        public bool IsPublished { get; set; }
        [DefaultValue(1)]
        public int SortOrder { get; set; }
        
        public DateTime DateCreated { get; set; }
        [Nullable]
        public DateTime DateModified { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        [DefaultValue(1)]
        public int Hits { get; set; }
        [Nullable]
        public string Url { get; set; }

        [DefaultValue(false)]
        public bool IsPin { get; set; }

        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = false )]
        public NoticeCategory Category { get; set; }

        [Association(ThisKey = "DepartmentID", OtherKey = "DepartmentID", CanBeNull = false )]
        public Department Department { get; set; }



        // FK_Product_Category
        [Association(ThisKey = "NoticeID", OtherKey = "NoticeID", CanBeNull = true)]
        public List<NoticeFileAttachment> NoticeFileAttachments { get; set; }

        [MapIgnore]
        public int CountFileAttachment { get; set; }

        [MapIgnore]
        public bool IsNew
        {
            get
            {
                return this.DateCreated.Date.Equals(DateTime.Now.Date);
            }
        }
        [MapIgnore]
        public string GetDateTime
        {
            get { return this.DateCreated == DateTime.MinValue ? "" : this.DateCreated.ToString("dd/MM/yyyy"); }
        }
    }
}

