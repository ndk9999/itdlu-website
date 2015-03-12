/* 
FileName: ContentExtension.cs
Project Name: DLUProject
Date Created: 18/11/2014 10:53:56 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DLUProject.Domain;
using DLUProject.Data;
using ColorLife.Core.Mvc;
using ColorLife.Core.Helper;
namespace DLUProject.Services
{
    public class ContentExtension
    {
        // YOUR CODE HERE
        IServices<Content> _repository;
        public ContentExtension(IServices<Content> repo)
        {
            this._repository = repo;
        }

        public PagedList<Content> GetAllContent(int pageIndex, int pageSize, string DisplayFlag, int categoryId, string queryString, bool isPublished)
        {
            var myList = _repository.All();
            var display = DisplayFlagContent.FontPage;
            if (!string.IsNullOrEmpty(DisplayFlag))
            {
                display = ConvertType.ToEnum<DisplayFlagContent>(DisplayFlag);
                myList = myList.Where(c => c.IsDisplayFlag(display)).ToList();
            }
            if (categoryId > 0)
            {              
                myList = myList.Where(c => c.CategoryID.Equals(categoryId)).ToList();
            }
            if (categoryId > 0 && !string.IsNullOrEmpty( DisplayFlag))
            {
                display = ConvertType.ToEnum<DisplayFlagContent>(DisplayFlag);
                myList = myList.Where(c => c.CategoryID.Equals(categoryId) && c.IsDisplayFlag(display)).ToList();
            }
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }

            if (categoryId> 0 && !String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => c.CategoryID.Equals(categoryId) && string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList.ToPagedList(pageIndex, pageSize);
        }
    }
}




