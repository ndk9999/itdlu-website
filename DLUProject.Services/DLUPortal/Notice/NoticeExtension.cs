/* 
FileName: NoticeExtension.cs
Project Name: DLUProject
Date Created: 18/11/2014 6:54:19 PM
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
using ColorLife.Core.Mvc;
namespace DLUProject.Services
{
	public class NoticeExtension
	{
		 IServices<Notice> _repository;
         public NoticeExtension(IServices<Notice> repo)
        {
            this._repository = repo;
        }

        public PagedList<Notice> GetAll(int pageIndex, int pageSize,   int categoryId, string queryString, bool isPublished)
        {
            var myList = _repository.All();
             
            if (categoryId > 0)
            {              
                myList = myList.Where(c => c.CategoryID.Equals(categoryId)).ToList();
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




