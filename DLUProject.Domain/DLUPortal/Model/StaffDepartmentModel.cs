/* FileName: StaffDepartmentModel.cs
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DLUProject.Domain;
namespace DLUProject.Model
{
    /// <summary>
    /// Represents a StaffDepartmentModel
    /// </summary>
    public partial class StaffDepartmentModel
    {
        public Staff Staff { get; set; }
        public List<Department> Departments { get; set; }
        public List<StaffDepartment> StaffDepartments { get; set; }
    }
}

