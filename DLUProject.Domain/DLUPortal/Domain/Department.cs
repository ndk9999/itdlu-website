/* FileName: Department.cs
Project Name: DLUProject
Date Created: 20/11/2014 4:26:06 PM
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
    /// Represents a Department
    /// </summary>
    [TableName("Department")]
    public class Department
    {
        [Identity, PrimaryKey]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string ShortName { get; set; }
        [Nullable]
        public string Alias { get; set; }
        [Nullable]
        public string Description { get; set; }
        [Nullable]
        public string HeadOffice { get; set; }
        [Nullable]
        public string Dean { get; set; }
        [Nullable]
        public string Address { get; set; }
        [Nullable]
        public string Email { get; set; }
        [Nullable]
        public string Phone { get; set; }
        [Nullable]
        public string Fax { get; set; }
        [Nullable]
        public string Website { get; set; }
        [DefaultValue(0)]
        public int ParentID { get; set; }
        [DefaultValue(0)]
        public int SortOrder { get; set; }
        [Nullable]
        public DateTime DateStarted { get; set; }
        [DefaultValue(true)]
        public bool IsPublished { get; set; }

        [MapIgnore]
        public string Breadcrumb { get; set; }
    }
}

