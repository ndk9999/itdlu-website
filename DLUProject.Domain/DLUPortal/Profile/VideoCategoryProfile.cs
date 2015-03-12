/* FileName: VideoCategoryProfile.cs
Project Name: DLUProject
Date Created: 17/12/2014 3:02:49 PM
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
    public partial class VideoCategoryProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<VideoCategory,  CategoryModel>();
            CreateMap<CategoryModel, VideoCategory>();
        }
    }
}

