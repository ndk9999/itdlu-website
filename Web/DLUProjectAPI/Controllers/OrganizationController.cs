using ColorLife.Core.Helper;
using DLUProject.Domain;
using DLUProject.Model;
using DLUProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DLUProjectAPI.Controllers
{
    public class OrganizationController : ApiController
    {
        IServices<Department> _departmentService;
        IServices<Staff> _staffService;
        StaffExtension _staffExt;
        public OrganizationController(IServices<Department> departmentService, IServices<Staff> staffService, StaffExtension staffExt)
        {
            this._departmentService = departmentService;
            this._staffService = staffService;
            this._staffExt = staffExt;
        }
        [HttpGet]
        public JsonResponse<SingleOrganizationModel> Single(int id)
        {
            var dept = _departmentService.Get(id);
            var model = new SingleOrganizationModel
            {
                Item = dept,
                ListStaff = _staffExt.GetAllStaffByDepartment(id)
            };
            return new JsonResponse<SingleOrganizationModel> { Success = dept != null, Data = model };
        }
    }
}
