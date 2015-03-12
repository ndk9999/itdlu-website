/* FileName: DocFileAttachment.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:35:17 PM
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
    /// Represents a DocFileAttachment
    /// </summary>
    [TableName("DocFileAttachment")]
    public class DocFileAttachment
    {
                public int DocumentID { get; set; }
        public int FileID { get; set; }

    }
}

