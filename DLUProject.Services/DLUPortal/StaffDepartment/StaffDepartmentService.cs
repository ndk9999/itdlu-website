/* FileName: StaffDepartmentService.cs
Project Name: DLUProject
Date Created: 22/11/2014 9:11:49 PM
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
    /// Represents a StaffDepartmentService
    /// </summary>
    public class StaffDepartmentService : IServices<StaffDepartment>
    {
		private  IRepository<StaffDepartment> _objectProxy;      
        public StaffDepartmentService(IRepository<StaffDepartment> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<StaffDepartment> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "StaffDepartment_Cache_Key";
         public List<StaffDepartment> All()
         {
             var cache = DataCache.GetCache(cacheKey) as List<StaffDepartment>;
             if (cache == null)
             {
                 var items = _objectProxy.Table.Select(c => new StaffDepartment
                 {
                     DepartmentID = c.DepartmentID,
                     ID = c.ID,
                     StaffID = c.StaffID,
                     Staff = c.Staff,
                     Department = c.Department
                 }).ToList();
                 DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                 return items;
             }
             return cache;
         }
		 
        public StaffDepartment Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public StaffDepartment Get(Expression<Func<StaffDepartment, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(StaffDepartment entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(StaffDepartment entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<StaffDepartment>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(StaffDepartment entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(StaffDepartment entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<StaffDepartment, bool>> expression)
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
		public int Delete(IEnumerable<StaffDepartment>items)
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
                   "ID",
"StaffID",
"DepartmentID",

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
						
                    var ID = worksheet.Cells[iRow, GetColumnIndex(properties, "ID")].Value.ToInt();
var StaffID = worksheet.Cells[iRow, GetColumnIndex(properties, "StaffID")].Value.ToInt();
var DepartmentID = worksheet.Cells[iRow, GetColumnIndex(properties, "DepartmentID")].Value.ToInt();


                    var entity = new StaffDepartment()
                    {
                        ID = ID,
StaffID = StaffID,
DepartmentID = DepartmentID,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<StaffDepartment> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("StaffDepartments");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("StaffDepartment");
				xmlWriter.WriteElementString("ID", null, entity.ID.ToString());
xmlWriter.WriteElementString("StaffID", null, entity.StaffID.ToString());
xmlWriter.WriteElementString("DepartmentID", null, entity.DepartmentID.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<StaffDepartment> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("StaffDepartment");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "ID",
"StaffID",
"DepartmentID",

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
					worksheet.Cells[row, col].Value = entity.ID;
col++;
worksheet.Cells[row, col].Value = entity.StaffID;
col++;
worksheet.Cells[row, col].Value = entity.DepartmentID;
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

