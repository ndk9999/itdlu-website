/* FileName: ResourceService.cs
Project Name: DLUProject
Date Created: 2/3/2015 8:54:58 AM
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
    /// Represents a ResourceService
    /// </summary>
    public class ResourceService : IServices<Resource>
    {
		private  IRepository<Resource> _objectProxy;      
        public ResourceService(IRepository<Resource> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Resource> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Resource_Cache_Key";
        public List<Resource> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Resource>;
            if (cache == null)
            {
                var items = _objectProxy.Table.Select(c => new Resource { 
					ResourceID = c.ResourceID,
CategoryID = c.CategoryID,
Title = c.Title,
Description = c.Description,
ResourceType = c.ResourceType,
CreatedByID = c.CreatedByID,
DateCreated = c.DateCreated,
ModifiedByID = c.ModifiedByID,
DateModified = c.DateModified,
IsPublished = c.IsPublished,

				}).ToList();
				
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }
		 
        public Resource Get(object id)
		{
			 return _objectProxy.Get(id);
			 // return All().SingleOrDefault(c=>c.WorkGroupID.Equals(id));
		}
		public Resource Get(Expression<Func<Resource, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Resource entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(Resource entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<Resource>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Resource entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Resource entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Resource, bool>> expression)
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
		public int Delete(IEnumerable<Resource>items)
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
                   "ResourceID",
"CategoryID",
"Title",
"Description",
"ResourceType",
"CreatedByID",
"DateCreated",
"ModifiedByID",
"DateModified",
"IsPublished",

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
						
                    var ResourceID = worksheet.Cells[iRow, GetColumnIndex(properties, "ResourceID")].Value.ToInt();
var CategoryID = worksheet.Cells[iRow, GetColumnIndex(properties, "CategoryID")].Value.ToInt();
var Title = worksheet.Cells[iRow, GetColumnIndex(properties, "Title")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;
var ResourceType = worksheet.Cells[iRow, GetColumnIndex(properties, "ResourceType")].Value ?? string.Empty;
var CreatedByID = worksheet.Cells[iRow, GetColumnIndex(properties, "CreatedByID")].Value.ToInt();
var DateCreated = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCreated")].Value.ToDateTime();
var ModifiedByID = worksheet.Cells[iRow, GetColumnIndex(properties, "ModifiedByID")].Value.ToInt();
var DateModified = worksheet.Cells[iRow, GetColumnIndex(properties, "DateModified")].Value.ToDateTime();
var IsPublished = worksheet.Cells[iRow, GetColumnIndex(properties, "IsPublished")].Value.ToBool();


                    var entity = new Resource()
                    {
                        ResourceID = ResourceID,
CategoryID = CategoryID,
Title = Title.ToString(),
Description = Description.ToString(),
ResourceType = ResourceType.ToString(),
CreatedByID = CreatedByID,
DateCreated = DateCreated,
ModifiedByID = ModifiedByID,
DateModified = DateModified,
IsPublished = IsPublished,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Resource> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Resources");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Resource");
				xmlWriter.WriteElementString("ResourceID", null, entity.ResourceID.ToString());
xmlWriter.WriteElementString("CategoryID", null, entity.CategoryID.ToString());
xmlWriter.WriteElementString("Title", null, entity.Title.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
xmlWriter.WriteElementString("ResourceType", null, entity.ResourceType.ToString());
xmlWriter.WriteElementString("CreatedByID", null, entity.CreatedByID.ToString());
xmlWriter.WriteElementString("DateCreated", null, entity.DateCreated.ToString());
xmlWriter.WriteElementString("ModifiedByID", null, entity.ModifiedByID.ToString());
xmlWriter.WriteElementString("DateModified", null, entity.DateModified.ToString());
xmlWriter.WriteElementString("IsPublished", null, entity.IsPublished.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Resource> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Resource");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "ResourceID",
"CategoryID",
"Title",
"Description",
"ResourceType",
"CreatedByID",
"DateCreated",
"ModifiedByID",
"DateModified",
"IsPublished",

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
					worksheet.Cells[row, col].Value = entity.ResourceID;
col++;
worksheet.Cells[row, col].Value = entity.CategoryID;
col++;
worksheet.Cells[row, col].Value = entity.Title;
col++;
worksheet.Cells[row, col].Value = entity.Description;
col++;
worksheet.Cells[row, col].Value = entity.ResourceType;
col++;
worksheet.Cells[row, col].Value = entity.CreatedByID;
col++;
worksheet.Cells[row, col].Value = entity.DateCreated;
col++;
worksheet.Cells[row, col].Value = entity.ModifiedByID;
col++;
worksheet.Cells[row, col].Value = entity.DateModified;
col++;
worksheet.Cells[row, col].Value = entity.IsPublished;
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

