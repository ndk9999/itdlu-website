/* FileName: PagesProfile.cs
Project Name: DLUProject
Date Created: 14/11/2014 2:49:18 PM
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
    public partial class PagesProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Pages, PagesModel>();
            CreateMap<PagesModel, Pages>();
        }
    }
}

