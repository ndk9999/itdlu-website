/* FileName: ResourceFileAttachment.cs
Project Name: DLUProject
Date Created: 2/3/2015 8:54:39 AM
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
    /// Represents a ResourceFileAttachment
    /// </summary>
    [TableName("ResourceFileAttachment")]
    public class ResourceFileAttachment
    {
                public int ResourceID { get; set; }
        public int FileID { get; set; }

    }
}

