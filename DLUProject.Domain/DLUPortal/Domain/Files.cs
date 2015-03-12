/* FileName: Files.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:51 AM
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
    /// Represents a Files
    /// </summary>
    [TableName("Files")]
    public class Files
    {
        [PrimaryKey, Identity]
        public int FileId { get; set; }
        public int FolderID { get; set; }
        [Nullable]
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string  Size { get; set; }
        [Nullable]
        public int Width { get; set; }
        [Nullable]
        public int Height { get; set; }
        [Nullable]
        public string ContentType { get; set; }
        [Nullable]
        public string Caption { get; set; }
        [Nullable]
        public string AltTag { get; set; }
        [Nullable]
        public string FileUrl { get; set; }
        [Nullable]
        public int CreatedByID { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public int ModifiedByID { get; set; }
        [Nullable]
        public DateTime DateModified { get; set; }
        [Nullable]
        public string MD5Hash { get; set; }
        public bool IsPublished { get; set; }

        [Association(ThisKey = "FolderID", OtherKey = "FolderID")]
        public Folders Folders { get; set; }
    }
}

