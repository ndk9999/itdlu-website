using System;
using System.Web;
namespace ColorLife.Core.HttpHandler
{
    public class DownloadHandler : IHttpHandler
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }


        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request.QueryString["file"];
            System.IO.FileInfo file = new System.IO.FileInfo(context.Server.MapPath(fileName));
            if (file.Exists)
            {
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                context.Response.AddHeader("Content-Length", file.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.WriteFile(file.FullName);
                context.Response.End();
            }
        }

        #endregion
    }
}