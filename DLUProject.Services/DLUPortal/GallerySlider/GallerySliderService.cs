/* FileName: GallerySliderService.cs
Project Name: DLUProject
Date Created: 14/11/2014 11:05:11 AM
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
    /// Represents a GallerySliderService
    /// </summary>
    public class GallerySliderService : IServices<GallerySlider>
    {
		private  IRepository<GallerySlider> _objectProxy;      
        public GallerySliderService(IRepository<GallerySlider> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<GallerySlider> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "GallerySlider_Cache_Key";
        public List<GallerySlider> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<GallerySlider>;
            if (cache == null)
            {
                var items = _objectProxy.All();
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }
		 
        public GallerySlider Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public GallerySlider Get(Expression<Func<GallerySlider, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(GallerySlider entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(GallerySlider entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<GallerySlider>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(GallerySlider entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(GallerySlider entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<GallerySlider, bool>> expression)
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
		public int Delete(IEnumerable<GallerySlider>items)
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
                   "GalleryID",
"DisplayFlags",
"Name",
"Description",
"ImageUrl",
"Url",
"Attribute",
"SortOrder",
"IsPublished",
"DateCreated",

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
						
                    var GalleryID = worksheet.Cells[iRow, GetColumnIndex(properties, "GalleryID")].Value.ToInt();
var DisplayFlags = worksheet.Cells[iRow, GetColumnIndex(properties, "DisplayFlags")].Value.ToInt();
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;
var ImageUrl = worksheet.Cells[iRow, GetColumnIndex(properties, "ImageUrl")].Value ?? string.Empty;
var Url = worksheet.Cells[iRow, GetColumnIndex(properties, "Url")].Value ?? string.Empty;
var Attribute = worksheet.Cells[iRow, GetColumnIndex(properties, "Attribute")].Value ?? string.Empty;
var SortOrder = worksheet.Cells[iRow, GetColumnIndex(properties, "SortOrder")].Value.ToInt();
var IsPublished = worksheet.Cells[iRow, GetColumnIndex(properties, "IsPublished")].Value.ToBool();
var DateCreated = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCreated")].Value.ToDateTime();


                    var entity = new GallerySlider()
                    {
                        GalleryID = GalleryID,
DisplayFlags = DisplayFlags,
Name = Name.ToString(),
Description = Description.ToString(),
ImageUrl = ImageUrl.ToString(),
Url = Url.ToString(),
Attribute = Attribute.ToString(),
SortOrder = SortOrder,
IsPublished = IsPublished,
DateCreated = DateCreated,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<GallerySlider> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("GallerySliders");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("GallerySlider");
				xmlWriter.WriteElementString("GalleryID", null, entity.GalleryID.ToString());
xmlWriter.WriteElementString("DisplayFlags", null, entity.DisplayFlags.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
xmlWriter.WriteElementString("ImageUrl", null, entity.ImageUrl.ToString());
xmlWriter.WriteElementString("Url", null, entity.Url.ToString());
xmlWriter.WriteElementString("Attribute", null, entity.Attribute.ToString());
xmlWriter.WriteElementString("SortOrder", null, entity.SortOrder.ToString());
xmlWriter.WriteElementString("IsPublished", null, entity.IsPublished.ToString());
xmlWriter.WriteElementString("DateCreated", null, entity.DateCreated.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<GallerySlider> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("GallerySlider");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "GalleryID",
"DisplayFlags",
"Name",
"Description",
"ImageUrl",
"Url",
"Attribute",
"SortOrder",
"IsPublished",
"DateCreated",

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
					worksheet.Cells[row, col].Value = entity.GalleryID;
col++;
worksheet.Cells[row, col].Value = entity.DisplayFlags;
col++;
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.Description;
col++;
worksheet.Cells[row, col].Value = entity.ImageUrl;
col++;
worksheet.Cells[row, col].Value = entity.Url;
col++;
worksheet.Cells[row, col].Value = entity.Attribute;
col++;
worksheet.Cells[row, col].Value = entity.SortOrder;
col++;
worksheet.Cells[row, col].Value = entity.IsPublished;
col++;
worksheet.Cells[row, col].Value = entity.DateCreated;
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

