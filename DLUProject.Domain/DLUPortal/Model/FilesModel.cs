/* FileName: FilesModel.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:51 AM
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
    /// Represents a FilesModel
    /// </summary>
    public partial class FilesModel
    {
        [Required]
        [Display(Name = "FileId")]
        public int FileId { get; set; }
        [Required]
        [Display(Name = "FolderID")]
        public int FolderID { get; set; }
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Required, StringLength(246)]
        [Display(Name = "FileName")]
        public string FileName { get; set; }
        [Required, StringLength(100)]
        [Display(Name = "Extension")]
        public string Extension { get; set; }
        [Required]
        [Display(Name = "Size")]
        public string  Size { get; set; }
        [Display(Name = "Width")]
        public int Width { get; set; }
        [Display(Name = "Height")]
        public int Height { get; set; }
        [Display(Name = "ContentType")]
        public string ContentType { get; set; }
        [Display(Name = "Caption")]
        public string Caption { get; set; }
        [Display(Name = "AltTag")]
        public string AltTag { get; set; }
        [Display(Name = "FileUrl")]
        public string FileUrl { get; set; }
        [Display(Name = "CreatedByID")]
        public int CreatedByID { get; set; }
        [Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "ModifiedByID")]
        public int ModifiedByID { get; set; }
        [Display(Name = "DateModified")]
        public DateTime DateModified { get; set; }
        [Display(Name = "MD5Hash")]
        public string MD5Hash { get; set; }
        [Required]
        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }

    }
}
