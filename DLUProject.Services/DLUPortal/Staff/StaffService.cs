/* FileName: StaffService.cs
Project Name: DLUProject
Date Created: 22/11/2014 9:07:03 PM
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
    /// Represents a StaffService
    /// </summary>
    public class StaffService : IServices<Staff>
    {
		private  IRepository<Staff> _objectProxy;      
        public StaffService(IRepository<Staff> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Staff> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Staff_Cache_Key";
        public List<Staff> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Staff>;
            if (cache == null)
            {
                var items = _objectProxy.All();
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }
		 
        public Staff Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public Staff Get(Expression<Func<Staff, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Staff entity)
		{
            var exists = _objectProxy.Table.FirstOrDefault(c => c.Email.Equals(entity.Email));
            if (exists == null)
            {
                int kq = _objectProxy.Insert(entity);
                DataCache.RemoveCache(cacheKey);
                return kq;
            }
            return -9999;
		}	
		public int Insert2(Staff entity)
		{
            var exists = _objectProxy.Table.FirstOrDefault(c => c.Email.Equals(entity.Email));
            if (exists == null)
            {
                int kq = _objectProxy.Insert2(entity);
                DataCache.RemoveCache(cacheKey);
                return kq;
            }
            return -9999;      			
		}		
		public int Insert(IEnumerable<Staff>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Staff entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Staff entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Staff, bool>> expression)
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
		public int Delete(IEnumerable<Staff>items)
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
                   "StaffID",
"StaffNoID",
"Title",
"FirstName",
"LastName",
"DOB",
"Phone",
"Fax",
"Mobile",
"Email",
"Image",
"Degree",
"Position",
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
						
                    var StaffID = worksheet.Cells[iRow, GetColumnIndex(properties, "StaffID")].Value.ToInt();
var StaffNoID = worksheet.Cells[iRow, GetColumnIndex(properties, "StaffNoID")].Value ?? string.Empty;
var Title = worksheet.Cells[iRow, GetColumnIndex(properties, "Title")].Value ?? string.Empty;
var FirstName = worksheet.Cells[iRow, GetColumnIndex(properties, "FirstName")].Value ?? string.Empty;
var LastName = worksheet.Cells[iRow, GetColumnIndex(properties, "LastName")].Value ?? string.Empty;
var DOB = worksheet.Cells[iRow, GetColumnIndex(properties, "DOB")].Value.ToDateTime();
var Phone = worksheet.Cells[iRow, GetColumnIndex(properties, "Phone")].Value ?? string.Empty;
var Fax = worksheet.Cells[iRow, GetColumnIndex(properties, "Fax")].Value ?? string.Empty;
var Mobile = worksheet.Cells[iRow, GetColumnIndex(properties, "Mobile")].Value ?? string.Empty;
var Email = worksheet.Cells[iRow, GetColumnIndex(properties, "Email")].Value ?? string.Empty;
var Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value ?? string.Empty;
var Degree = worksheet.Cells[iRow, GetColumnIndex(properties, "Degree")].Value ?? string.Empty;
var Position = worksheet.Cells[iRow, GetColumnIndex(properties, "Position")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;


                    var entity = new Staff()
                    {
                        StaffID = StaffID,
StaffNoID = StaffNoID.ToString(),
Title = Title.ToString(),
FirstName = FirstName.ToString(),
LastName = LastName.ToString(),
DOB = DOB,
Phone = Phone.ToString(),
Fax = Fax.ToString(),
Mobile = Mobile.ToString(),
Email = Email.ToString(),
Image = Image.ToString(),
Degree = Degree.ToString(),
Position = Position.ToString(),
Description = Description.ToString(),

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Staff> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Staffs");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Staff");
				xmlWriter.WriteElementString("StaffID", null, entity.StaffID.ToString());
xmlWriter.WriteElementString("StaffNoID", null, entity.StaffNoID.ToString());
xmlWriter.WriteElementString("Title", null, entity.Title.ToString());
xmlWriter.WriteElementString("FirstName", null, entity.FirstName.ToString());
xmlWriter.WriteElementString("LastName", null, entity.LastName.ToString());
xmlWriter.WriteElementString("DOB", null, entity.DOB.ToString());
xmlWriter.WriteElementString("Phone", null, entity.Phone.ToString());
xmlWriter.WriteElementString("Fax", null, entity.Fax.ToString());
xmlWriter.WriteElementString("Mobile", null, entity.Mobile.ToString());
xmlWriter.WriteElementString("Email", null, entity.Email.ToString());
xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
xmlWriter.WriteElementString("Degree", null, entity.Degree.ToString());
xmlWriter.WriteElementString("Position", null, entity.Position.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Staff> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Staff");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "StaffID",
"StaffNoID",
"Title",
"FirstName",
"LastName",
"DOB",
"Phone",
"Fax",
"Mobile",
"Email",
"Image",
"Degree",
"Position",
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
					worksheet.Cells[row, col].Value = entity.StaffID;
col++;
worksheet.Cells[row, col].Value = entity.StaffNoID;
col++;
worksheet.Cells[row, col].Value = entity.Title;
col++;
worksheet.Cells[row, col].Value = entity.FirstName;
col++;
worksheet.Cells[row, col].Value = entity.LastName;
col++;
worksheet.Cells[row, col].Value = entity.DOB;
col++;
worksheet.Cells[row, col].Value = entity.Phone;
col++;
worksheet.Cells[row, col].Value = entity.Fax;
col++;
worksheet.Cells[row, col].Value = entity.Mobile;
col++;
worksheet.Cells[row, col].Value = entity.Email;
col++;
worksheet.Cells[row, col].Value = entity.Image;
col++;
worksheet.Cells[row, col].Value = entity.Degree;
col++;
worksheet.Cells[row, col].Value = entity.Position;
col++;
worksheet.Cells[row, col].Value = entity.Description;
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

