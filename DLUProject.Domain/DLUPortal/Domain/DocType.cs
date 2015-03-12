/* FileName: DocType.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:34:59 PM
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
    /// Represents a DocType
    /// </summary>
    [TableName("DocType")]
    public class DocType
    {
        [Identity, PrimaryKey]
        public int DocTypeID { get; set; }
        public string Name { get; set; }
        [Nullable]
        public string Description { get; set; }

    }
}

