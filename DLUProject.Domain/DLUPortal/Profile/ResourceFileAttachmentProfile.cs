﻿/* FileName: ResourceFileAttachmentProfile.cs
Project Name: DLUProject
Date Created: 2/3/2015 8:54:39 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/
using System;
using AutoMapper;
using DLUProject.Domain;
using DLUProject.Model;
namespace DLUProject.Profiles
{
    public partial class ResourceFileAttachmentProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ResourceFileAttachment, ResourceFileAttachmentModel>();
            CreateMap<ResourceFileAttachmentModel, ResourceFileAttachment>();
        }
    }
}

