/* FileName: Folders.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:16 AM
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
    /// Represents a Folders
    /// </summary>
    [TableName("Folders")]
    public class Folders
    {
        [PrimaryKey, Identity]
        public int FolderID { get; set; }
        [Nullable]
        public int PortalID { get; set; }
        public string FolderPath { get; set; }
        public int StorageLocation { get; set; }
        public bool IsProtected { get; set; }
        public bool IsCached { get; set; }
        
        [Nullable]
        public int CreatedByID { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public int ModifiedByID { get; set; }
        [Nullable]
        public DateTime DateModified { get; set; }
        [Nullable]
        public int ParentID { get; set; }
        public bool IsVersioned { get; set; }
        public string Description { get; set; }
        [MapIgnore]
        public string Breadcrumb { get; set; }
    }
}

