/* 
FileName: SystemsProfile.cs
Project Name: DLUProject
Date Created: 8/5/2014 7:29:01 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/
using System;
using AutoMapper;
namespace DLUProject.Domain
{
	public partial class SystemsProfile : Profile
	{

protected override void Configure()
                {
                    CreateMap<Systems, SystemsModel>();
                    CreateMap<SystemsModel, Systems>();
                }
	}
}


