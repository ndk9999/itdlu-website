/* FileName: WorkGroupService.cs
Project Name: DLUProject
Date Created: 25/11/2014 12:32:15 PM
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
    /// Represents a WorkGroupService
    /// </summary>
    public class WorkGroupService : IServices<WorkGroup>
    {
        private IRepository<WorkGroup> _objectProxy;
        public WorkGroupService(IRepository<WorkGroup> proxy)
        {
            this._objectProxy = proxy;
        }
        public IQueryable<WorkGroup> Table { get { return _objectProxy.Table; } }

        private string cacheKey = "WorkGroup_Cache_Key";
        public List<WorkGroup> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<WorkGroup>;
            if (cache == null)
            {
                var items = _objectProxy.Table.Select(c => new WorkGroup
                {
                    WorkGroupID = c.WorkGroupID,
                    SystemID = c.SystemID,
                    Name = c.Name,
                    ParentID = c.ParentID,
                    Url = c.Url,
                    Image = c.Image,
                    SortOrder = c.SortOrder,
                    DisplayFlags = c.DisplayFlags,
                    IsEnabled = c.IsEnabled,
                    Systems=c.Systems,
                     
                }).OrderBy(c=>c.SortOrder).ToList();

                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }

        public WorkGroup Get(object id)
        {
            return _objectProxy.Get(id);
            // return All().SingleOrDefault(c=>c.WorkGroupID.Equals(id));
        }
        public WorkGroup Get(Expression<Func<WorkGroup, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(WorkGroup entity)
        {

            int kq = _objectProxy.Insert(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Insert2(WorkGroup entity)
        {
            int kq = _objectProxy.Insert2(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Insert(IEnumerable<WorkGroup> items)
        {

            int kq = _objectProxy.Insert(items);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Update(WorkGroup entity)
        {

            int kq = _objectProxy.Update(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(WorkGroup entity)
        {

            int kq = _objectProxy.Delete(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(Expression<Func<WorkGroup, bool>> expression)
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
        public int Delete(IEnumerable<WorkGroup> items)
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

        #region ImportExport
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
                   "WorkGroupID",
"SystemID",
"Name",
"ParentID",
"Url",
"Image",
"SortOrder",
"DisplayFlags",
"IsEnabled",

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

                    var WorkGroupID = worksheet.Cells[iRow, GetColumnIndex(properties, "WorkGroupID")].Value.ToInt();
                    var SystemID = worksheet.Cells[iRow, GetColumnIndex(properties, "SystemID")].Value.ToInt();
                    var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
                    var ParentID = worksheet.Cells[iRow, GetColumnIndex(properties, "ParentID")].Value.ToInt();
                    var Url = worksheet.Cells[iRow, GetColumnIndex(properties, "Url")].Value ?? string.Empty;
                    var Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value ?? string.Empty;
                    var SortOrder = worksheet.Cells[iRow, GetColumnIndex(properties, "SortOrder")].Value.ToInt();
                    var DisplayFlags = worksheet.Cells[iRow, GetColumnIndex(properties, "DisplayFlags")].Value.ToInt();
                    var IsEnabled = worksheet.Cells[iRow, GetColumnIndex(properties, "IsEnabled")].Value.ToBool();


                    var entity = new WorkGroup()
                    {
                        WorkGroupID = WorkGroupID,
                        SystemID = SystemID,
                        Name = Name.ToString(),
                        ParentID = ParentID,
                        Url = Url.ToString(),
                        Image = Image.ToString(),
                        SortOrder = SortOrder,
                        DisplayFlags = DisplayFlags,
                        IsEnabled = IsEnabled,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
        }
        public string ExportToXml(List<WorkGroup> items)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("WorkGroups");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("WorkGroup");
                xmlWriter.WriteElementString("WorkGroupID", null, entity.WorkGroupID.ToString());
                xmlWriter.WriteElementString("SystemID", null, entity.SystemID.ToString());
                xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
                xmlWriter.WriteElementString("ParentID", null, entity.ParentID.ToString());
                xmlWriter.WriteElementString("Url", null, entity.Url.ToString());
                xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
                xmlWriter.WriteElementString("SortOrder", null, entity.SortOrder.ToString());
                xmlWriter.WriteElementString("DisplayFlags", null, entity.DisplayFlags.ToString());
                xmlWriter.WriteElementString("IsEnabled", null, entity.IsEnabled.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }
        public void ExportToXlsx(Stream stream, List<WorkGroup> items)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("WorkGroup");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "WorkGroupID",
"SystemID",
"Name",
"ParentID",
"Url",
"Image",
"SortOrder",
"DisplayFlags",
"IsEnabled",

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
                    worksheet.Cells[row, col].Value = entity.WorkGroupID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.SystemID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Name;
                    col++;
                    worksheet.Cells[row, col].Value = entity.ParentID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Url;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Image;
                    col++;
                    worksheet.Cells[row, col].Value = entity.SortOrder;
                    col++;
                    worksheet.Cells[row, col].Value = entity.DisplayFlags;
                    col++;
                    worksheet.Cells[row, col].Value = entity.IsEnabled;
                    col++;



                    row++;
                }

                // save the new spreadsheet
                xlPackage.Save();
            }
        }
        #endregion

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

        public List<WorkGroup> GetBySystemID(int SystemID)
        {
            return _objectProxy.All().Where(c => c.SystemID == SystemID).ToList();
        }

    }
}

