﻿/* FileName: GalleryCategoryService.cs
Project Name: DLUProject
Date Created: 28/11/2014 2:02:55 PM
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
    /// Represents a GalleryCategoryService
    /// </summary>
    public class GalleryCategoryService : IServices<GalleryCategory>
    {
		private  IRepository<GalleryCategory> _objectProxy;      
        public GalleryCategoryService(IRepository<GalleryCategory> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<GalleryCategory> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "GalleryCategory_Cache_Key";
         public List<GalleryCategory> All()
         {
             var cache = DataCache.GetCache(cacheKey) as List<GalleryCategory>;
             if (cache == null)
             {
                 var items = _objectProxy.Table.Select(c => new GalleryCategory
                 {
                     CategoryID = c.CategoryID,
                     Name = c.Name,
                     Alias = c.Alias,
                     Image = c.Image,
                     Description = c.Description,
                     ParentID = c.ParentID,
                     SortOrder = c.SortOrder,
                     IsPublished = c.IsPublished,
                     MetaTitle = c.MetaTitle,
                     MetaDescription = c.MetaDescription,
                     MetaKeywords = c.MetaKeywords,
                     CountGallery = c.Galleries.Count
                 }).ToList();

                 DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                 return items;
             }
             return cache;
         }
		 
        public GalleryCategory Get(object id)
		{
			 return _objectProxy.Get(id);
			 // return All().SingleOrDefault(c=>c.WorkGroupID.Equals(id));
		}
		public GalleryCategory Get(Expression<Func<GalleryCategory, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(GalleryCategory entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(GalleryCategory entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<GalleryCategory>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(GalleryCategory entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(GalleryCategory entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<GalleryCategory, bool>> expression)
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
		public int Delete(IEnumerable<GalleryCategory>items)
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
                   "CategoryID",
"Name",
"Alias",
"Image",
"Description",
"ParentID",
"SortOrder",
"IsPublished",
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
						
                    var CategoryID = worksheet.Cells[iRow, GetColumnIndex(properties, "CategoryID")].Value.ToInt();
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var Alias = worksheet.Cells[iRow, GetColumnIndex(properties, "Alias")].Value ?? string.Empty;
var Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;
var ParentID = worksheet.Cells[iRow, GetColumnIndex(properties, "ParentID")].Value.ToInt();
var SortOrder = worksheet.Cells[iRow, GetColumnIndex(properties, "SortOrder")].Value.ToInt();
var IsPublished = worksheet.Cells[iRow, GetColumnIndex(properties, "IsPublished")].Value.ToBool();
var MetaTitle = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaTitle")].Value ?? string.Empty;
var MetaDescription = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaDescription")].Value ?? string.Empty;
var MetaKeywords = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaKeywords")].Value ?? string.Empty;


                    var entity = new GalleryCategory()
                    {
                        CategoryID = CategoryID,
Name = Name.ToString(),
Alias = Alias.ToString(),
Image = Image.ToString(),
Description = Description.ToString(),
ParentID = ParentID,
SortOrder = SortOrder,
IsPublished = IsPublished,
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
		public string ExportToXml(List<GalleryCategory> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("GalleryCategorys");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("GalleryCategory");
				xmlWriter.WriteElementString("CategoryID", null, entity.CategoryID.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("Alias", null, entity.Alias.ToString());
xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
xmlWriter.WriteElementString("ParentID", null, entity.ParentID.ToString());
xmlWriter.WriteElementString("SortOrder", null, entity.SortOrder.ToString());
xmlWriter.WriteElementString("IsPublished", null, entity.IsPublished.ToString());
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
		public void ExportToXlsx(Stream stream, List<GalleryCategory> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("GalleryCategory");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "CategoryID",
"Name",
"Alias",
"Image",
"Description",
"ParentID",
"SortOrder",
"IsPublished",
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
					worksheet.Cells[row, col].Value = entity.CategoryID;
col++;
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.Alias;
col++;
worksheet.Cells[row, col].Value = entity.Image;
col++;
worksheet.Cells[row, col].Value = entity.Description;
col++;
worksheet.Cells[row, col].Value = entity.ParentID;
col++;
worksheet.Cells[row, col].Value = entity.SortOrder;
col++;
worksheet.Cells[row, col].Value = entity.IsPublished;
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

