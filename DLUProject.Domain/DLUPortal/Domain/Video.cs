/* FileName: Video.cs
Project Name: DLUProject
Date Created: 17/12/2014 2:57:27 PM
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
    /// Represents a Video
    /// </summary>
    [TableName("Video")]
    public class Video
    {
        [Identity, PrimaryKey]
        public int VideoID { get; set; }
        public int GalleryID { get; set; }
        [Nullable]
        public int DataSource { get; set; }
        public string Name { get; set; }
        public string VideoUrl { get; set; }

         [Nullable]
        public string Image { get; set; }
        [Nullable]
        public string Caption { get; set; }
        [Nullable]
        public string Description { get; set; }

    }
}

