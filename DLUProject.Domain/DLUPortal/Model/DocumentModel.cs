/* FileName: DocumentModel.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:35:31 PM
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
    /// Represents a DocumentModel
    /// </summary>
    public partial class DocumentModel
    {
        [Display(Name = "DocumentID")]
        public int DocumentID { get; set; } 
        [Required, StringLength(250)]
[Display(Name = "Name")]
        public string Name { get; set; } 
[Display(Name = "Alias")]
        public string Alias { get; set; } 
        [StringLength(4000), DataType(DataType.MultilineText)]
[Display(Name = "Description")]
        public string Description { get; set; } 
[Required]
[Display(Name = "CategoryID")]
        public int CategoryID { get; set; } 
[Display(Name = "DocTypeID")]
        public int DocTypeID { get; set; } 
[Required]
[Display(Name = "DepartmentID")]
        public int DepartmentID { get; set; } 
[Display(Name = "DocumentNo")]
        public string DocumentNo { get; set; } 
[Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; } 
[Display(Name = "DateEffected")]
        public DateTime DateEffected { get; set; } 
[Display(Name = "DateEnded")]
        public DateTime DateEnded { get; set; } 
[Display(Name = "DatePublished")]
        public DateTime DatePublished { get; set; } 
[Display(Name = "IsPublished")]
        public bool IsPublished { get; set; } 
[Display(Name = "IsDeleted")]
        public bool IsDeleted { get; set; } 

    }
}

