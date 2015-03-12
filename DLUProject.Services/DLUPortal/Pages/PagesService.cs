/* FileName: PagesService.cs
Project Name: DLUProject
Date Created: 29/11/2014 10:40:12 AM
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
    /// Represents a PagesService
    /// </summary>
    public class PagesService : IServices<Pages>
    {
		private  IRepository<Pages> _objectProxy;      
        public PagesService(IRepository<Pages> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Pages> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Pages_Cache_Key";
         public List<Pages> All()
         {
             var cache = DataCache.GetCache(cacheKey) as List<Pages>;
             if (cache == null)
             {
                 var items = _objectProxy.Table.Select(c => new Pages
                 {
                     PageID = c.PageID,
                     Name = c.Name,
                     Alias = c.Alias.Trim().ToLower(),
                     Slug = c.Slug,
                     Description = c.Description,
                     FullText = c.FullText,
                     Image = c.Image,
                     IsPublished = c.IsPublished,
                     IsFrontPage = c.IsFrontPage,
                     DateCreated = c.DateCreated,
                     DateModified = c.DateModified,
                     ParentID = c.ParentID,
                     IsDeleted = c.IsDeleted,
                     MetaTitle = c.MetaTitle,
                     MetaDescription = c.MetaDescription,
                     MetaKeywords = c.MetaKeywords,

                 }).ToList();

                 DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                 return items;
             }
             return cache;
         }		 
        public Pages Get(object id)
		{
			 return _objectProxy.Get(id);
			 // return All().SingleOrDefault(c=>c.WorkGroupID.Equals(id));
		}
		public Pages Get(Expression<Func<Pages, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Pages entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(Pages entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<Pages>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Pages entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Pages entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Pages, bool>> expression)
		{
			 
			int kq= _objectProxy.Delete(expression);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
        public int Delete(object id)
		{
			 
			int kq= _objectProxy.Delete(id);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(IEnumerable<Pages>items)
		{			
			 
            int kq= _objectProxy.Delete(items);            
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
                   "PageID",
"Name",
"Alias",
"Slug",
"Description",
"FullText",
"Image",
"IsPublished",
"IsFrontPage",
"DateCreated",
"DateModified",
"ParentID",
"IsDeleted",
"MetaTitle",
"MetaDescription",
"MetaKeywords",

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
						
                    var PageID = worksheet.Cells[iRow, GetColumnIndex(properties, "PageID")].Value.ToInt();
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var Alias = worksheet.Cells[iRow, GetColumnIndex(properties, "Alias")].Value ?? string.Empty;
var Slug = worksheet.Cells[iRow, GetColumnIndex(properties, "Slug")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;
var FullText = worksheet.Cells[iRow, GetColumnIndex(properties, "FullText")].Value ?? string.Empty;
var Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value ?? string.Empty;
var IsPublished = worksheet.Cells[iRow, GetColumnIndex(properties, "IsPublished")].Value.ToBool();
var IsFrontPage = worksheet.Cells[iRow, GetColumnIndex(properties, "IsFrontPage")].Value.ToBool();
var DateCreated = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCreated")].Value.ToDateTime();
var DateModified = worksheet.Cells[iRow, GetColumnIndex(properties, "DateModified")].Value.ToDateTime();
var ParentID = worksheet.Cells[iRow, GetColumnIndex(properties, "ParentID")].Value.ToInt();
var IsDeleted = worksheet.Cells[iRow, GetColumnIndex(properties, "IsDeleted")].Value.ToBool();
var MetaTitle = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaTitle")].Value ?? string.Empty;
var MetaDescription = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaDescription")].Value ?? string.Empty;
var MetaKeywords = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaKeywords")].Value ?? string.Empty;


                    var entity = new Pages()
                    {
                        PageID = PageID,
Name = Name.ToString(),
Alias = Alias.ToString(),
Slug = Slug.ToString(),
Description = Description.ToString(),
FullText = FullText.ToString(),
Image = Image.ToString(),
IsPublished = IsPublished,
IsFrontPage = IsFrontPage,
DateCreated = DateCreated,
DateModified = DateModified,
ParentID = ParentID,
IsDeleted = IsDeleted,
MetaTitle = MetaTitle.ToString(),
MetaDescription = MetaDescription.ToString(),
MetaKeywords = MetaKeywords.ToString(),

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Pages> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Pagess");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Pages");
				xmlWriter.WriteElementString("PageID", null, entity.PageID.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("Alias", null, entity.Alias.ToString());
xmlWriter.WriteElementString("Slug", null, entity.Slug.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
xmlWriter.WriteElementString("FullText", null, entity.FullText.ToString());
xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
xmlWriter.WriteElementString("IsPublished", null, entity.IsPublished.ToString());
xmlWriter.WriteElementString("IsFrontPage", null, entity.IsFrontPage.ToString());
xmlWriter.WriteElementString("DateCreated", null, entity.DateCreated.ToString());
xmlWriter.WriteElementString("DateModified", null, entity.DateModified.ToString());
xmlWriter.WriteElementString("ParentID", null, entity.ParentID.ToString());
xmlWriter.WriteElementString("IsDeleted", null, entity.IsDeleted.ToString());
xmlWriter.WriteElementString("MetaTitle", null, entity.MetaTitle.ToString());
xmlWriter.WriteElementString("MetaDescription", null, entity.MetaDescription.ToString());
xmlWriter.WriteElementString("MetaKeywords", null, entity.MetaKeywords.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Pages> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Pages");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "PageID",
"Name",
"Alias",
"Slug",
"Description",
"FullText",
"Image",
"IsPublished",
"IsFrontPage",
"DateCreated",
"DateModified",
"ParentID",
"IsDeleted",
"MetaTitle",
"MetaDescription",
"MetaKeywords",

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
					worksheet.Cells[row, col].Value = entity.PageID;
col++;
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.Alias;
col++;
worksheet.Cells[row, col].Value = entity.Slug;
col++;
worksheet.Cells[row, col].Value = entity.Description;
col++;
worksheet.Cells[row, col].Value = entity.FullText;
col++;
worksheet.Cells[row, col].Value = entity.Image;
col++;
worksheet.Cells[row, col].Value = entity.IsPublished;
col++;
worksheet.Cells[row, col].Value = entity.IsFrontPage;
col++;
worksheet.Cells[row, col].Value = entity.DateCreated;
col++;
worksheet.Cells[row, col].Value = entity.DateModified;
col++;
worksheet.Cells[row, col].Value = entity.ParentID;
col++;
worksheet.Cells[row, col].Value = entity.IsDeleted;
col++;
worksheet.Cells[row, col].Value = entity.MetaTitle;
col++;
worksheet.Cells[row, col].Value = entity.MetaDescription;
col++;
worksheet.Cells[row, col].Value = entity.MetaKeywords;
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

		
    }
}

