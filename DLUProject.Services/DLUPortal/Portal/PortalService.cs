/* FileName: PortalService.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:47:55 AM
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
    /// Represents a PortalService
    /// </summary>
    public class PortalService : IServices<Portal>
    {
		private  IRepository<Portal> _objectProxy;      
        public PortalService(IRepository<Portal> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Portal> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Portal_Cache_Key";
        public List<Portal> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Portal>;
            if (cache == null)
            {
                var items = _objectProxy.All();
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }
		 
        public Portal Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public Portal Get(Expression<Func<Portal, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Portal entity)
		{			 
			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(Portal entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<Portal>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Portal entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Portal entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Portal, bool>> expression)
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
		public int Delete(IEnumerable<Portal>items)
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
                   "PortalID",
"Name",
"Url",
"Host",
"LogoUrl",
"SSLEnable",
"SecureUrl",
"IsDefault",

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
						
                    var PortalID = worksheet.Cells[iRow, GetColumnIndex(properties, "PortalID")].Value.ToInt();
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var Url = worksheet.Cells[iRow, GetColumnIndex(properties, "Url")].Value ?? string.Empty;
var Host = worksheet.Cells[iRow, GetColumnIndex(properties, "Host")].Value ?? string.Empty;
var LogoUrl = worksheet.Cells[iRow, GetColumnIndex(properties, "LogoUrl")].Value ?? string.Empty;
var SSLEnable = worksheet.Cells[iRow, GetColumnIndex(properties, "SSLEnable")].Value.ToBool();
var SecureUrl = worksheet.Cells[iRow, GetColumnIndex(properties, "SecureUrl")].Value ?? string.Empty;
var IsDefault = worksheet.Cells[iRow, GetColumnIndex(properties, "IsDefault")].Value.ToBool();


                    var entity = new Portal()
                    {
                        PortalID = PortalID,
Name = Name.ToString(),
Url = Url.ToString(),
Host = Host.ToString(),
LogoUrl = LogoUrl.ToString(),
SSLEnable = SSLEnable,
SecureUrl = SecureUrl.ToString(),
IsDefault = IsDefault,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Portal> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Portals");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Portal");
				xmlWriter.WriteElementString("PortalID", null, entity.PortalID.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("Url", null, entity.Url.ToString());
xmlWriter.WriteElementString("Host", null, entity.Host.ToString());
xmlWriter.WriteElementString("LogoUrl", null, entity.LogoUrl.ToString());
xmlWriter.WriteElementString("SSLEnable", null, entity.SSLEnable.ToString());
xmlWriter.WriteElementString("SecureUrl", null, entity.SecureUrl.ToString());
xmlWriter.WriteElementString("IsDefault", null, entity.IsDefault.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Portal> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Portal");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "PortalID",
"Name",
"Url",
"Host",
"LogoUrl",
"SSLEnable",
"SecureUrl",
"IsDefault",

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
					worksheet.Cells[row, col].Value = entity.PortalID;
col++;
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.Url;
col++;
worksheet.Cells[row, col].Value = entity.Host;
col++;
worksheet.Cells[row, col].Value = entity.LogoUrl;
col++;
worksheet.Cells[row, col].Value = entity.SSLEnable;
col++;
worksheet.Cells[row, col].Value = entity.SecureUrl;
col++;
worksheet.Cells[row, col].Value = entity.IsDefault;
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

