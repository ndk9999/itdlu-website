/* FileName: DocFileAttachmentService.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:35:17 PM
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
    /// Represents a DocFileAttachmentService
    /// </summary>
    public class DocFileAttachmentService : IServices<DocFileAttachment>
    {
		private  IRepository<DocFileAttachment> _objectProxy;      
        public DocFileAttachmentService(IRepository<DocFileAttachment> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<DocFileAttachment> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "DocFileAttachment_Cache_Key";
        public List<DocFileAttachment> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<DocFileAttachment>;
            if (cache == null)
            {
                var items = _objectProxy.Table.Select(c => new DocFileAttachment { 
					DocumentID = c.DocumentID,
FileID = c.FileID,

				}).ToList();
				
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }
		 
        public DocFileAttachment Get(object id)
		{
			 return _objectProxy.Get(id);
			 // return All().SingleOrDefault(c=>c.WorkGroupID.Equals(id));
		}
		public DocFileAttachment Get(Expression<Func<DocFileAttachment, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(DocFileAttachment entity)
		{

            var exists = _objectProxy.Table.FirstOrDefault(x => x.DocumentID.Equals(entity.DocumentID) && x.FileID.Equals(entity.FileID));
            if (exists == null)
            {
                int kq = _objectProxy.Insert(entity);
                DataCache.RemoveCache(cacheKey);
                return kq;
            }
            return -1;
		}	
		public int Insert2(DocFileAttachment entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<DocFileAttachment>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(DocFileAttachment entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(DocFileAttachment entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<DocFileAttachment, bool>> expression)
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
		public int Delete(IEnumerable<DocFileAttachment>items)
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
                   "DocumentID",
"FileID",

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
						
                    var DocumentID = worksheet.Cells[iRow, GetColumnIndex(properties, "DocumentID")].Value.ToInt();
var FileID = worksheet.Cells[iRow, GetColumnIndex(properties, "FileID")].Value.ToInt();


                    var entity = new DocFileAttachment()
                    {
                        DocumentID = DocumentID,
FileID = FileID,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<DocFileAttachment> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("DocFileAttachments");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("DocFileAttachment");
				xmlWriter.WriteElementString("DocumentID", null, entity.DocumentID.ToString());
xmlWriter.WriteElementString("FileID", null, entity.FileID.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<DocFileAttachment> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("DocFileAttachment");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "DocumentID",
"FileID",

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
					worksheet.Cells[row, col].Value = entity.DocumentID;
col++;
worksheet.Cells[row, col].Value = entity.FileID;
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

