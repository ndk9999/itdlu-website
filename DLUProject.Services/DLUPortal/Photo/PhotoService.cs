﻿/* FileName: PhotoService.cs
Project Name: DLUProject
Date Created: 28/11/2014 2:03:05 PM
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
    /// Represents a PhotoService
    /// </summary>
    public class PhotoService : IServices<Photo>
    {
		private  IRepository<Photo> _objectProxy;      
        public PhotoService(IRepository<Photo> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Photo> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Photo_Cache_Key";
         public List<Photo> All()
         {
             var cache = DataCache.GetCache(cacheKey) as List<Photo>;
             if (cache == null)
             {
                 var items = _objectProxy.Table.Select(c => new Photo
                 {
                     PhotoID = c.PhotoID,
                     GalleryID = c.GalleryID,
                     Image = c.Image,
                     Caption = c.Caption,
                     Description = c.Description,

                 }).OrderByDescending(c => c.PhotoID).ToList();

                 DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                 return items;
             }
             return cache;
         }
		 
        public Photo Get(object id)
		{
			 return _objectProxy.Get(id);
			 // return All().SingleOrDefault(c=>c.WorkGroupID.Equals(id));
		}
		public Photo Get(Expression<Func<Photo, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Photo entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(Photo entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<Photo>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Photo entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Photo entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Photo, bool>> expression)
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
		public int Delete(IEnumerable<Photo>items)
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
                   "PhotoID",
"GalleryID",
"Image",
"Caption",
"Description",

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
						
                    var PhotoID = worksheet.Cells[iRow, GetColumnIndex(properties, "PhotoID")].Value.ToInt();
var GalleryID = worksheet.Cells[iRow, GetColumnIndex(properties, "GalleryID")].Value.ToInt();
var Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value ?? string.Empty;
var Caption = worksheet.Cells[iRow, GetColumnIndex(properties, "Caption")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;


                    var entity = new Photo()
                    {
                        PhotoID = PhotoID,
GalleryID = GalleryID,
Image = Image.ToString(),
Caption = Caption.ToString(),
Description = Description.ToString(),

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Photo> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Photos");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Photo");
				xmlWriter.WriteElementString("PhotoID", null, entity.PhotoID.ToString());
xmlWriter.WriteElementString("GalleryID", null, entity.GalleryID.ToString());
xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
xmlWriter.WriteElementString("Caption", null, entity.Caption.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Photo> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Photo");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "PhotoID",
"GalleryID",
"Image",
"Caption",
"Description",

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
					worksheet.Cells[row, col].Value = entity.PhotoID;
col++;
worksheet.Cells[row, col].Value = entity.GalleryID;
col++;
worksheet.Cells[row, col].Value = entity.Image;
col++;
worksheet.Cells[row, col].Value = entity.Caption;
col++;
worksheet.Cells[row, col].Value = entity.Description;
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

