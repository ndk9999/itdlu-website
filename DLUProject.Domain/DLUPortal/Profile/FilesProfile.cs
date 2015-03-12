/* FileName: FilesProfile.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:51 AM
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
    public partial class FilesProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Files, FilesModel>();
            CreateMap<FilesModel, Files>();
        }
    }
}

