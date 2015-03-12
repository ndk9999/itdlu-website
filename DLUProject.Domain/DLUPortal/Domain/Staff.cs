/* FileName: Staff.cs
Project Name: DLUProject
Date Created: 22/11/2014 9:07:03 PM
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
    /// Represents a Staff
    /// </summary>
    [TableName("Staff")]
    public class Staff
    {
        [Identity, PrimaryKey]
        public int StaffID { get; set; }
        public string StaffNoID { get; set; }
        [Nullable]
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Nullable]
        public DateTime DOB { get; set; }
        [Nullable]
        public string Phone { get; set; }
        [Nullable]
        public string Fax { get; set; }
        [Nullable]
        public string Mobile { get; set; }
        
        public string Email { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string Degree { get; set; }
        [Nullable]
        public string Position { get; set; }
        [Nullable]
        public string Description { get; set; }

        [MapIgnore]
        public string FullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
        [MapIgnore]
        public string FullName2
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Degree))
                    return string.Format("{0}. {1} {2}", this.Degree, this.FirstName, this.LastName);
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

    }
}

