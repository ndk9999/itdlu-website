/* FileName: NoticeService.cs
Project Name: DLUProject
Date Created: 18/11/2014 6:54:19 PM
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
    /// Represents a NoticeService
    /// </summary>
    public class NoticeService : IServices<Notice>
    {
        private IRepository<Notice> _objectProxy;
        public NoticeService(IRepository<Notice> proxy)
        {
            this._objectProxy = proxy;
        }
        public IQueryable<Notice> Table { get { return _objectProxy.Table; } }

        private string cacheKey = "Notice_Cache_Key";
        public List<Notice> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Notice>;
            if (cache == null)
            {
                var items = _objectProxy.Table.Select(c => new Notice
                {
                    NoticeID = c.NoticeID,
                    SortOrder = c.SortOrder,
                    Category = c.Category,
                    CategoryID = c.CategoryID,
                    Alias = c.Alias,
                    CreatedByID = c.CreatedByID,
                    DateCreated = c.DateCreated,
                    DateModified = c.DateModified,
                    DepartmentID = c.DepartmentID,
                    Description = c.Description,
                    FullText = c.FullText,
                    Hits = c.Hits,
                    Image = c.Image,
                    IsDeleted = c.IsDeleted,
                    IsPublished = c.IsPublished,
                    MetaDescription = c.MetaDescription,
                    MetaKeywords = c.MetaKeywords,
                    MetaTitle = c.MetaTitle,
                    Name = c.Name,
                     Department = c.Department,
                    Url = c.Url,
                    IsPin = c.IsPin,
                    CountFileAttachment = c.NoticeFileAttachments.Count
                }).OrderBy(c => c.IsPin).OrderByDescending(c => c.NoticeID).ThenBy(c => c.SortOrder).ToList();
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }

        public Notice Get(object id)
        {
            return _objectProxy.Get(id);
        }
        public Notice Get(Expression<Func<Notice, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Notice entity)
        {

            int kq = _objectProxy.Insert(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Insert2(Notice entity)
        {
            int kq = _objectProxy.Insert2(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Insert(IEnumerable<Notice> items)
        {

            int kq = _objectProxy.Insert(items);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Update(Notice entity)
        {

            int kq = _objectProxy.Update(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(Notice entity)
        {

            int kq = _objectProxy.Delete(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(Expression<Func<Notice, bool>> expression)
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
        public int Delete(IEnumerable<Notice> items)
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


        public void ImportFromXlsx(Stream stream)
        {
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new ArgumentNullException("No worksheet found");

                //the columns
                var properties = new string[]
                {
                   "NoticeID",
"CategoryID",
"DepartmentID",
"Name",
"Alias",
 
"Description",
"FullText",
"Image",
"CreatedByID",
"MetaTitle",
"MetaKeywords",
"MetaDescription",
"IsPublished",
"DateCreated",
"DateModified",
"IsDeleted",
"Hits",
"Url",

                };
                int iRow = 2;

                while (true)
                {
                    bool allColumnsAreEmpty = true;
                    for (var i = 1; i <= properties.Length; i++)
                        if (worksheet.Cells[iRow, i].Value != null && !String.IsNullOrEmpty(worksheet.Cells[iRow, i].Value.ToString()))
                        {
                            allColumnsAreEmpty = false;
                            break;
                        }
                    if (allColumnsAreEmpty)
                        break;

                    var NoticeID = worksheet.Cells[iRow, GetColumnIndex(properties, "NoticeID")].Value.ToInt();
                    var CategoryID = worksheet.Cells[iRow, GetColumnIndex(properties, "CategoryID")].Value.ToInt();
                    var DepartmentID = worksheet.Cells[iRow, GetColumnIndex(properties, "DepartmentID")].Value.ToInt();
                    var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
                    var Alias = worksheet.Cells[iRow, GetColumnIndex(properties, "Alias")].Value ?? string.Empty;

                    var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;
                    var FullText = worksheet.Cells[iRow, GetColumnIndex(properties, "FullText")].Value ?? string.Empty;
                    var Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value ?? string.Empty;
                    var CreatedByID = worksheet.Cells[iRow, GetColumnIndex(properties, "CreatedByID")].Value ?? string.Empty;
                    var MetaTitle = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaTitle")].Value ?? string.Empty;
                    var MetaKeywords = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaKeywords")].Value ?? string.Empty;
                    var MetaDescription = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaDescription")].Value ?? string.Empty;
                    var IsPublished = worksheet.Cells[iRow, GetColumnIndex(properties, "IsPublished")].Value.ToBool();
                    var DateCreated = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCreated")].Value.ToDateTime();
                    var DateModified = worksheet.Cells[iRow, GetColumnIndex(properties, "DateModified")].Value.ToDateTime();
                    var IsDeleted = worksheet.Cells[iRow, GetColumnIndex(properties, "IsDeleted")].Value.ToBool();
                    var Hits = worksheet.Cells[iRow, GetColumnIndex(properties, "Hits")].Value.ToInt();
                    var Url = worksheet.Cells[iRow, GetColumnIndex(properties, "Url")].Value ?? string.Empty;


                    var entity = new Notice()
                    {
                        NoticeID = NoticeID,
                        CategoryID = CategoryID,
                        DepartmentID = DepartmentID,
                        Name = Name.ToString(),
                        Alias = Alias.ToString(),

                        Description = Description.ToString(),
                        FullText = FullText.ToString(),
                        Image = Image.ToString(),
                        CreatedByID = CreatedByID.ToString(),
                        MetaTitle = MetaTitle.ToString(),
                        MetaKeywords = MetaKeywords.ToString(),
                        MetaDescription = MetaDescription.ToString(),
                        IsPublished = IsPublished,
                        DateCreated = DateCreated,
                        DateModified = DateModified,
                        IsDeleted = IsDeleted,
                        Hits = Hits,
                        Url = Url.ToString(),

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
        }
        public string ExportToXml(List<Notice> items)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Notices");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Notice");
                xmlWriter.WriteElementString("NoticeID", null, entity.NoticeID.ToString());
                xmlWriter.WriteElementString("CategoryID", null, entity.CategoryID.ToString());
                xmlWriter.WriteElementString("DepartmentID", null, entity.DepartmentID.ToString());
                xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
                xmlWriter.WriteElementString("Alias", null, entity.Alias.ToString());

                xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
                xmlWriter.WriteElementString("FullText", null, entity.FullText.ToString());
                xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
                xmlWriter.WriteElementString("CreatedByID", null, entity.CreatedByID.ToString());
                xmlWriter.WriteElementString("MetaTitle", null, entity.MetaTitle.ToString());
                xmlWriter.WriteElementString("MetaKeywords", null, entity.MetaKeywords.ToString());
                xmlWriter.WriteElementString("MetaDescription", null, entity.MetaDescription.ToString());
                xmlWriter.WriteElementString("IsPublished", null, entity.IsPublished.ToString());
                xmlWriter.WriteElementString("DateCreated", null, entity.DateCreated.ToString());
                xmlWriter.WriteElementString("DateModified", null, entity.DateModified.ToString());
                xmlWriter.WriteElementString("IsDeleted", null, entity.IsDeleted.ToString());
                xmlWriter.WriteElementString("Hits", null, entity.Hits.ToString());
                xmlWriter.WriteElementString("Url", null, entity.Url.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }
        public void ExportToXlsx(Stream stream, List<Notice> items)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Notice");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "NoticeID",
"CategoryID",
"DepartmentID",
"Name",
"Alias",
 
"Description",
"FullText",
"Image",
"CreatedByID",
"MetaTitle",
"MetaKeywords",
"MetaDescription",
"IsPublished",
"DateCreated",
"DateModified",
"IsDeleted",
"Hits",
"Url",

                    };
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }
                int row = 2;
                foreach (var entity in items)
                {
                    int col = 1;
                    worksheet.Cells[row, col].Value = entity.NoticeID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.CategoryID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.DepartmentID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Name;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Alias;
                    col++;

                    worksheet.Cells[row, col].Value = entity.Description;
                    col++;
                    worksheet.Cells[row, col].Value = entity.FullText;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Image;
                    col++;
                    worksheet.Cells[row, col].Value = entity.CreatedByID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.MetaTitle;
                    col++;
                    worksheet.Cells[row, col].Value = entity.MetaKeywords;
                    col++;
                    worksheet.Cells[row, col].Value = entity.MetaDescription;
                    col++;
                    worksheet.Cells[row, col].Value = entity.IsPublished;
                    col++;
                    worksheet.Cells[row, col].Value = entity.DateCreated;
                    col++;
                    worksheet.Cells[row, col].Value = entity.DateModified;
                    col++;
                    worksheet.Cells[row, col].Value = entity.IsDeleted;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Hits;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Url;
                    col++;



                    row++;
                }

                // save the new spreadsheet
                xlPackage.Save();
            }
        }


        #region Utilities

        protected virtual int GetColumnIndex(string[] properties, string columnName)
        {
            if (properties == null)
                throw new ArgumentNullException("properties");

            if (columnName == null)
                throw new ArgumentNullException("columnName");

            for (int i = 0; i < properties.Length; i++)
                if (properties[i].Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return i + 1; //excel indexes start from 1
            return 0;
        }

        #endregion


    }
}
