/* FileName: DepartmentModel.cs
Project Name: DLUProject
Date Created: 20/11/2014 4:26:06 PM
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
    /// Represents a DepartmentModel
    /// </summary>
    public partial class DepartmentModel
    {
        [Required]
        [Display(Name = "DepartmentID")]
        public int DepartmentID { get; set; }
        [Required, StringLength(250)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "ShortName")]
        public string ShortName { get; set; }
        [Display(Name = "Alias")]
        public string Alias { get; set; }
        [StringLength(4000), DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "HeadOffice")]
        public string HeadOffice { get; set; }
        [Display(Name = "Dean")]
        public string Dean { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
        [Display(Name = "Website")]
        public string Website { get; set; }
        [Display(Name = "ParentID")]
        public int ParentID { get; set; }
        [Display(Name="SortOrder")]
        public int SortOrder { get; set; }
        [Display(Name = "DateStarted")]
        public DateTime DateStarted { get; set; }
          [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }

    }
}

