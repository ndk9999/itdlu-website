/* FileName: Photo.cs
Project Name: DLUProject
Date Created: 28/11/2014 2:03:05 PM
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
    /// Represents a Photo
    /// </summary>
    [TableName("Photo")]
    public class Photo
    {
        [Identity, PrimaryKey]
        public int PhotoID { get; set; }
        public int GalleryID { get; set; }
        [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string Caption { get; set; }
        [Nullable]
        public string Description { get; set; }

    }
}

