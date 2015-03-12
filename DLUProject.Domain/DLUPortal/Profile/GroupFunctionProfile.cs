/* 
FileName: GroupFunctionProfile.cs
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
using DLUProject.Domain;
using DLUProject.Domain;
namespace DLUProject.Profiles
{
	public partial class GroupFunctionProfile : Profile
	{

protected override void Configure()
                {
                    CreateMap<AccountGroupFunction, GroupFunctionModel>();
                    CreateMap<GroupFunctionModel, AccountGroupFunction>();
                }
	}
}


