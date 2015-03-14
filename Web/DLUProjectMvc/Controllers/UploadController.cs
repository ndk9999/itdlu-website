using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DLUProjectMvc.Controllers
{
    /**   FileName: UploadController.cs 
          Project Name: DateTime Ajax
          Date Created: 12/17/2014 11:30:58 PM 
          Description:  File Upload trong ASP.NET MVC
          Version: 0.0.0.0 
          Author: Lê Thanh Tuấn - Khoa CNTT
          Author Email: tuanitpro@gmail.com 
          Author Mobile: 0976060432
          Author URI: http://tuanitpro.com 
          License: 
     */

    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("/Uploads/demo"),
                                               Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
                TempData["Msg"] = string.Format("Upload file {0} thành công", uploadFile.FileName);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadMulti(List<HttpPostedFileBase> uploadFile)
        {
            string abc = "";
            string def = "";
            foreach (var item in uploadFile)
            {

                string filePath = Path.Combine(HttpContext.Server.MapPath("/Uploads/demo"),
                                               Path.GetFileName(item.FileName));
                item.SaveAs(filePath);

                abc = string.Format("Upload {0} file thành công", uploadFile.Count);

                def += item.FileName + "; ";


            }
            TempData["Msg"] = abc + "</br>" + def;
            return RedirectToAction("Index");
        }
    }
}