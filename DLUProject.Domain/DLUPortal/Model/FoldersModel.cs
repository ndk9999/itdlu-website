/* FileName: FoldersModel.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:16 AM
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DLUProject.Model
{
    /// <summary>
    /// Represents a FoldersModel
    /// </summary>
    public partial class FoldersModel
    {
        [Required]
        [Display(Name = "FolderID")]
        public int FolderID { get; set; }
        [Display(Name = "PortalID")]
        public int PortalID { get; set; }
        [Required, StringLength(300)]
        [Display(Name = "FolderPath")]
        public string FolderPath { get; set; }
        [Required]
        [Display(Name = "StorageLocation")]
        public int StorageLocation { get; set; }
        [Required]
        [Display(Name = "IsProtected")]
        public bool IsProtected { get; set; }
        [Required]
        [Display(Name = "IsCached")]
        public bool IsCached { get; set; }
        
        [Display(Name = "CreatedByID")]
        public int CreatedByID { get; set; }
        [Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "ModifiedByID")]
        public int ModifiedByID { get; set; }
        [Display(Name = "DateModified")]
        public DateTime DateModified { get; set; }
        [Display(Name = "ParentID")]
        public int ParentID { get; set; }
        [Required]
        [Display(Name = "IsVersioned")]
        public bool IsVersioned { get; set; }
        public string Description { get; set; }
    }
}

