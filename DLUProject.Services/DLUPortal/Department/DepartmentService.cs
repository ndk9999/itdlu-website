/* FileName: DepartmentService.cs
Project Name: DLUProject
Date Created: 20/11/2014 4:03:44 PM
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
    /// Represents a DepartmentService
    /// </summary>
    public class DepartmentService : IServices<Department>
    {
		private  IRepository<Department> _objectProxy;      
        public DepartmentService(IRepository<Department> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Department> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Department_Cache_Key";
        public List<Department> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Department>;
            if (cache == null)
            {
                var items = _objectProxy.All();
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }
		 
        public Department Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public Department Get(Expression<Func<Department, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Department entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(Department entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<Department>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Department entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Department entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Department, bool>> expression)
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
		public int Delete(IEnumerable<Department>items)
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
                   "DepartmentID",
"Name",
"ShortName",
"Alias",
"Description",
"HeadOffice",
"Dean",
"Address",
"Email",
"Phone",
"Fax",
"Website",
"ParentID",
"DateStarted",

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
						
                    var DepartmentID = worksheet.Cells[iRow, GetColumnIndex(properties, "DepartmentID")].Value.ToInt();
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var ShortName = worksheet.Cells[iRow, GetColumnIndex(properties, "ShortName")].Value ?? string.Empty;
var Alias = worksheet.Cells[iRow, GetColumnIndex(properties, "Alias")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;
var HeadOffice = worksheet.Cells[iRow, GetColumnIndex(properties, "HeadOffice")].Value ?? string.Empty;
var Dean = worksheet.Cells[iRow, GetColumnIndex(properties, "Dean")].Value ?? string.Empty;
var Address = worksheet.Cells[iRow, GetColumnIndex(properties, "Address")].Value ?? string.Empty;
var Email = worksheet.Cells[iRow, GetColumnIndex(properties, "Email")].Value ?? string.Empty;
var Phone = worksheet.Cells[iRow, GetColumnIndex(properties, "Phone")].Value ?? string.Empty;
var Fax = worksheet.Cells[iRow, GetColumnIndex(properties, "Fax")].Value ?? string.Empty;
var Website = worksheet.Cells[iRow, GetColumnIndex(properties, "Website")].Value ?? string.Empty;
var ParentID = worksheet.Cells[iRow, GetColumnIndex(properties, "ParentID")].Value.ToInt();
var DateStarted = worksheet.Cells[iRow, GetColumnIndex(properties, "DateStarted")].Value.ToDateTime();


                    var entity = new Department()
                    {
                        DepartmentID = DepartmentID,
Name = Name.ToString(),
ShortName = ShortName.ToString(),
Alias = Alias.ToString(),
Description = Description.ToString(),
HeadOffice = HeadOffice.ToString(),
Dean = Dean.ToString(),
Address = Address.ToString(),
Email = Email.ToString(),
Phone = Phone.ToString(),
Fax = Fax.ToString(),
Website = Website.ToString(),
ParentID = ParentID,
DateStarted = DateStarted,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Department> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Departments");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Department");
				xmlWriter.WriteElementString("DepartmentID", null, entity.DepartmentID.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("ShortName", null, entity.ShortName.ToString());
xmlWriter.WriteElementString("Alias", null, entity.Alias.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
xmlWriter.WriteElementString("HeadOffice", null, entity.HeadOffice.ToString());
xmlWriter.WriteElementString("Dean", null, entity.Dean.ToString());
xmlWriter.WriteElementString("Address", null, entity.Address.ToString());
xmlWriter.WriteElementString("Email", null, entity.Email.ToString());
xmlWriter.WriteElementString("Phone", null, entity.Phone.ToString());
xmlWriter.WriteElementString("Fax", null, entity.Fax.ToString());
xmlWriter.WriteElementString("Website", null, entity.Website.ToString());
xmlWriter.WriteElementString("ParentID", null, entity.ParentID.ToString());
xmlWriter.WriteElementString("DateStarted", null, entity.DateStarted.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Department> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Department");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "DepartmentID",
"Name",
"ShortName",
"Alias",
"Description",
"HeadOffice",
"Dean",
"Address",
"Email",
"Phone",
"Fax",
"Website",
"ParentID",
"DateStarted",

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
					worksheet.Cells[row, col].Value = entity.DepartmentID;
col++;
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.ShortName;
col++;
worksheet.Cells[row, col].Value = entity.Alias;
col++;
worksheet.Cells[row, col].Value = entity.Description;
col++;
worksheet.Cells[row, col].Value = entity.HeadOffice;
col++;
worksheet.Cells[row, col].Value = entity.Dean;
col++;
worksheet.Cells[row, col].Value = entity.Address;
col++;
worksheet.Cells[row, col].Value = entity.Email;
col++;
worksheet.Cells[row, col].Value = entity.Phone;
col++;
worksheet.Cells[row, col].Value = entity.Fax;
col++;
worksheet.Cells[row, col].Value = entity.Website;
col++;
worksheet.Cells[row, col].Value = entity.ParentID;
col++;
worksheet.Cells[row, col].Value = entity.DateStarted;
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

