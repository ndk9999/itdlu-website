/* FileName: VideoProfile.cs
Project Name: DLUProject
Date Created: 17/12/2014 2:57:27 PM
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
    public partial class VideoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Video, VideoModel>();
            CreateMap<VideoModel, Video>();
        }
    }
}

