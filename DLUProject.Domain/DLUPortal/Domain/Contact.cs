/* FileName: Contact.cs
Project Name: DLUProject
Date Created: 17/11/2014 10:08:11 AM
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
    /// Represents a Contact
    /// </summary>
    [TableName("Contact")]
    public class Contact
    {
        [Identity, PrimaryKey]
        public int ContactID { get; set; }
        public string FullName { get; set; }
        [Nullable]
        public string Address { get; set; }
        [Nullable]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [NullDateTime]
        public DateTime DateCreated { get; set; }
        [DefaultValue(false)]
        public bool IsRead { get; set; }

    }
}
