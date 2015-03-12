using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ColorLife.Core.FileManager
{
    public static class FileHelper
    {
        public static string RemoveDotFileName(this string fileName)
        {
            string fileNameX = "";
            if (fileName.LastIndexOf(".") > 0)
            {
                fileNameX = fileName.Substring(0, fileName.LastIndexOf("."));
            }
            return fileNameX;
        }
        public static string GenerateUniqueFileName
        {
            get
            {
            //    return DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss") + "-colorlife";// + Guid.NewGuid().ToString();
                var ouput = DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss") + "-" + Guid.NewGuid();
                return ouput.Replace("-", string.Empty);
            }
        }
        /// <summary>
        /// Check file extention to Upload
        /// </summary>
        /// <param name="fileExtension">File.txt</param>
        /// <param name="allowedExtensions">^.txt|.jpg|.gif|.png|.jpeg|.rar|.zip|.swf$</param>
        /// <returns></returns>
        public static bool IsExtensionAllowed(this string fileExtension, string allowedExtensions)
        {
            bool tempResult = true;
            if (!object.ReferenceEquals(fileExtension, string.Empty))
            {
                try
                {
                    tempResult = Regex.IsMatch(fileExtension, allowedExtensions, RegexOptions.IgnoreCase);
                }
                catch
                {
                    tempResult = false;
                }
            }
            return tempResult;
        }
        /// <summary>
        /// Check Image extention
        /// </summary>
        /// <param name="fileExtension">Image.jpg</param>        
        /// <returns></returns>
        public static bool IsExtensionImage(this string fileExtension)
        {
            string allowedImageExt = "^.jpg|.jpeg|.png$";
            return IsExtensionAllowed(fileExtension, allowedImageExt);
        }
        public static bool IsFolderExists(this string myFolderName)
        {
            return System.IO.Directory.Exists(myFolderName);
        }
        public static bool IsFileExists(this string myFileName)
        {
            
            return System.IO.File.Exists(myFileName);
        }
        public static string FormatFileSize(this long size)
        {
            double s = Convert.ToDouble(size);
            string[] format = new string[] { "{0} Bytes", "{0} Kb", "{0} Mb", "{0} Gb", "{0} Tb", "{0} Pb", "{0} Eb" };
            int i = 0;
            while (i < format.Length && s >= 1024)
            {
                s = (int)(100 * s / 1024) / 100.0;
                i++;
            }
            return string.Format(format[i], s);
        }
        /// <summary>
        /// Save file from Url to Storage Host
        /// </summary>
        /// <param name="file_name">File name: C:\image.jpg</param>
        /// <param name="url">http://example.com/image.jpg</param>
        public static void SaveFileFromUrl(string file_name, string url)
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

        public static string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
    }
}