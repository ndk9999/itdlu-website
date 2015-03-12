/* FileName: ContentProfile.cs
Project Name: DLUProject
Date Created: 18/11/2014 10:53:56 AM
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
    public partial class ContentProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Content, ContentModel>();
            CreateMap<ContentModel, Content>();
        }
    }
}

