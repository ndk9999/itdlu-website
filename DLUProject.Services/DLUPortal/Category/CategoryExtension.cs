using DLUProject.Data;
using DLUProject.Domain;
/* 
FileName: BlogCategoryExtension.cs
Project Name: ColorBlogProject
Date Created: 28/6/2014 7:16:15 PM
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
using ColorLife.Core.Helper;
using ColorLife.Core.Mvc;
namespace DLUProject.Services
{
    public class CategoryExtension
    {
        // YOUR CODE HERE
        IServices<Category> _repository;
        public CategoryExtension(IServices<Category> repo)
        {
            this._repository = repo;
        }
       
        public List<Category> GetByParent(object id, bool isPublished)
        {
            if (isPublished)
                return _repository.All().Where(c => c.IsPublished == true && c.ParentID == id.ToInt()).OrderBy(c => c.SortOrder).ThenBy(c=>c.ParentID).ToList();
            return _repository.All().Where(c => c.ParentID == id.ToInt()).OrderBy(c => c.SortOrder).ThenBy(c => c.ParentID).ToList();
        }
        int level = -1;
        public List<Category> GetByParent(List<Category> list, int parentId, bool isPublished)
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

                Category m = new Category
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
        public List<Category> GetAllDropdownList(bool isPublished)
        {
            var myList = GetByParent(new List<Category>(), 0, isPublished);
            myList.Insert(0, new Category { CategoryID = 0, Breadcrumb = "---Chọn danh mục---" });
            return myList;
        }
        public List<Category> GetAll(string queryString, bool isPublished)
        {
            var myList = GetByParent(new List<Category>(), 0, isPublished);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList;
        }
        public PagedList<Category> GetAllCategories(int pageIndex, int pageSize, string queryString, bool isPublished)
        {
            var myList = GetByParent(new List<Category>(), 0, isPublished);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList.ToPagedList(pageIndex, pageSize);
        }
        public string GetFormattedBreadCrumb(Category category, string separator = ">>")
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

        /// <summary>
        /// Get formatted category breadcrumb 
        /// Note: ACL and store mapping is ignored
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="allCategories">All categories</param>
        /// <param name="separator">Separator</param>
        /// <returns>Formatted breadcrumb</returns>
        public string GetFormattedBreadCrumb(Category category,
            List<Category> allCategories,
            string separator = ">>")
        {
            if (category == null)
                throw new ArgumentNullException("category");

            if (allCategories == null)
                throw new ArgumentNullException("category");

            string result = string.Empty;

            //used to prevent circular references 
            var alreadyProcessedCategoryIds = new List<int>() { };

            while (category != null && //not null 

                   !alreadyProcessedCategoryIds.Contains(category.CategoryID)) //prevent circular references 
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

                category = (from c in allCategories
                            where c.CategoryID == category.ParentID
                            select c).FirstOrDefault();
            }
            return result;
        }
    }
}




