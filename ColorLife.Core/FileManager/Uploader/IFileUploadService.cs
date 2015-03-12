using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ColorLife.Core.FileManager
{
    public interface IFileUploadService
    {
        void Upload(HttpPostedFile httpPostedFile);
        void Upload(HttpPostedFile httpPostedFile, ref string exceptionMessage, ref string savedFilePath);
        void Upload(HttpPostedFileBase httpPostedFileBase);
        void Upload(HttpPostedFileBase httpPostedFileBase, ref string exceptionMessage, ref string savedFilePath);

        FileItem Upload2(HttpPostedFileBase httpPostedFileBase, ref string exceptionMessage, ref string savedFilePath);

    }
}
