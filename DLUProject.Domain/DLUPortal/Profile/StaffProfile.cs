/* FileName: StaffProfile.cs
Project Name: DLUProject
Date Created: 22/11/2014 9:07:03 PM
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
    public partial class StaffProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Staff, StaffModel>();
            CreateMap<StaffModel, Staff>();
        }
    }
}

