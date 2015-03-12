/* FileName: GalleryCategory.cs
Project Name: DLUProject
Date Created: 28/11/2014 2:02:55 PM
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
    /// Represents a GalleryCategory
    /// </summary>
    [TableName("GalleryCategory")]
    public class GalleryCategory:Category
    {
        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = true)]
        public List<Gallery> Galleries { get; set; }
        [MapIgnore]
        public int CountGallery { get; set; }

    }
}

