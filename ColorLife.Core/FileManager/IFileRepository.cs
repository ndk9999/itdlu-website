using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorLife.Core.FileManager
{
    public interface IFileRepository
    {
        JsTreeNode GetJsTreeData(string _path);

        // List<FileItem> AllFolder();        
        List<FileItem> All(string _path);
        List<FileItem> All(string _path, FileFilter _filter);        
        List<FileItem> All(string _path, string fileExtAllow);
        List<FileItem> All(string _path, FileFilter _filter, string fileExtAllow);
        List<FileItem> All(string _path, string _searchTxt, string _sortOrder, FileFilter _filter, string fileExtAllow);

        bool CreateNewFolder(string _folderName);
        bool CreateNewFolder(string _path, string _folderName);
        bool CreateNewFile(string _fileName);
        bool CreateNewFile(string _path, string _fileName);

        bool RenameFileFolder(string oldName, string newName);
        bool DeleteFileFolder(string _path);
        void CopyFileFolder(string _source, string _destination);
        void MoveFileFolder(string _source, string _destination);
         
    }
}
