/* 
FileName: FoldersExtension.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:16 AM
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
	public class FoldersExtension
	{
		  IServices<Folders> _repository;
          public FoldersExtension(IServices<Folders> repo)
        {
            this._repository = repo;
        }

          public List<Folders> GetByParent(object id)
        {
           
            return _repository.All().Where(c => c.ParentID == id.ToInt()).ToList();
        }
        int level = -1;
        public List<Folders> GetByParent(List<Folders> list, int parentId)
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
            foreach (var m in GetByParent(parentId))
            {

                Folders d = new Folders
                {
                    FolderID = m.FolderID,
                    CreatedByID = m.CreatedByID,
                    DateCreated = m.DateCreated,
                    DateModified = m.DateModified,
                    FolderPath = m.FolderPath,
                    IsCached = m.IsCached,
                    IsProtected = m.IsProtected,
                    IsVersioned = m.IsVersioned,
                    
                    ModifiedByID = m.ModifiedByID,
                    ParentID = m.ParentID,
                    PortalID = m.PortalID,
                    StorageLocation = m.StorageLocation,
                    Description = m.Description,
                    Breadcrumb = GetFormattedBreadCrumb(m, ">>"),
                };
                list.Add(d);
                GetByParent(list, d.FolderID);
            }
            level--;
            return list;
        }
        public List<Folders> GetAllDropdownList()
        {
            var myList = GetByParent(new List<Folders>(), 0);
            myList.Insert(0, new Folders { FolderID = 0, Breadcrumb = "---Chọn thư mục---" });
            return myList;
        }
        public List<Folders> GetAll(string queryString)
        {
            var myList = GetByParent(new List<Folders>(), 0);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.FolderPath, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList;
        }
        public PagedList<Folders> GetAllDepartments(int pageIndex, int pageSize, string queryString)
        {
            var myList = GetByParent(new List<Folders>(), 0);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.FolderPath, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList.ToPagedList(pageIndex, pageSize);
        }
        public string GetFormattedBreadCrumb(Folders category, string separator = ">>")
        {
            if (category == null)
                throw new ArgumentNullException("category");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>() { };

            while (category != null && !alreadyProcessedCategoryIds.Contains(category.FolderID)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.FolderPath;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.FolderPath, separator, result);
                }

                alreadyProcessedCategoryIds.Add(category.FolderID);

                category = _repository.Get(category.ParentID);

            }
            return result;
        }
	}
}




