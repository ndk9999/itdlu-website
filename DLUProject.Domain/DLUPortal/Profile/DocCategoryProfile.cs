﻿/* FileName: DocCategoryProfile.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:35:11 PM
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
    public partial class DocCategoryProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<DocCategory, CategoryModel>();
            CreateMap<CategoryModel, DocCategory>();
        }
    }
}

