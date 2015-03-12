using System;
using System.IO;
using System.Web;
namespace ColorLife.Core.HttpHandler
{
    public class DeleteHandler : IHttpHandler
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }
        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request.QueryString["file"];
            string path = context.Server.MapPath(fileName);

            if (Directory.Exists(path))
            {
                var directory = new DirectoryInfo(path);
                foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
                foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
           // context.Response.ContentType = "text/plain";
          //  context.Response.Write("Xóa thành công");
        }
        #endregion
    }
}