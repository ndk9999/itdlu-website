using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ColorLife.Core.FileManager
{
    public enum FileFilter
    {
        All = 0,
        Directories = 1,
        Files = 2
    }
    public enum FileFilterExt
    {
        All = 0,
        Image = 1,
        File = 2,
        Archive = 3,
        Music = 4,
        Video = 5
    }
    public class FileItem
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool IsFolder { get; set; }
        public bool IsExistsIcon { get; set; }
        public object Size { get; set; }
        public DateTime LastAccessDate { get; set; }
        public DateTime LastWriteDate { get; set; }
        public DateTime Date { get; set; }
        public string Attributes { get; set; }
        public string Extension { get; set; }
        public long FileCount { get; set; }
        public long SubFolderCount { get; set; }
        public string ContentType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string MD5Hash { get; set; }
        public string OutputUrl { get; set; }
        public bool IsImage
        {
            get
            {
                return this.Extension.IsExtensionImage();
            }
        }
        public string AllowedExtensions { get; set; }
        // string extensionAllowed = DalatGreen.Core.Entities.Settings.Setting("file.allowfileext");
        public bool IsExtensionAllowed(string fileExt)
        {
            bool tempResult = true;
            if (!object.ReferenceEquals(AllowedExtensions, string.Empty))
            {
                try
                {
                    tempResult = System.Text.RegularExpressions.Regex.IsMatch(fileExt, AllowedExtensions, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                }
                catch
                {
                    tempResult = false;
                }
            }
            return tempResult;
        }
        private bool IsExtensionAllowed()
        {
            bool tempResult = true;
            if (!object.ReferenceEquals(AllowedExtensions, string.Empty))
            {
                try
                {
                    tempResult = Regex.IsMatch(Extension, AllowedExtensions, RegexOptions.IgnoreCase);
                }
                catch
                {
                    tempResult = false;
                }
            }
            return tempResult;
        }

    }
}
