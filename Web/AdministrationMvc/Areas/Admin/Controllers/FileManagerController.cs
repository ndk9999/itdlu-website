using ColorLife.Core.FileManager;
using DLUProjectFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DLUProjectMvc.Areas.Admin.Controllers
{

    public class FileManagerController : Controller
    {
        //
        // GET: /Admin/FileManager/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FullPage()
        {
            return View();
        }
        private string AllowedFileExtensions(FileFilterExt f)
        {
            string allowedExtensions = "";
            switch (f)
            {
                case FileFilterExt.All:
                    allowedExtensions = "";
                    break;
                case FileFilterExt.Image:
                    allowedExtensions = "^.jpg|.jpeg|.png|.PNG|.gif$";
                    break;
                case FileFilterExt.File:
                    allowedExtensions = "^.html|.sql|.php|.doc|.docx|.pdf|.ppt|.pptx|.xls|.xlsx|.txt$";
                    break;
                case FileFilterExt.Archive:
                    allowedExtensions = "^.rar|.zip|.7z$";
                    break;
                case FileFilterExt.Music:
                    allowedExtensions = "^.mp3|.wav$";
                    break;
                case FileFilterExt.Video:
                    allowedExtensions = "^.flv|.mp4$";
                    break;
                default:
                    allowedExtensions = "";
                    break;
            }
            return allowedExtensions;
        }
        [HttpPost]
        public JsonResult LoadAllFileFolder(string path, string searchTxt, string sortOrder, int filter, int filterExt)
        {
            string allowedExtensions = AllowedFileExtensions((FileFilterExt)filterExt);// "^.jpg|.jpeg|.png|.PNG|.rar|.zip|.doc|.docx|.pdf|.ppt|.pptx|.xls|.xlsx|.txt$";

            var query = FileManager.Instance.All(path, searchTxt, sortOrder, (FileFilter)filter, allowedExtensions);

            //if (query.Count() > 250)
            //    return query.ToList().GetRange(0, 250);
            //return query.ToList();

            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetTreeData(string id)
        {
            var rootNode = FileManager.Instance.GetJsTreeData(id);
            return Json(rootNode, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateNewFolder(string path, string text)
        {
            bool rs = FileManager.Instance.CreateNewFolder(path, text);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteFileFolder(string path)
        {
            bool rs = FileManager.Instance.DeleteFileFolder(path);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RenameFileFolder(string oldName, string newName)
        {
            bool rs = FileManager.Instance.RenameFileFolder(oldName, newName);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CopyFileFolder(string source, string target)
        {
            FileManager.Instance.CopyFileFolder(source, target);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MoveFileFolder(string source, string target)
        {
            FileManager.Instance.MoveFileFolder(source, target);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DemoUpload()
        {
            var fileService = new FileRepository();
            var model = fileService.All("/Uploads/test", FileFilter.All);
            return View(model);
        }

        [HttpPost]
        public ActionResult SimpleUpload(HttpPostedFileBase uploadFile)
        {
            FileUploadConfig config = new FileUploadConfig
            {
                VirtualSavePath = "/Uploads/test/",
                GenerateDateFolder = false,
                GenerateUniqueFileName = true,
                OverwriteExistingFile = true,
                FileContentMaxLenght = 1, // 1MB
                IsResizeImage = true,
                AllowedExtensions = "^.jpg|.jpeg|.png|.PNG|.rar|.zip$"
            };

            IFileUploadService uploadService = new FileUploadService(config);

            if (uploadFile.ContentLength > 0)
            {
                uploadService.Upload(uploadFile);
            }
            return RedirectToAction("DemoUpload");

        }
        public ActionResult MutilUpload(IEnumerable<HttpPostedFileBase> uploadFile)
        {

            FileUploadConfig config = new FileUploadConfig
            {
                VirtualSavePath = "/Uploads/test/",
                GenerateDateFolder = false,
                GenerateUniqueFileName = false,
                OverwriteExistingFile = true,
                FileContentMaxLenght = 1, // 1MB
                IsResizeImage = true,
                AllowedExtensions = "^.jpg|.jpeg|.png|.PNG|.rar|.zip$"
                //  AllowedExtensions = "^.jpg|.gif|.png|.jpeg|.rar|.zip|.swf$"
            };

            foreach (var file in uploadFile)
            {
                IFileUploadService uploadService = new FileUploadService(config);
                uploadService.Upload(file);
                // TODO: do something with the uploaded file here
            }

            return RedirectToAction("DemoUpload");
        }
        [HttpPost]
        public ActionResult SaveFileURL(string fileUrl)
        {
            string file_name = Server.MapPath("/uploads/test") + "\\logohehe.jpg";
            save_file_from_url(file_name, fileUrl);

            return RedirectToAction("DemoUpload");
        }
        [HttpPost]
        public JsonResult SaveFileToServer(string folderPath, string fileUrl)
        {
            string fileName = Server.MapPath(folderPath) + "\\" + Guid.NewGuid() + ".jpg";
            save_file_from_url(fileName, fileUrl);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public void save_file_from_url(string file_name, string url)
        {
            byte[] content; HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                content = br.ReadBytes(500000); br.Close();
            }
            response.Close();
            FileStream fs = new FileStream(file_name, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs); try
            {
                bw.Write(content);
            }
            finally { fs.Close(); bw.Close(); }
        }
        //- See more at: http://4rapiddev.com/csharp/asp-net-c-download-or-save-image-file-from-url/#sthash.ebsMWJ93.dpuf

        public ActionResult TinyMce()
        {
            return View();
        }
        public ActionResult JsTree()
        {
            return View();
        }
        public ActionResult FineUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AsyncUpload2(string folder)
        {
            string folderUpload = folder;
            // if (string.IsNullOrEmpty(folder))
            //  folderUpload = "/Uploads";

            HttpPostedFileBase postedFile = Request.Files[0];
            string errMess = "";
            string path = "";
            var config = new FileUploadConfig
            {
                VirtualSavePath = folderUpload,
                GenerateDateFolder = false,
                GenerateUniqueFileName = true,
                OverwriteExistingFile = true,
                FileContentMaxLenght = 10, // 1MB
                IsResizeImage = false,
                // AllowedExtensions = "^.jpg|.jpeg|.png|.PNG|.rar|.zip|.doc|.docx|.pdf|.rar|.ppt|.pptx|.txt$",
                AllowedExtensions = "^.jpg|.jpeg|.png|.PNG|.rar|.zip|.doc|.docx|.pdf|.ppt|.pptx|.xls|.xlsx|.txt$"
            };
            IFileUploadService uploadService = new FileUploadService(config);
            try
            {
                var item = uploadService.Upload2(postedFile, ref errMess, ref path);
                return Json(new
                {
                    success = true,
                    data = item
                },
             "text/plain");

            }
            catch (ArgumentException ex)
            {
                return Json(new
                {
                    success = false,
                    data = "",
                },
             "text/plain");
            }
        }
    }
}
