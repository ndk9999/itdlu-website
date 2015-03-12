using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ColorLife.Core.FileManager
{
    public class FileUploadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            var output = "";
            string _uploadFolder = "";
            if (context.Request["folder"] != null)
            {
                _uploadFolder = context.Request["folder"];
            }
            else
            {
                _uploadFolder = "/Uploads/Images/"; //Setting.String("FILE_FILEFOLDER");
            }
            HttpPostedFile postedFile = context.Request.Files[0];
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

                Uri uriSiteRoot = new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority));
                output = string.Format("{0}{1}", "", path);
                context.Response.ContentType = "text/plain";
                context.Response.Write(output);
            }
            catch (ArgumentException ex)
            {
                output = ex.Message;
                context.Response.ContentType = "text/plain";
                context.Response.Write(output);
            }
            // TODO: do something with the uploaded file here

          
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}