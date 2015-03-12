 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DLUProject.Domain;
using DLUProject.Profiles;
using DLUProject.Model;
using AutoMapper;

namespace DLUProjectFramework.AutoMapper
{
    public static class AutoMapperConfig
    {
        
        public static void AutoMapConfigure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<CategoryProfile>();
               
                x.AddProfile<ContactProfile>();
                x.AddProfile<NoticeCategoryProfile>();
                x.AddProfile<MediaCategoryProfile>();
                x.AddProfile<GalleryCategoryProfile>();
                x.AddProfile<DocCategoryProfile>();
                x.AddProfile<VideoCategoryProfile>();
            });
        }
    }
}
