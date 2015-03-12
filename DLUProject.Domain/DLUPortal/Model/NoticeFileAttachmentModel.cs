/* FileName: DocFileAttachmentModel.cs
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DLUProject.Model
{
    /// <summary>
    /// Represents a DocFileAttachmentModel
    /// </summary>
    public partial class NoticeFileAttachmentModel
    {
        [Required]
        [Display(Name = "NoticeID")]
        public int NoticeID { get; set; }
        [Required]
        [Display(Name = "FileID")]
        public int FileID { get; set; }

    }
}

