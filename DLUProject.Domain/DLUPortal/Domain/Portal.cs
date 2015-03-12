/* FileName: Portal.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:47:54 AM
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
    /// Represents a Portal
    /// </summary>
    [TableName("Portal")]
    public class Portal
    {
        [Identity, PrimaryKey]
        public int PortalID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Url { get; set; }
        [Nullable]
        public string Host { get; set; }
        [Nullable]
        public string LogoUrl { get; set; }
        [Nullable]
        public bool SSLEnable { get; set; }
        [Nullable]
        public string SecureUrl { get; set; }
        [Nullable]
        public bool IsDefault { get; set; }

    }
}

