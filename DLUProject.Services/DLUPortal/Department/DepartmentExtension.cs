/* 
FileName: DepartmentExtension.cs
Project Name: DLUProject
Date Created: 20/11/2014 4:03:44 PM
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
using ColorLife.Core.Helper;
namespace DLUProject.Services
{
    public class DepartmentExtension
    {
        IServices<Department> _repository;
        public DepartmentExtension(IServices<Department> repo)
        {
            this._repository = repo;
        }

        public List<Department> GetByParent(object id)
        {

            return _repository.All().Where(c => c.ParentID == id.ToInt()).ToList();
        }
        int level = -1;
        public List<Department> GetByParent(List<Department> list, int parentId)
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
            foreach (var d in GetByParent(parentId))
            {

                Department m = new Department
                {
                    DepartmentID = d.DepartmentID,
                    Name = d.Name,
                    Alias = d.Alias,
                    Description = d.Description,

                    ParentID = d.ParentID,
                    ShortName = d.ShortName,
                    Address = d.Address,
                    DateStarted = d.DateStarted,
                    Dean = d.Dean,
                    Email = d.Email,
                    Fax = d.Fax,
                    HeadOffice = d.HeadOffice,
                    Phone = d.Phone,
                    Website = d.Website,
                     IsPublished=d.IsPublished,
                      SortOrder=d.SortOrder,
                    Breadcrumb = GetFormattedBreadCrumb(d, ">>"),

                };
                list.Add(m);
                GetByParent(list, m.DepartmentID);
            }
            level--;
            return list;
        }
        public List<Department> GetAllDropdownList()
        {
            var myList = GetByParent(new List<Department>(), 0);
           // myList.Insert(0, new Department { DepartmentID = 0, Breadcrumb = "---Chọn Phòng ban---" });
            return myList;
        }
        public List<Department> GetAll(string queryString)
        {
            var myList = GetByParent(new List<Department>(), 0);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList;
        }
        public PagedList<Department> GetAllDepartments(int pageIndex, int pageSize, string queryString)
        {
            var myList = GetByParent(new List<Department>(), 0);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList.ToPagedList(pageIndex, pageSize);
        }
        public string GetFormattedBreadCrumb(Department category, string separator = ">>")
        {
            if (category == null)
                throw new ArgumentNullException("category");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>() { };

            while (category != null && !alreadyProcessedCategoryIds.Contains(category.DepartmentID)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.Name, separator, result);
                }

                alreadyProcessedCategoryIds.Add(category.DepartmentID);

                category = _repository.Get(category.ParentID);

            }
            return result;
        }


    }
}








