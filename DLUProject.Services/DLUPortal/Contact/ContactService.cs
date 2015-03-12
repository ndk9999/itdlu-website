/* FileName: ContactService.cs
Project Name: DLUProject
Date Created: 17/11/2014 10:08:11 AM
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
    /// Represents a ContactService
    /// </summary>
    public class ContactService : IServices<Contact>
    {
		private  IRepository<Contact> _objectProxy;      
        public ContactService(IRepository<Contact> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Contact> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Contact_Cache_Key";
         public List<Contact> All()
         {
             var listX = _objectProxy.Table.Select(c => new Contact
             {
                 ContactID = c.ContactID,
                 IsRead = c.IsRead,
                 DateCreated = c.DateCreated,
                 Subject = c.Subject,
                 Email = c.Email,
                 Address = c.Address,
                 Body = c.Body,
                 FullName = c.FullName,
                 Phone = c.Phone
                 // Aticles = c.Aticles
             }).OrderByDescending(c => c.ContactID).ThenBy(c => c.IsRead).ToList();


             var cache = DataCache.GetCache(cacheKey) as List<Contact>;
             if (cache == null)
             {                  
                 DataCache.SetCache(cacheKey, listX, DateTime.Now.AddDays(1));
                 return listX;
             }
             return cache;
         }
		 
        public Contact Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public Contact Get(Expression<Func<Contact, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Contact entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(Contact entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<Contact>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Contact entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Contact entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Contact, bool>> expression)
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
		public int Delete(IEnumerable<Contact>items)
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
                   "ContactID",
"FullName",
"Address",
"Phone",
"Email",
"Subject",
"Body",
"DateCreated",
"IsRead",

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
						
                    var ContactID = worksheet.Cells[iRow, GetColumnIndex(properties, "ContactID")].Value.ToInt();
var FullName = worksheet.Cells[iRow, GetColumnIndex(properties, "FullName")].Value ?? string.Empty;
var Address = worksheet.Cells[iRow, GetColumnIndex(properties, "Address")].Value ?? string.Empty;
var Phone = worksheet.Cells[iRow, GetColumnIndex(properties, "Phone")].Value ?? string.Empty;
var Email = worksheet.Cells[iRow, GetColumnIndex(properties, "Email")].Value ?? string.Empty;
var Subject = worksheet.Cells[iRow, GetColumnIndex(properties, "Subject")].Value ?? string.Empty;
var Body = worksheet.Cells[iRow, GetColumnIndex(properties, "Body")].Value ?? string.Empty;
var DateCreated = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCreated")].Value.ToDateTime();
var IsRead = worksheet.Cells[iRow, GetColumnIndex(properties, "IsRead")].Value.ToBool();


                    var entity = new Contact()
                    {
                        ContactID = ContactID,
FullName = FullName.ToString(),
Address = Address.ToString(),
Phone = Phone.ToString(),
Email = Email.ToString(),
Subject = Subject.ToString(),
Body = Body.ToString(),
DateCreated = DateCreated,
IsRead = IsRead,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Contact> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Contacts");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Contact");
				xmlWriter.WriteElementString("ContactID", null, entity.ContactID.ToString());
xmlWriter.WriteElementString("FullName", null, entity.FullName.ToString());
xmlWriter.WriteElementString("Address", null, entity.Address.ToString());
xmlWriter.WriteElementString("Phone", null, entity.Phone.ToString());
xmlWriter.WriteElementString("Email", null, entity.Email.ToString());
xmlWriter.WriteElementString("Subject", null, entity.Subject.ToString());
xmlWriter.WriteElementString("Body", null, entity.Body.ToString());
xmlWriter.WriteElementString("DateCreated", null, entity.DateCreated.ToString());
xmlWriter.WriteElementString("IsRead", null, entity.IsRead.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Contact> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Contact");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "ContactID",
"FullName",
"Address",
"Phone",
"Email",
"Subject",
"Body",
"DateCreated",
"IsRead",

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
					worksheet.Cells[row, col].Value = entity.ContactID;
col++;
worksheet.Cells[row, col].Value = entity.FullName;
col++;
worksheet.Cells[row, col].Value = entity.Address;
col++;
worksheet.Cells[row, col].Value = entity.Phone;
col++;
worksheet.Cells[row, col].Value = entity.Email;
col++;
worksheet.Cells[row, col].Value = entity.Subject;
col++;
worksheet.Cells[row, col].Value = entity.Body;
col++;
worksheet.Cells[row, col].Value = entity.DateCreated;
col++;
worksheet.Cells[row, col].Value = entity.IsRead;
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

