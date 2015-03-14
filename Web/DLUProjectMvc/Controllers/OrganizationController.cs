using System.Web.Mvc;
using DLUProject.Services;
using DLUProject.Domain;
using DLUProject.Model;
namespace DLUProjectMvc.Controllers
{
    public class OrganizationController : Controller
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
        // GET: Organization
        public ActionResult Index()
        {
            var model = _departmentService.All();//.Where(c => c.ParentID == 0);
            return View(model);
        }
        public ActionResult Single(int id)
        {
            var dept = _departmentService.Get(id);
            if (dept == null)
            {
                return RedirectToAction("Index");
            }
            var model = new SingleOrganizationModel
            {
                Item = dept,
               
                ListStaff = _staffExt.GetAllStaffByDepartment(id)
            };
            ViewBag.Title = dept.Name;
            return View(model);
        }
        public ActionResult SinglePopup(int id)
        {
            var dept = _departmentService.Get(id);
            if (dept == null)
            {
                return RedirectToAction("Index");
            }
            var model = new SingleOrganizationModel
            {
                Item = dept,
               
                ListStaff = _staffExt.GetAllStaffByDepartment(id)
            };
            ViewBag.Title = dept.Name;
            return View(model);
        }
    }
}