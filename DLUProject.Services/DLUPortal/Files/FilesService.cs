/* FileName: FilesService.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:51 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/
using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Xml;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLToolkit.Mapping;
using BLToolkit.DataAccess;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
 

using DLUProject.Domain;
using DLUProject.Data;


using ColorLife.Core.Helper;
using OfficeOpenXml.Style;


namespace DLUProject.Services
{
    /// <summary>
    /// Represents a FilesService
    /// </summary>
    public class FilesService : IServices<Files>
    {
        private IRepository<Files> _objectProxy;
        public FilesService(IRepository<Files> proxy)
        {
            this._objectProxy = proxy;
        }
        public IQueryable<Files> Table { get { return _objectProxy.Table; } }

        private string cacheKey = "Files_Cache_Key";
        public List<Files> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Files>;
            if (cache == null)
            {
                var items = _objectProxy.Table.Select(c => new Files
                {
                    FileId = c.FileId,
                    FolderID = c.FolderID,
                    Name = c.Name,
                    FileName = c.FileName,
                    Extension = c.Extension,
                    Size = c.Size,
                    Width = c.Width,
                    Height = c.Height,
                    ContentType = c.ContentType,
                    Caption = c.Caption,
                    AltTag = c.AltTag,
                    FileUrl = c.FileUrl,
                    CreatedByID = c.CreatedByID,
                    DateCreated = c.DateCreated,
                    ModifiedByID = c.ModifiedByID,
                    DateModified = c.DateModified,
                    MD5Hash = c.MD5Hash,
                    IsPublished = c.IsPublished,
                    Folders = c.Folders
                }).OrderByDescending(x=>x.FileId).ToList();
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }

        public Files Get(object id)
        {
            return _objectProxy.Get(id);
        }
        public Files Get(Expression<Func<Files, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Files entity)
        {

            int kq = _objectProxy.Insert(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Insert2(Files entity)
        {
            int kq = _objectProxy.Insert2(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Insert(IEnumerable<Files> items)
        {

            int kq = _objectProxy.Insert(items);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Update(Files entity)
        {

            int kq = _objectProxy.Update(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(Files entity)
        {

            int kq = _objectProxy.Delete(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(Expression<Func<Files, bool>> expression)
        {

            int kq = _objectProxy.Delete(expression);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(object id)
        {

            int kq = _objectProxy.Delete(id);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(IEnumerable<Files> items)
        {

            int kq = _objectProxy.Delete(items);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete()
        {
            int kq = _objectProxy.Delete();
            DataCache.RemoveCache(cacheKey);
            return kq;
        }



    }
}

