/* FileName: NoticeCategory.cs
Project Name: DLUProject
Date Created: 18/11/2014 6:53:53 PM
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
    /// Represents a MediaCategory
    /// </summary>
    [TableName("MediaCategory")]
    public class MediaCategory : Category
    {
        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = true)]
        public List<MediaAlbum> MediaAlbums { get; set; }
        [MapIgnore]
        public int CountAlbums { get; set; }
    }
}

