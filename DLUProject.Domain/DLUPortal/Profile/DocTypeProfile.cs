/* FileName: DocTypeProfile.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:34:59 PM
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
    public partial class DocTypeProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<DocType, DocTypeModel>();
            CreateMap<DocTypeModel, DocType>();
        }
    }
}

