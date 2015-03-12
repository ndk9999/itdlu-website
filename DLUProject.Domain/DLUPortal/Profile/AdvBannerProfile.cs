/* FileName: AdvBannerProfile.cs
Project Name: DLUProject
Date Created: 24/11/2014 12:48:05 PM
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
    public partial class AdvBannerProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AdvBanner, AdvBannerModel>();
            CreateMap<AdvBannerModel, AdvBanner>();
        }
    }
}

