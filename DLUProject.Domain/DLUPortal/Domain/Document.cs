/* FileName: Document.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:35:31 PM
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
    /// Represents a Document
    /// </summary>
    [TableName("Document")]
    public class Document
    {
        [PrimaryKey, Identity]
        public int DocumentID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [Nullable]
        public string Description { get; set; }
        public int CategoryID { get; set; }
       
        public int DocTypeID { get; set; }
         [Nullable]
        public int DepartmentID { get; set; }
        [Nullable]
        public string DocumentNo { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public DateTime DateEffected { get; set; }
        [Nullable]
        public DateTime DateEnded { get; set; }
        [Nullable]
        public DateTime DatePublished { get; set; }
        [DefaultValue(true)]
        public bool IsPublished { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = true)]
        public DocCategory Category { get; set; }

        [Association(ThisKey = "DocTypeID", OtherKey = "DocTypeID", CanBeNull = true)]
        public DocType DocType { get; set; }

        // FK_Product_Category
        [Association(ThisKey = "DocumentID", OtherKey = "DocumentID", CanBeNull = true)]
        public List<DocFileAttachment> DocFileAttachments { get; set; }

        [MapIgnore]
        public int CountFileAttachment { get; set; }
    }
}

