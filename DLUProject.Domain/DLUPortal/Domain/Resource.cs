/* FileName: Resource.cs
Project Name: DLUProject
Date Created: 2/3/2015 8:54:58 AM
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
    /// Represents a Resource
    /// </summary>
    [TableName("Resource")]
    public class Resource
    {
        [Identity, PrimaryKey]
        public int ResourceID { get; set; }
        public int CategoryID { get; set; }
        [Nullable]
        public string Title { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public string ResourceType { get; set; }
        [Nullable]
        public int CreatedByID { get; set; }
        [Nullable]
        public DateTime DateCreated { get; set; }
        [Nullable]
        public int ModifiedByID { get; set; }
        [Nullable]
        public DateTime DateModified { get; set; }
        [Nullable]
        public bool IsPublished { get; set; }

    }
}

