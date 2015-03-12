/* FileName: StaffDepartment.cs
Project Name: DLUProject
Date Created: 22/11/2014 9:11:49 PM
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
    /// Represents a StaffDepartment
    /// </summary>
    [TableName("StaffDepartment")]
    public class StaffDepartment
    {
        [Identity, PrimaryKey]
        public int ID { get; set; }
        public int StaffID { get; set; }
        public int DepartmentID { get; set; }

        [Association(ThisKey = "StaffID", OtherKey = "StaffID")]
        public Staff Staff { get; set; }

        [Association(ThisKey = "DepartmentID", OtherKey = "DepartmentID")]
        public Department Department { get; set; }

    }
}

