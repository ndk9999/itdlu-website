using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Web;

namespace ColorLife.Core.FileManager
{
    public class FileRepository : IFileRepository
    {
        #region Helper
        string AppPath
        {
            get
            {
                string _p = ConfigurationManager.AppSettings["AppPath"];
                if (string.IsNullOrEmpty(_p))
                    _p = HttpContext.Current.Request.ApplicationPath;
                return _p.TrimEnd('/').TrimEnd('\\');
            }
        }
        bool IsRemoveAppPath
        {
            get
            {
                bool _b;
                bool.TryParse(ConfigurationManager.AppSettings["RemoveAppPath"], out _b);
                return _b;
            }
        }
        public string RelativePath(string _fullPath)
        {
            string _appFullPath = !String.IsNullOrEmpty(AppPath) ? HttpContext.Current.Server.MapPath(AppPath) : HttpContext.Current.Server.MapPath("/");
            string _path = _fullPath.Replace(_appFullPath, !String.IsNullOrEmpty(AppPath) ? "" : "/").Replace("\\", "/");
            return IsRemoveAppPath ? _path : AppPath + _path;
        }

        string IconPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("/icons/");
            }
        }
        bool IsExistsIcon(string _extension)
        {
            return File.Exists(IconPath + _extension.Replace(".", "") + ".gif");
        }
        FileItem CreateDirectoryItem(DirectoryInfo _dir)
        {
            var _item = new FileItem
            {
                Name = _dir.Name,
                FullName = RelativePath(_dir.FullName),
                Date = _dir.CreationTimeUtc,
                FileCount = _dir.GetFiles().LongLength,
                SubFolderCount = _dir.GetDirectories().LongLength,
                IsFolder = true,
                IsExistsIcon = true,
                LastWriteDate = _dir.LastWriteTime,
                LastAccessDate = _dir.LastAccessTime,

            };
            return _item;
        }
        FileItem CreateFileItem(FileInfo _file)
        {
            var _item = new FileItem
            {
                Name = _file.Name,
                FullName = RelativePath(_file.FullName),
                Date = _file.CreationTimeUtc,
                LastAccessDate = _file.LastAccessTimeUtc,
                LastWriteDate = _file.LastWriteTime,
                IsFolder = false,
                IsExistsIcon = true,
                Size = FileHelper.FormatFileSize(_file.Length),
                Extension = _file.Extension,
                Attributes = _file.Attributes.ToString()
            };
            return _item;
        }
        /// <summary>
        /// A method to populate a TreeView with directories, subdirectories, etc
        /// </summary>
        /// <param name="dir">The path of the directory</param>
        /// <param name="node">The "master" node, to populate</param>
        void PopulateTree(string dir, JsTreeNode node)
        {
            if (node.children == null)
            {
                node.children = new List<JsTreeNode>();
            }
            var list = All(dir, FileFilter.Directories);
            // loop through each subdirectory
            foreach (var d in list)
            {
                // create a new node
                var t = new JsTreeNode();
                t.a_attr = new JsTreeNodeAAttributes();
                t.id = d.FullName;
                t.text = d.Name.ToString();
                // populate the new node recursively
                PopulateTree(d.FullName, t);
                node.children.Add(t); // add the node to the "master" node                
            }

            //// get the information of the directory
            //DirectoryInfo directory = new DirectoryInfo(dir);
            //// loop through each subdirectory
            //foreach (DirectoryInfo d in directory.GetDirectories())
            //{
            //    // create a new node
            //    JsTreeModel t = new JsTreeModel();
            //    t.attr = new JsTreeAttribute();
            //    t.attr.id = d.FullName;
            //    t.data = d.Name.ToString();
            //    // populate the new node recursively
            //    PopulateTree(d.FullName, t);
            //    node.children.Add(t); // add the node to the "master" node
            //}
            // lastly, loop through each file in the directory, and add these as nodes
            //foreach (FileInfo f in directory.GetFiles())
            //{
            //    // create a new node
            //    JsTreeModel t = new JsTreeModel();
            //    t.attr = new JsTreeAttribute();
            //    t.attr.id = f.FullName;
            //    t.data = f.Name.ToString();
            //    // add it to the "master"
            //   // node.children.Add(t);
            //}
        }
        #endregion

        public JsTreeNode GetJsTreeData(string _path)
        {
            var rootNode = new JsTreeNode();
            rootNode.a_attr = new JsTreeNodeAAttributes { href = _path };
            rootNode.text = "My Documents";
            // string rootPath = Request.MapPath("/Uploads");
            string rootPath = _path;
            rootNode.id = rootPath;
            PopulateTree(rootPath, rootNode);
            // AlreadyPopulated = true;
            return rootNode;
        }
        public List<FileItem> All(string _path)
        {
            return All(_path, null, null, FileFilter.All, null);
        }
        public List<FileItem> All(string _path, FileFilter _filter)
        {
            return All(_path, null, null, _filter, "");
        }
        public List<FileItem> All(string _path, FileFilter _filter, string fileExtAllow)
        {
            return All(_path, null, null, _filter, fileExtAllow);
        }

        public List<FileItem> All(string _path, string fileExtAllow)
        {
            return All(_path, null, null, FileFilter.All, fileExtAllow);
        }
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
       

        public List<FileItem> All(string _path, string _searchTxt, string _sortOrder, FileFilter _filter, string fileExtAllow)
        {
            _path = HttpContext.Current.Server.MapPath(_path);

            string[] arraySort = { };
            if (!string.IsNullOrEmpty(_sortOrder))
            {
                arraySort = _sortOrder.Split(' ');
            }
            var _list = new List<FileItem>();
            var _folders = new List<string>();
            var _files = new List<string>();
            bool sortByType = true;
            if (Directory.Exists(_path))
            {
                if (_filter == FileFilter.All || _filter == FileFilter.Directories)
                {
                    _folders = Directory.EnumerateDirectories(_path).ToList();
                }
                if (_filter == FileFilter.All || _filter == FileFilter.Files)
                {
                    _files = Directory.EnumerateFiles(_path).ToList();
                }

                if (arraySort.Length > 0)
                {
                    switch (arraySort[0].ToLower())
                    {
                        case "name":
                            if (arraySort[1].ToLower().Equals("desc"))
                            {
                                _folders = _folders.OrderByDescending(x => new DirectoryInfo(x).Name).ToList();
                                _files = _files.OrderByDescending(x => new FileInfo(x).Name).ToList();
                            }
                            else
                            {
                                _folders = _folders.OrderBy(x => new DirectoryInfo(x).Name).ToList();
                                _files = _files.OrderBy(x => new FileInfo(x).Name).ToList();
                            }
                            break;
                        case "size":
                            if (arraySort[1].ToLower().Equals("desc"))
                            {
                                _folders = _folders.OrderByDescending(x => new DirectoryInfo(x).Name).ToList();
                                _files = _files.OrderByDescending(x => new FileInfo(x).Name).ToList();
                            }
                            else
                            {
                                _folders = _folders.OrderBy(x => new DirectoryInfo(x).Name).ToList();
                                _files = _files.OrderBy(x => new FileInfo(x).Name).ToList();
                            }
                            break;
                        case "type":
                            if (arraySort[1].ToLower().Equals("desc"))
                            {
                                sortByType = false;
                            }
                            else
                            {

                                sortByType = true;
                            }
                            break;
                        case "date":
                            if (arraySort[1].ToLower().Equals("desc"))
                            {
                                _folders = _folders.OrderByDescending(x => new DirectoryInfo(x).Name).ToList();
                                _files = _files.OrderByDescending(x => new FileInfo(x).Name).ToList();
                            }
                            else
                            {
                                _folders = _folders.OrderBy(x => new DirectoryInfo(x).Name).ToList();
                                _files = _files.OrderBy(x => new FileInfo(x).Name).ToList();
                            }
                            break;
                    }
                }
                if (sortByType)
                {
                    // List Folders
                    foreach (string _f in _folders)
                    {
                        DirectoryInfo _dir = new DirectoryInfo(_f);
                        _list.Add(CreateDirectoryItem(_dir));
                    }
                    // List Files
                    foreach (string _f in _files)
                    {
                        FileInfo _file = new FileInfo(_f);
                        string ext = Path.GetExtension(_file.Name);
                        if (FileHelper.IsExtensionAllowed(ext, fileExtAllow))
                        {
                            _list.Add(CreateFileItem(_file));
                        }
                    }
                }
                else
                {
                    // List Files
                    foreach (string _f in _files)
                    {
                        FileInfo _file = new FileInfo(_f);
                        string ext = Path.GetExtension(_file.Name);
                        if (FileHelper.IsExtensionAllowed(ext, fileExtAllow))
                        {
                            _list.Add(CreateFileItem(_file));
                        }
                    }
                    // List Folders
                    foreach (string _f in _folders)
                    {
                        DirectoryInfo _dir = new DirectoryInfo(_f);
                        _list.Add(CreateDirectoryItem(_dir));
                    }
                }

                //if (!string.IsNullOrEmpty(_sortOrder))
                //{
                //    SortExtensions.Sort<FileItem>(_list, _sortOrder);
                //}
            }
            if (!string.IsNullOrEmpty(_searchTxt))
            {
                _searchTxt = _searchTxt.ToLower();
                return _list.Where(c => string.Format("{0} {1}", c.Name, c.FullName).ToLower().Contains(_searchTxt)).ToList();
            }
            else
                return _list;
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool CreateNewFolder(string _folderName)
        {
            if (!Directory.Exists(_folderName))
                Directory.CreateDirectory(_folderName);
            return true;
        }
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool CreateNewFolder(string _path, string _folderName)
        {
            return CreateNewFolder(HttpContext.Current.Server.MapPath(_path + "/" + _folderName));
        }

        public bool CreateNewFile(string _fileName)
        {
            throw new NotImplementedException();
        }

        public bool CreateNewFile(string _path, string _fileName)
        {
            throw new NotImplementedException();
        }

        public bool RenameFileFolder(string oldName, string newName)
        {
            bool ok = false;
            string _path = HttpContext.Current.Server.MapPath(oldName);

            string _newPath = HttpContext.Current.Server.MapPath(newName);
            try
            {
                if (Directory.Exists(_path) && _path != _newPath)
                {
                    Directory.Move(_path, _newPath);
                    ok = true;
                }
                if (File.Exists(_path) && _path != _newPath)
                {
                    File.Move(_path, _newPath);
                    ok = true;
                }
            }
            catch
            {
                ok = false;
            }
            return ok;
        }
        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool DeleteFileFolder(string _path)
        {
            _path.Replace("//", "/");
            string path = HttpContext.Current.Server.MapPath(_path).Replace("//", "/");
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                Directory.Delete(path, true);
            else
                System.IO.File.Delete(path);
            return true;
        }
        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public void CopyFileFolder(string _source, string _destination)
        {
            _source.Replace("//", "/");
            string source = HttpContext.Current.Server.MapPath(_source).Replace("//", "/");
            string dest = HttpContext.Current.Server.MapPath(_destination);
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(source);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {

                string[] files = System.IO.Directory.GetFiles(source);

                // Copy the files and overwrite destination files if they already exist.

                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    // ADD Unique File Name Check to Below!!!!
                    string destt = Path.Combine(dest, name);
                    System.IO.File.Copy(file, destt, true);
                }
                string[] folders = Directory.GetDirectories(source);
                foreach (string folder in folders)
                {
                    if (folder != dest)
                    {
                        string name = Path.GetFileName(folder);
                        string destt = Path.Combine(dest, name);
                        CopyFileFolder(folder, destt);
                    }
                }

                // DirectoryCopy(source, dest, true);
            }
            else
            {
                string name = Path.GetFileName(source);
                System.IO.File.Copy(source, dest + "/" + name, true);
            }
        }
        public void MoveFileFolder(string _source, string _destination)
        {
            _source.Replace("//", "/");
            string source = HttpContext.Current.Server.MapPath(_source).Replace("//", "/");
            string dest = HttpContext.Current.Server.MapPath(_destination);
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(source);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var files = System.IO.Directory.EnumerateFiles(source);

                // Copy the files and overwrite destination files if they already exist.

                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    // ADD Unique File Name Check to Below!!!!
                    string destF = dest + "/" + name;
                    if (!File.Exists(destF))
                    {
                        string destt = Path.Combine(dest, destF);
                        System.IO.File.Move(file, destt);
                    }
                }
                var folders = Directory.EnumerateDirectories(source);
                foreach (string folder in folders)
                {
                    if (folder != dest)
                    {
                        string name = Path.GetFileName(folder);
                        string destt = Path.Combine(dest, name);
                        MoveFileFolder(folder, destt);
                    }
                }                
             //   Directory.Move(source, dest);
            }
            else
            {
                string name = Path.GetFileName(source);
                string destF = dest + "/" + name;
                if (!File.Exists(destF))
                {
                    System.IO.File.Move(source, destF);
                }
            }
        }
    }
}
