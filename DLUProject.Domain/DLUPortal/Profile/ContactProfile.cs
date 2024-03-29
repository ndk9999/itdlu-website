﻿/* FileName: ContactProfile.cs
Project Name: DLUProject
Date Created: 17/11/2014 10:08:11 AM
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
    public partial class ContactProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Contact, ContactModel>();
            CreateMap<ContactModel, Contact>();
        }
    }
}

