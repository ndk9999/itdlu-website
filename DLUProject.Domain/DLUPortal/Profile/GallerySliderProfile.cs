/* FileName: GallerySliderProfile.cs
Project Name: DLUProject
Date Created: 14/11/2014 11:05:10 AM
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
    public partial class GallerySliderProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<GallerySlider, GallerySliderModel>();
            CreateMap<GallerySliderModel, GallerySlider>();
        }
    }
}

