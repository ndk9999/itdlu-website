/* FileName: GalleryProfile.cs
Project Name: DLUProject
Date Created: 28/11/2014 2:03:13 PM
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
    public partial class GalleryProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Gallery, GalleryModel>();
            CreateMap<GalleryModel, Gallery>();
        }
    }
}

