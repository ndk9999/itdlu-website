/* 
FileName: StaffExtension.cs
Project Name: DLUProject
Date Created: 22/11/2014 9:07:03 PM
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
using DLUProject.Model;
using ColorLife.Core.Mvc;

namespace DLUProject.Services
{
    public class StaffExtension
    {
        IServices<Department> _departmentService;
        IServices<Staff> _staffService;
        IServices<StaffDepartment> _staffDepartment;
        DepartmentExtension _departmentExt;
        public StaffExtension(IServices<Department> departmentService, DepartmentExtension deptExt,
            IServices<Staff> staffService, IServices<StaffDepartment> staffDepartment)
        {
            this._staffService = staffService;
            this._staffDepartment = staffDepartment;
            this._departmentService = departmentService;
            this._departmentExt = deptExt;
        }

        // YOUR CODE HERE

        public List<Department> InDepartment(Staff c)
        {
            var query = from item in _departmentService.All()
                        join t in _staffDepartment.All().Where(x => x.StaffID == c.StaffID)
                        on item.DepartmentID equals t.DepartmentID
                        select new Department
                        {
                            DepartmentID = item.DepartmentID,
                            Name = item.Name,
                            Breadcrumb = _departmentExt.GetFormattedBreadCrumb(item)
                        };
            return query.ToList();
        }
        public int AddToDepartment(int staffId, int deptId)
        {
            var exists = _staffDepartment.Get(c => c.StaffID == staffId && c.DepartmentID == deptId);
            if (exists == null)
            {
                return _staffDepartment.Insert(new StaffDepartment { StaffID = staffId, DepartmentID = deptId });
            }
            return -1;
        }
        public int RemoveFromDepartment(int staffId, int deptId)
        {
            var a = _staffDepartment.Table.Where(c => c.StaffID == staffId && c.DepartmentID == deptId);
            int kq = _staffDepartment.Delete(a);
            return kq;
        }
        public int RemoveFromDepartment(int staffId)
        {
            var a = _staffDepartment.Table.Where(c => c.StaffID == staffId);
            int kq = _staffDepartment.Delete(a);
            return kq;
        }

        public List<StaffDepartmentModel> GetAllStaffDepartment()
        {
            // var DistinctItems = _staffDepartment.All().GroupBy(x => x.StaffID).Select(y => y.First());

            //var DistinctItems2 = _staffDepartment.All().GroupBy(x => x.DepartmentID).Select(y => y.First());

            //var dept = from item in _departmentService.All()
            //           join t in DistinctItems2
            //           on item.DepartmentID equals t.DepartmentID
            //           select item;


            var query = from item in _staffService.All()

                        select new StaffDepartmentModel
                        {
                            Staff = item,
                            // Departments = dept.ToList(),
                            StaffDepartments = _staffDepartment.All().Where(c => c.StaffID.Equals(item.StaffID)).ToList()
                        };
            return query.ToList();
        }
        public List<StaffDepartmentModel> GetAllStaffByDepartment(int deptId)
        {
            var DistinctItems = _staffDepartment.All().Where(m => m.DepartmentID.Equals(deptId)).GroupBy(x => x.StaffID).Select(y => y.First());
            var query = from item in _staffService.All()
                        join t in DistinctItems
                        on item.StaffID equals t.StaffID

                        select new StaffDepartmentModel
                        {
                            Staff = item,
                            StaffDepartments = _staffDepartment.All().Where(c => c.StaffID.Equals(item.StaffID)).ToList()
                        };            
            return query.ToList();
        }
        public PagedList<StaffDepartmentModel> GetAllStaffs(int pageIndex, int pageSize, int deptId, string queryString)
        {
            var myList = GetAllStaffDepartment();
            if (deptId > 0)
            {
                var DistinctItems = _staffDepartment.All().Where(m=>m.DepartmentID.Equals(deptId)).GroupBy(x => x.StaffID).Select(y => y.First());
                var query = from item in _staffService.All()
                            join t in DistinctItems
                            on item.StaffID equals t.StaffID
                            
                            select new StaffDepartmentModel
                            {
                                Staff = item,
                                StaffDepartments = _staffDepartment.All().Where(c=>c.StaffID.Equals(item.StaffID)).ToList()
                            };
                myList = query.ToList();
            }

            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                var query = from item in _staffService.All().Where(c => string.Format("{0} {1} {2} {3} {4}", c.StaffNoID, c.FirstName, c.LastName, c.Email, c.Position).ToLower().Contains(queryString))
                            
                            select new StaffDepartmentModel
                            {
                                Staff = item,
                                StaffDepartments = _staffDepartment.All().Where(c => c.StaffID.Equals(item.StaffID)).ToList()
                            };
                myList = query.ToList();
            }

            if (deptId > 0 && !String.IsNullOrEmpty(queryString))
            {             
                queryString = queryString.ToLower();
                var DistinctItems = _staffDepartment.All().Where(m => m.DepartmentID.Equals(deptId)).GroupBy(x => x.StaffID).Select(y => y.First());
                var query = from item in _staffService.All().Where(c => string.Format("{0} {1} {2} {3} {4}", c.StaffNoID, c.FirstName, c.LastName, c.Email, c.Position).ToLower().Contains(queryString))
                            join t in DistinctItems

                            on item.StaffID equals t.StaffID                         
                            select new StaffDepartmentModel
                            {
                                Staff = item,
                                StaffDepartments = _staffDepartment.All().Where(c => c.StaffID.Equals(item.StaffID)).ToList()
                            };
                myList = query.ToList();
            }
            return myList.ToPagedList(pageIndex, pageSize);
        }
    }
}




