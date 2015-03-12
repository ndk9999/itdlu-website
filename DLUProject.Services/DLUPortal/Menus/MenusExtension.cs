/* 
FileName: MenusExtension.cs
Project Name: DLUProject
Date Created: 14/11/2014 10:45:29 PM
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
using ColorLife.Core.Helper;
using ColorLife.Core.Mvc;
namespace DLUProject.Services
{
	public class MenusExtension
	{
		// YOUR CODE HERE
	        IRepository<Menus> _repository;
        public MenusExtension(IRepository<Menus> repo)
        {
            this._repository = repo;
        }
        public List<Menus> GetByParent(object id, bool isPublished)
        {
            if (isPublished)
                return _repository.Table.Where(c => c.IsPublished == true && c.ParentID == id.ToInt()).OrderBy(c => c.SortOrder).ThenBy(c => c.ParentID).ToList();
            return _repository.Table.Where(c => c.ParentID == id.ToInt()).OrderBy(c => c.SortOrder).ThenBy(c=>c.ParentID).ToList();
        }
        int level = -1;
        public List<Menus> GetByParent(List<Menus> list, int parentId, bool isPublished)
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

                Menus m = new Menus
                {
                    MenuID = d.MenuID,
                    Name = d.Name,
                    Alias = d.Alias,
                    Description = d.Description,
                    Image = d.Image,
                    ParentID = d.ParentID,
                    SortOrder = d.SortOrder,
                    IsPublished = d.IsPublished,
                    Route = d.Route,
                    Url =d.Url,
                    DisplayFlags=d.DisplayFlags,
                    Breadcrumb = GetFormattedBreadCrumb(d, ">>"),
                     
                };
                list.Add(m);
                GetByParent(list, m.MenuID, isPublished);
            }
            level--;
            return list;
        }
        public List<Menus> GetAllDropdownList(bool isPublished)
        {
            var myList = GetByParent(new List<Menus>(), 0, isPublished);
            myList.Insert(0, new Menus { MenuID = 0, Breadcrumb = "---Chọn danh mục---" });
            return myList;
        }
        public List<Menus> GetAll(string queryString, bool isPublished)
        {
            var myList = GetByParent(new List<Menus>(), 0, isPublished);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList;
        }
        public PagedList<Menus> GetAllMenus(int pageIndex, int pageSize,string DisplayFlag, string queryString, bool isPublished)
        {
            var display = DisplayFlagMenuEnum.MainMenu;
            if (!string.IsNullOrEmpty(DisplayFlag)) 
                display = ConvertType.ToEnum<DisplayFlagMenuEnum>(DisplayFlag);
            var myList = GetByParent(new List<Menus>(), 0, isPublished);
            if (!string.IsNullOrEmpty(DisplayFlag))
            {                
                myList = myList.Where(c => c.IsDisplayFlag(display)).ToList();
            }
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            if (!string.IsNullOrEmpty(DisplayFlag) && !String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => c.IsDisplayFlag(display) && string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            return myList.ToPagedList(pageIndex, pageSize);
        }
        public string GetFormattedBreadCrumb(Menus Menus, string separator = ">>")
        {
            if (Menus == null)
                throw new ArgumentNullException("Menus");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedMenusIds = new List<int>() { };

            while (Menus != null && !alreadyProcessedMenusIds.Contains(Menus.MenuID)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = Menus.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", Menus.Name, separator, result);
                }

                alreadyProcessedMenusIds.Add(Menus.MenuID);

                Menus = _repository.Get(Menus.ParentID);

            }
            return result;
        }

       
    }
}




