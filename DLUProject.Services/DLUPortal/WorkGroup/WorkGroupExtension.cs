/* 
FileName: IWorkGroupExtension.cs
Project Name: ColorLife.Data
Date Created: 13/5/2014 7:41:41 AM
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
using System.Web;
using DLUProject.Data;
using DLUProject.Domain;
using ColorLife.Core.Mvc;
namespace DLUProject.Services
{
    public class WorkGroupExtension
    {
        // YOUR CODE HERE
        IRepository<WorkGroup> _repository;
        public WorkGroupExtension(IRepository<WorkGroup> repo)
        {
            this._repository = repo;
        }
        public List<WorkGroup> GetByParent(int parentID, bool isPublished)
        {
            var data = _repository.Table.Where(c => c.ParentID == parentID);
            if (isPublished)
                data = data.Where(c => c.IsEnabled == true);

            return data.Select(d => new WorkGroup
            {

                DisplayFlags = d.DisplayFlags,
                Image = d.Image,
                SystemID = d.SystemID,
                Name = d.Name,
                Systems = d.Systems,
                SortOrder = d.SortOrder,
                IsEnabled = d.IsEnabled,
                ParentID = d.ParentID,
                Url = d.Url,
                WorkGroupID = d.WorkGroupID
            }).OrderBy(c => c.SystemID).ThenBy(c => c.ParentID).ThenBy(c=>c.SortOrder).ToList();
        }
        int level = -1;
        public List<WorkGroup> GetByParent(List<WorkGroup> list, int parentId, bool isPublished)
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

                WorkGroup m = new WorkGroup
                {
                    WorkGroupID = d.WorkGroupID,
                    Name = d.Name,
                    IsEnabled = d.IsEnabled,
                    Image = d.Image,
                    ParentID = d.ParentID,
                    SortOrder = d.SortOrder,
                    SystemID = d.SystemID,
                    Url = d.Url,

                    DisplayFlags = d.DisplayFlags,

                    Breadcrumb = d.Systems.Name + ">> " + GetFormattedBreadCrumb(d, ">>"),

                };
                list.Add(m);
                GetByParent(list, m.WorkGroupID, isPublished);
            }
            level--;
            return list;
        }
        public List<WorkGroup> GetAllDropdownList(bool isPublished)
        {
            var myList = GetByParent(new List<WorkGroup>(), 0, isPublished);
           // myList.Insert(0, new WorkGroup {  Breadcrumb = "---Chọn danh mục---" });
            return myList;
        }
        public List<WorkGroup> GetAll(string queryString, bool isPublished)
        {
            var myList = GetByParent(new List<WorkGroup>(), 0, isPublished);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Url).ToLower().Contains(queryString)).ToList();
            }
            return myList;
        }
        public PagedList<WorkGroup> GetAllWorkGroup(int pageIndex, int pageSize, string queryString, int sysId, bool isPublished)
        {
            var myList = GetByParent(new List<WorkGroup>(), 0, isPublished);
            if (sysId > 0)
            {
                myList = myList.Where(c => c.SystemID == sysId).ToList();
            }
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Url).ToLower().Contains(queryString)).ToList();
            }
            if (sysId > 0 && !string.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => c.SystemID==sysId && string.Format("{0} {1}", c.Name, c.Url).ToLower().Contains(queryString)).ToList();            
            }
            return myList.ToPagedList(pageIndex, pageSize);
        }
        public string GetFormattedBreadCrumb(WorkGroup WorkGroup, string separator = ">>")
        {
            if (WorkGroup == null)
                throw new ArgumentNullException("WorkGroup");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedWorkGroupIds = new List<int>() { };

            while (WorkGroup != null && !alreadyProcessedWorkGroupIds.Contains(WorkGroup.WorkGroupID)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = WorkGroup.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", WorkGroup.Name, separator, result);
                }

                alreadyProcessedWorkGroupIds.Add(WorkGroup.WorkGroupID);

                WorkGroup = _repository.Get(WorkGroup.ParentID);

            }
            return result;
        }

        /// <summary>
        /// Get formatted WorkGroup breadcrumb 
        /// Note: ACL and store mapping is ignored
        /// </summary>
        /// <param name="WorkGroup">WorkGroup</param>
        /// <param name="allCategories">All categories</param>
        /// <param name="separator">Separator</param>
        /// <returns>Formatted breadcrumb</returns>
        public string GetFormattedBreadCrumb(WorkGroup WorkGroup,
            List<WorkGroup> allCategories,
            string separator = ">>")
        {
            if (WorkGroup == null)
                throw new ArgumentNullException("WorkGroup");

            if (allCategories == null)
                throw new ArgumentNullException("WorkGroup");

            string result = string.Empty;

            //used to prevent circular references 
            var alreadyProcessedWorkGroupIds = new List<int>() { };

            while (WorkGroup != null && //not null 

                   !alreadyProcessedWorkGroupIds.Contains(WorkGroup.WorkGroupID)) //prevent circular references 
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = WorkGroup.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", WorkGroup.Name, separator, result);
                }

                alreadyProcessedWorkGroupIds.Add(WorkGroup.WorkGroupID);

                WorkGroup = (from c in allCategories
                             where c.WorkGroupID == WorkGroup.ParentID
                             select c).FirstOrDefault();
            }
            return result;
        }
    }
}




