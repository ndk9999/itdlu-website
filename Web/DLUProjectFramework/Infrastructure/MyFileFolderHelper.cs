using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Web;
public class MyFileFolderHelper
{
    public static string GetFolderFileSystem
    {
        get
        {
            string path = ConfigurationManager.AppSettings["FileSystems"];
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/FileSystems"));
                path = "/FileSystems/";
            }
            if (path.EndsWith("/") == false) path = path + "/";
            return path;
        }
    }
    public static string GetFolderUploadImage
    {
        get
        {
            return "";
        }
    }
}
 
