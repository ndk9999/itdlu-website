using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Security.Permissions;
using System.Security;
using System.Text.RegularExpressions;
using System.Drawing;
namespace ColorLife.Core.FileManager
{
    public class FileUploadService : IFileUploadService
    {
        FileUploadConfig _fileUploadConfig;
        public FileUploadService() { }
        public FileUploadService(FileUploadConfig fileUploadConfig)
        {
            _fileUploadConfig = fileUploadConfig;
        }

        public void Upload(HttpPostedFile httpPostedFile)
        {
            string fileName = "";
            double contentLenght = (double)(httpPostedFile.ContentLength / 1024) / 1024;
            if (httpPostedFile.ContentLength > 0)
            {
                if (_fileUploadConfig.GenerateUniqueFileName)
                {
                    fileName = FileHelper.GenerateUniqueFileName;
                }
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);
                }

                if (_fileUploadConfig.GenerateDateFolder)
                {
                    // 2013/10
                    _fileUploadConfig.VirtualSavePath += DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0');

                    // 102013
                    // _fileUploadConfig.VirtualSavePath += DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
                }
                string fileExtension = System.IO.Path.GetExtension(httpPostedFile.FileName);

                string physicalFolderAndFileName = Path.Combine(HttpContext.Current.Server.MapPath(_fileUploadConfig.VirtualSavePath), fileName);
                physicalFolderAndFileName += fileExtension;


                if (_fileUploadConfig.VirtualSavePath == string.Empty)
                {
                    throw new ArgumentException("Cannot save the file without a " + "VirtualSavePath.", "VirtualSavePath");
                }
                if (_fileUploadConfig.OverwriteExistingFile == false)
                {
                    if (physicalFolderAndFileName.IsFileExists())
                    {
                        throw new ArgumentException("The file " + fileName + " already exists in the VirtualSavePath folder. Choose a different name or set OverwriteExistingFile to True.", "FileName");
                    }
                }
                if (!fileExtension.IsExtensionAllowed(_fileUploadConfig.AllowedExtensions))
                {
                    throw new ArgumentException("The file " + fileName + " has an invalid extension. Only the extension(s) " + _fileUploadConfig.AllowedExtensions + " are allowed.", "Extension");
                }
                if (contentLenght > _fileUploadConfig.FileContentMaxLenght)
                {
                    throw new ArgumentException("File size must  small than < " + _fileUploadConfig.FileContentMaxLenght + "MB", "contentLenght");
                }
                try
                {
                    string myFolder = Path.GetDirectoryName(physicalFolderAndFileName);
                    if (!myFolder.IsFolderExists())
                    {
                        Directory.CreateDirectory(myFolder);
                    }
                    httpPostedFile.SaveAs(physicalFolderAndFileName);
                    if (fileExtension.IsExtensionImage())
                    {
                        if (_fileUploadConfig.IsResizeImage)
                        {
                            Imaging.ResizeImage(physicalFolderAndFileName, _fileUploadConfig.ImageWidth, _fileUploadConfig.ImageHeight);
                        }
                    }
                }
                catch (UnauthorizedAccessException uaex)
                {
                    throw new ArgumentException(uaex.Message, "FilePermission");
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("File Content Lenght must > 0 & < " + _fileUploadConfig.FileContentMaxLenght, "contentLenght");
            }
        }
        public void Upload(HttpPostedFile httpPostedFile, ref string exceptionMessage, ref string savedFilePath)
        {
            string fileName = "";
            double contentLenght = (double)(httpPostedFile.ContentLength / 1024) / 1024;
            if (httpPostedFile.ContentLength > 0)
            {
                if (_fileUploadConfig.GenerateUniqueFileName)
                {
                    fileName = FileHelper.GenerateUniqueFileName;
                }
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);
                }

                if (_fileUploadConfig.GenerateDateFolder)
                {
                    // 2013/10
                    _fileUploadConfig.VirtualSavePath += DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0');

                    // 102013
                    // _fileUploadConfig.VirtualSavePath += DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
                }
                string fileExtension = System.IO.Path.GetExtension(httpPostedFile.FileName);

                string physicalFolderAndFileName = Path.Combine(HttpContext.Current.Server.MapPath(_fileUploadConfig.VirtualSavePath), fileName);
                physicalFolderAndFileName += fileExtension;


                if (_fileUploadConfig.VirtualSavePath == string.Empty)
                {
                    throw new ArgumentException("Cannot save the file without a " + "VirtualSavePath.", "VirtualSavePath");
                }
                if (_fileUploadConfig.OverwriteExistingFile == false)
                {
                    if (physicalFolderAndFileName.IsFileExists())
                    {
                        throw new ArgumentException("The file " + fileName + " already exists in the VirtualSavePath folder. Choose a different name or set OverwriteExistingFile to True.", "FileName");
                    }
                }
                if (!fileExtension.IsExtensionAllowed(_fileUploadConfig.AllowedExtensions))
                {
                    throw new ArgumentException("The file " + fileName + " has an invalid extension. Only the extension(s) " + _fileUploadConfig.AllowedExtensions + " are allowed.", "Extension");
                }
                if (contentLenght > _fileUploadConfig.FileContentMaxLenght)
                {
                    throw new ArgumentException("File size must  small than < " + _fileUploadConfig.FileContentMaxLenght + "MB", "contentLenght");
                }
                try
                {
                    string myFolder = Path.GetDirectoryName(physicalFolderAndFileName);
                    if (!myFolder.IsFolderExists())
                    {
                        Directory.CreateDirectory(myFolder);
                    }
                    httpPostedFile.SaveAs(physicalFolderAndFileName);
                    if (fileExtension.IsExtensionImage())
                    {
                        if (_fileUploadConfig.IsResizeImage)
                        {
                            Imaging.ResizeImage(physicalFolderAndFileName, _fileUploadConfig.ImageWidth, _fileUploadConfig.ImageHeight);
                        }
                    }
                    exceptionMessage = "Upload file successful.";
                    savedFilePath = Path.Combine(_fileUploadConfig.VirtualSavePath, fileName) + fileExtension;
                    //savedFilePath = Path.Combine(physicalFolderAndFileName);
                }
                catch (UnauthorizedAccessException uaex)
                {
                    throw new ArgumentException(uaex.Message, "FilePermission");
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("File Content Lenght must > 0 & < " + _fileUploadConfig.FileContentMaxLenght, "contentLenght");
            }
        }
        public void Upload(HttpPostedFileBase httpPostedFileBase)
        {
            string fileName = "";
            double contentLenght = (double)(httpPostedFileBase.ContentLength / 1024) / 1024;
            if (httpPostedFileBase.ContentLength > 0)
            {
                if (_fileUploadConfig.GenerateUniqueFileName)
                {
                    fileName = FileHelper.GenerateUniqueFileName;
                }
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(httpPostedFileBase.FileName);
                }

                if (_fileUploadConfig.GenerateDateFolder)
                {
                    // 2013/10
                    _fileUploadConfig.VirtualSavePath += DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0');

                    // 102013
                    // _fileUploadConfig.VirtualSavePath += DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
                }
                string fileExtension = System.IO.Path.GetExtension(httpPostedFileBase.FileName);

                string physicalFolderAndFileName = Path.Combine(HttpContext.Current.Server.MapPath(_fileUploadConfig.VirtualSavePath), fileName);
                physicalFolderAndFileName += fileExtension;


                if (_fileUploadConfig.VirtualSavePath == string.Empty)
                {
                    throw new ArgumentException("Cannot save the file without a " + "VirtualSavePath.", "VirtualSavePath");
                }
                if (_fileUploadConfig.OverwriteExistingFile == false)
                {
                    if (physicalFolderAndFileName.IsFileExists())
                    {
                        throw new ArgumentException("The file " + fileName + " already exists in the VirtualSavePath folder. Choose a different name or set OverwriteExistingFile to True.", "FileName");
                    }
                }
                if (!fileExtension.IsExtensionAllowed(_fileUploadConfig.AllowedExtensions))
                {
                    throw new ArgumentException("The file " + fileName + " has an invalid extension. Only the extension(s) " + _fileUploadConfig.AllowedExtensions + " are allowed.", "Extension");
                }
                if (contentLenght > _fileUploadConfig.FileContentMaxLenght)
                {
                    throw new ArgumentException("File size must  small than < " + _fileUploadConfig.FileContentMaxLenght + "MB", "contentLenght");
                }
                try
                {
                    string myFolder = Path.GetDirectoryName(physicalFolderAndFileName);
                    if (!myFolder.IsFolderExists())
                    {
                        Directory.CreateDirectory(myFolder);
                    }
                    httpPostedFileBase.SaveAs(physicalFolderAndFileName);
                    if (fileExtension.IsExtensionImage())
                    {
                        if (_fileUploadConfig.IsResizeImage)
                        {
                            Imaging.ResizeImage(physicalFolderAndFileName, _fileUploadConfig.ImageWidth, _fileUploadConfig.ImageHeight);
                        }
                    }
                }
                catch (UnauthorizedAccessException uaex)
                {
                    throw new ArgumentException(uaex.Message, "FilePermission");
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("File Content Lenght must > 0 & < " + _fileUploadConfig.FileContentMaxLenght, "contentLenght");
            }
        }




        public void Upload(HttpPostedFileBase httpPostedFileBase, ref string exceptionMessage, ref string savedFilePath)
        {
            string fileName = "";
            double contentLenght = (double)(httpPostedFileBase.ContentLength / 1024) / 1024;
            if (httpPostedFileBase.ContentLength > 0)
            {
                if (_fileUploadConfig.GenerateUniqueFileName)
                {
                    fileName = FileHelper.GenerateUniqueFileName;
                }
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(httpPostedFileBase.FileName);
                }

                if (_fileUploadConfig.GenerateDateFolder)
                {
                    // 2013/10
                    _fileUploadConfig.VirtualSavePath += DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0');

                    // 102013
                    // _fileUploadConfig.VirtualSavePath += DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
                }
                string fileExtension = System.IO.Path.GetExtension(httpPostedFileBase.FileName);

                string physicalFolderAndFileName = Path.Combine(HttpContext.Current.Server.MapPath(_fileUploadConfig.VirtualSavePath), fileName);
                physicalFolderAndFileName += fileExtension;


                if (_fileUploadConfig.VirtualSavePath == string.Empty)
                {
                    throw new ArgumentException("Cannot save the file without a " + "VirtualSavePath.", "VirtualSavePath");
                }
                if (_fileUploadConfig.OverwriteExistingFile == false)
                {
                    if (physicalFolderAndFileName.IsFileExists())
                    {
                        throw new ArgumentException("The file " + fileName + " already exists in the VirtualSavePath folder. Choose a different name or set OverwriteExistingFile to True.", "FileName");
                    }
                }
                if (!fileExtension.IsExtensionAllowed(_fileUploadConfig.AllowedExtensions))
                {
                    throw new ArgumentException("The file " + fileName + " has an invalid extension. Only the extension(s) " + _fileUploadConfig.AllowedExtensions + " are allowed.", "Extension");
                }
                if (contentLenght > _fileUploadConfig.FileContentMaxLenght)
                {
                    throw new ArgumentException("File size must  small than < " + _fileUploadConfig.FileContentMaxLenght + "MB", "contentLenght");
                }
                try
                {
                    string myFolder = Path.GetDirectoryName(physicalFolderAndFileName);
                    if (!myFolder.IsFolderExists())
                    {
                        Directory.CreateDirectory(myFolder);
                    }
                    httpPostedFileBase.SaveAs(physicalFolderAndFileName);
                    if (fileExtension.IsExtensionImage())
                    {
                        if (_fileUploadConfig.IsResizeImage)
                        {
                            Imaging.ResizeImage(physicalFolderAndFileName, _fileUploadConfig.ImageWidth, _fileUploadConfig.ImageHeight);
                        }
                    }
                    exceptionMessage = "Upload file successful.";
                    savedFilePath = Path.Combine(_fileUploadConfig.VirtualSavePath, fileName) + fileExtension;
                }
                catch (UnauthorizedAccessException uaex)
                {
                    throw new ArgumentException(uaex.Message, "FilePermission");
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("File Content Lenght must > 0 & < " + _fileUploadConfig.FileContentMaxLenght, "contentLenght");
            }
        }


        public FileItem Upload2(HttpPostedFileBase httpPostedFileBase, ref string exceptionMessage, ref string savedFilePath)
        {
            string fileName = "";
            string fileExtension = "";
            string md5 = "";

            Size originalSize = new Size(0, 0);
            double contentLenght = (double)(httpPostedFileBase.ContentLength / 1024) / 1024;
            long fileSize = (long)httpPostedFileBase.ContentLength;
            string contentType = httpPostedFileBase.ContentType;
            if (httpPostedFileBase.ContentLength > 0)
            {
                if (_fileUploadConfig.GenerateUniqueFileName)
                {
                    fileName = FileHelper.GenerateUniqueFileName;
                }
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(httpPostedFileBase.FileName);
                }
                if (_fileUploadConfig.GenerateDateFolder)
                {
                    // 2013/10
                    _fileUploadConfig.VirtualSavePath += DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0');

                    // 102013
                    // _fileUploadConfig.VirtualSavePath += DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
                }
                fileExtension = System.IO.Path.GetExtension(httpPostedFileBase.FileName);

                string physicalFolderAndFileName = Path.Combine(HttpContext.Current.Server.MapPath(_fileUploadConfig.VirtualSavePath), fileName);
                physicalFolderAndFileName += fileExtension;


                if (_fileUploadConfig.VirtualSavePath == string.Empty)
                {
                    throw new ArgumentException("Cannot save the file without a " + "VirtualSavePath.", "VirtualSavePath");
                }
                if (_fileUploadConfig.OverwriteExistingFile == false)
                {
                    if (physicalFolderAndFileName.IsFileExists())
                    {
                        throw new ArgumentException("The file " + fileName + " already exists in the VirtualSavePath folder. Choose a different name or set OverwriteExistingFile to True.", "FileName");
                    }
                }
                if (!fileExtension.IsExtensionAllowed(_fileUploadConfig.AllowedExtensions))
                {
                    throw new ArgumentException("The file " + fileName + " has an invalid extension. Only the extension(s) " + _fileUploadConfig.AllowedExtensions + " are allowed.", "Extension");
                }
                if (contentLenght > _fileUploadConfig.FileContentMaxLenght)
                {
                    throw new ArgumentException("File size must  small than < " + _fileUploadConfig.FileContentMaxLenght + "MB", "contentLenght");
                }
                try
                {
                    string myFolder = Path.GetDirectoryName(physicalFolderAndFileName);
                    if (!myFolder.IsFolderExists())
                    {
                        Directory.CreateDirectory(myFolder);
                    }
                    httpPostedFileBase.SaveAs(physicalFolderAndFileName);
                    savedFilePath = Path.Combine(_fileUploadConfig.VirtualSavePath, fileName) + fileExtension;
                    string fullPath = HttpContext.Current.Server.MapPath(savedFilePath);
                    md5 = FileHelper.GetMD5HashFromFile(fullPath);
                    if (fileExtension.IsExtensionImage())
                    {

                        Bitmap bmpSource = null;
                        bmpSource = new Bitmap(fullPath);
                        originalSize = bmpSource.Size;


                        if (_fileUploadConfig.IsResizeImage)
                        {
                            Imaging.ResizeImage(physicalFolderAndFileName, _fileUploadConfig.ImageWidth, _fileUploadConfig.ImageHeight);
                            originalSize = new Size(_fileUploadConfig.ImageWidth, _fileUploadConfig.ImageHeight);
                        }
                    }
                    exceptionMessage = "Upload file successful.";

                }
                catch (UnauthorizedAccessException uaex)
                {
                    throw new ArgumentException(uaex.Message, "FilePermission");
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("File Content Lenght must > 0 & < " + _fileUploadConfig.FileContentMaxLenght, "contentLenght");
            }
            var fileItem = new FileItem
            {
                Name = fileName + fileExtension,
                FullName = savedFilePath,
                OutputUrl = savedFilePath,
                Size = FileHelper.FormatFileSize(fileSize),
                Extension = fileExtension.Replace(".", string.Empty),
                Width = originalSize.Width,
                Height = originalSize.Height,
                ContentType = contentType,
                MD5Hash = md5,
                
            };
            return fileItem;
        }
    }
}
