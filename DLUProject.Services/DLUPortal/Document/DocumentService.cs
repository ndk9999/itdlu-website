/* FileName: DocumentService.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:35:31 PM
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
    /// Represents a DocumentService
    /// </summary>
    public class DocumentService : IServices<Document>
    {
		private  IRepository<Document> _objectProxy;      
        public DocumentService(IRepository<Document> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Document> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Document_Cache_Key";
         public List<Document> All()
         {
             var cache = DataCache.GetCache(cacheKey) as List<Document>;
             if (cache == null)
             {
                 var items = _objectProxy.Table.Select(c => new Document
                 {
                     DocumentID = c.DocumentID,
                     Name = c.Name,
                     Alias = c.Alias,
                     Description = c.Description,
                     CategoryID = c.CategoryID,
                     DocTypeID = c.DocTypeID,
                     DepartmentID = c.DepartmentID,
                     DocumentNo = c.DocumentNo,
                     DateCreated = c.DateCreated,
                     DateEffected = c.DateEffected,
                     DateEnded = c.DateEnded,
                     DatePublished = c.DatePublished,
                     IsPublished = c.IsPublished,
                     IsDeleted = c.IsDeleted,
                     Category = c.Category,
                     DocType=c.DocType,
                     CountFileAttachment = c.DocFileAttachments.Count

                 }).OrderByDescending(x=>x.DocumentID).ToList();

                 DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                 return items;
             }
             return cache;
         }
		 
        public Document Get(object id)
		{
			 return _objectProxy.Get(id);
			 // return All().SingleOrDefault(c=>c.WorkGroupID.Equals(id));
		}
		public Document Get(Expression<Func<Document, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Document entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(Document entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<Document>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Document entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Document entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Document, bool>> expression)
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
		public int Delete(IEnumerable<Document>items)
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
"Name",
"Alias",
"Description",
"CategoryID",
"DocTypeID",
"DepartmentID",
"DocumentNo",
"DateCreated",
"DateEffected",
"DateEnded",
"DatePublished",
"IsPublished",
"IsDeleted",

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
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var Alias = worksheet.Cells[iRow, GetColumnIndex(properties, "Alias")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;
var CategoryID = worksheet.Cells[iRow, GetColumnIndex(properties, "CategoryID")].Value.ToInt();
var DocTypeID = worksheet.Cells[iRow, GetColumnIndex(properties, "DocTypeID")].Value.ToInt();
var DepartmentID = worksheet.Cells[iRow, GetColumnIndex(properties, "DepartmentID")].Value.ToInt();
var DocumentNo = worksheet.Cells[iRow, GetColumnIndex(properties, "DocumentNo")].Value ?? string.Empty;
var DateCreated = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCreated")].Value.ToDateTime();
var DateEffected = worksheet.Cells[iRow, GetColumnIndex(properties, "DateEffected")].Value.ToDateTime();
var DateEnded = worksheet.Cells[iRow, GetColumnIndex(properties, "DateEnded")].Value.ToDateTime();
var DatePublished = worksheet.Cells[iRow, GetColumnIndex(properties, "DatePublished")].Value.ToDateTime();
var IsPublished = worksheet.Cells[iRow, GetColumnIndex(properties, "IsPublished")].Value.ToBool();
var IsDeleted = worksheet.Cells[iRow, GetColumnIndex(properties, "IsDeleted")].Value.ToBool();


                    var entity = new Document()
                    {
                        DocumentID = DocumentID,
Name = Name.ToString(),
Alias = Alias.ToString(),
Description = Description.ToString(),
CategoryID = CategoryID,
DocTypeID = DocTypeID,
DepartmentID = DepartmentID,
DocumentNo = DocumentNo.ToString(),
DateCreated = DateCreated,
DateEffected = DateEffected,
DateEnded = DateEnded,
DatePublished = DatePublished,
IsPublished = IsPublished,
IsDeleted = IsDeleted,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Document> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Documents");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Document");
				xmlWriter.WriteElementString("DocumentID", null, entity.DocumentID.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("Alias", null, entity.Alias.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
xmlWriter.WriteElementString("CategoryID", null, entity.CategoryID.ToString());
xmlWriter.WriteElementString("DocTypeID", null, entity.DocTypeID.ToString());
xmlWriter.WriteElementString("DepartmentID", null, entity.DepartmentID.ToString());
xmlWriter.WriteElementString("DocumentNo", null, entity.DocumentNo.ToString());
xmlWriter.WriteElementString("DateCreated", null, entity.DateCreated.ToString());
xmlWriter.WriteElementString("DateEffected", null, entity.DateEffected.ToString());
xmlWriter.WriteElementString("DateEnded", null, entity.DateEnded.ToString());
xmlWriter.WriteElementString("DatePublished", null, entity.DatePublished.ToString());
xmlWriter.WriteElementString("IsPublished", null, entity.IsPublished.ToString());
xmlWriter.WriteElementString("IsDeleted", null, entity.IsDeleted.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Document> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Document");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "DocumentID",
"Name",
"Alias",
"Description",
"CategoryID",
"DocTypeID",
"DepartmentID",
"DocumentNo",
"DateCreated",
"DateEffected",
"DateEnded",
"DatePublished",
"IsPublished",
"IsDeleted",

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
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.Alias;
col++;
worksheet.Cells[row, col].Value = entity.Description;
col++;
worksheet.Cells[row, col].Value = entity.CategoryID;
col++;
worksheet.Cells[row, col].Value = entity.DocTypeID;
col++;
worksheet.Cells[row, col].Value = entity.DepartmentID;
col++;
worksheet.Cells[row, col].Value = entity.DocumentNo;
col++;
worksheet.Cells[row, col].Value = entity.DateCreated;
col++;
worksheet.Cells[row, col].Value = entity.DateEffected;
col++;
worksheet.Cells[row, col].Value = entity.DateEnded;
col++;
worksheet.Cells[row, col].Value = entity.DatePublished;
col++;
worksheet.Cells[row, col].Value = entity.IsPublished;
col++;
worksheet.Cells[row, col].Value = entity.IsDeleted;
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

