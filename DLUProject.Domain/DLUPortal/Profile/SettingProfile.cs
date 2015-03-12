/* FileName: SettingProfile.cs
Project Name: DLUProject
Date Created: 14/11/2014 1:30:15 PM
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
    public partial class SettingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Setting, SettingModel>();
            CreateMap<SettingModel, Setting>();
        }
    }
}

