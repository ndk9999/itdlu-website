using ColorLife.Core.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DLUProjectMvc.Areas.Admin.Controllers
{
    public class FileUploaderController : Controller
    {
        [HttpPost]
        public ActionResult AsyncUpload(string folder)
        {
            // string _uploadFolder = "";
            //if (Request["folder"] != null)
            //{
            //    _uploadFolder = Request["folder"];
            //}
            //else
            //{
            //    _uploadFolder = "/Uploads/Images/"; //Setting.String("FILE_FILEFOLDER");
            //}

            HttpPostedFileBase postedFile = Request.Files[0];
            string errMess = "";
            string path = "";
            var config = new FileUploadConfig
            {
                VirtualSavePath = folder,
                GenerateDateFolder = false,
                GenerateUniqueFileName = true,
                OverwriteExistingFile = true,
                FileContentMaxLenght = 10, // 1MB
                IsResizeImage = true,
                AllowedExtensions = "^.jpg|.jpeg|.png|.PNG|.rar|.zip|.doc|.docx|.pdf|.rar|.ppt|.pptx|.xls|.xlsx$"
            };
            IFileUploadService uploadService = new FileUploadService(config);
            try
            {
                uploadService.Upload(postedFile, ref errMess, ref path);

                return Json(new
                {
                    success = true,

                    imageUrl = path
                },
               "text/plain");
            }
            catch (ArgumentException ex)
            {
                return Json(new
                {
                    success = false,
                    imageUrl = ""
                },
             "text/plain");
            }
        }


        [HttpPost]
        public ActionResult AsyncUpload2(string folder)
        {
            string folderUpload = MyFileFolderHelper.GetFolderFileSystem;
            if (string.IsNullOrEmpty(folder))
                folderUpload = MyFileFolderHelper.GetFolderFileSystem;
            else folderUpload = MyFileFolderHelper.GetFolderFileSystem + folder;

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
                IsResizeImage = false ,
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
        [HttpPost]
        public ActionResult SimpleUpload()
        {
            string _uploadFolder = "";
            if (Request["folder"] != null)
            {
                _uploadFolder = Request["folder"];
            }
            else
            {
                _uploadFolder = "/Uploads/Images/"; //Setting.String("FILE_FILEFOLDER");
            }

            HttpPostedFileBase postedFile = Request.Files[0];
            string errMess = "";
            string path = "";
            var config = new FileUploadConfig
            {
                VirtualSavePath = _uploadFolder,
                GenerateDateFolder = true,
                GenerateUniqueFileName = false,
                OverwriteExistingFile = true,
                FileContentMaxLenght = 10, // 1MB
                IsResizeImage = true,
                AllowedExtensions = "^.jpg|.jpeg|.png|.PNG|.rar|.zip$"
            };

            IFileUploadService uploadService = new FileUploadService(config);
            try
            {
                uploadService.Upload(postedFile, ref errMess, ref path);
                return Content(path, "text/plain");

            }
            catch (ArgumentException ex)
            {
                return Content(ex.Message, "text/plain");
            }
        }
        [HttpPost]
        public ActionResult DeleteFile(string file)
        {
            IFileRepository fileRepository = new FileRepository();
            fileRepository.DeleteFileFolder(file);
            return Json(new { success = true, data = "Xóa file " + file + " thành công" }, JsonRequestBehavior.AllowGet);
        }
    }
}