/* 
FileName: DocCategoryExtension.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DLUProject.Domain;
using ColorLife.Core.Helper;
using ColorLife.Core.Mvc;
namespace DLUProject.Services
{
	public class DocCategoryExtension
	{
		 IServices<DocCategory> _repository;
         public DocCategoryExtension(IServices<DocCategory> repo)
        {
            this._repository = repo;
        }

         public List<DocCategory> GetByParent(object id, bool isPublished)
        {
            if (isPublished)
                return _repository.All().Where(c => c.IsPublished == true && c.ParentID == id.ToInt()).OrderBy(c => c.SortOrder).ThenBy(c=>c.ParentID).ToList();
            return _repository.All().Where(c => c.ParentID == id.ToInt()).OrderBy(c => c.SortOrder).ThenBy(c => c.ParentID).ToList();
        }
        int level = -1;
        public List<DocCategory> GetByParent(List<DocCategory> list, int parentId, bool isPublished)
        {
            level++;
            string x = "";
            for (int j = 0; j < level; j++)
            {
                // x += "&nbsp;&nbsp;&nbsp;&nbsp;";
                x += "--";
            }
            if (level > 0)
            {
                x += "-- ";
            }
            foreach (var d in GetByParent(parentId, isPublished))
            {

                DocCategory m = new DocCategory
                {
                    CategoryID = d.CategoryID,
                    Name = d.Name,
                    Alias = d.Alias,
                    Description = d.Description,
                    Image = d.Image,
                    ParentID = d.ParentID,
                    SortOrder = d.SortOrder,
                    IsPublished = d.IsPublished,
                   
                    Breadcrumb = GetFormattedBreadCrumb(d, ">>"),
                    
                };
                list.Add(m);
                GetByParent(list, m.CategoryID, isPublished);
            }
            level--;
            return list;
        }
        public List<DocCategory> GetAllDropdownList(bool isPublished)
        {
            var myList = GetByParent(new List<DocCategory>(), 0, isPublished);
            myList.Insert(0, new DocCategory { CategoryID = 0, Breadcrumb = "---Chọn danh mục---" });
            return myList;
        }
        public List<DocCategory> GetAll(string queryString, bool isPublished)
        {
            var myList = GetByParent(new List<DocCategory>(), 0, isPublished);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList;
        }
        public PagedList<DocCategory> GetAllCategories(int pageIndex, int pageSize, string queryString, bool isPublished)
        {
            var myList = GetByParent(new List<DocCategory>(), 0, isPublished);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList.ToPagedList(pageIndex, pageSize);
        }
        public string GetFormattedBreadCrumb(DocCategory category, string separator = ">>")
        {
            if (category == null)
                throw new ArgumentNullException("category");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>() { };

            while (category != null && !alreadyProcessedCategoryIds.Contains(category.CategoryID)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.Name, separator, result);
                }

                alreadyProcessedCategoryIds.Add(category.CategoryID);

                category = _repository.Get(category.ParentID);

            }
            return result;
        }

         
    }
}







