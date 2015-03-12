/* FileName: SettingService.cs
Project Name: DLUProject
Date Created: 14/11/2014 1:30:15 PM
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
    /// Represents a SettingService
    /// </summary>
    public class SettingService : IServices<Setting>
    {
		private  IRepository<Setting> _objectProxy;      
        public SettingService(IRepository<Setting> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Setting> Table {  get { return _objectProxy.Table; }}
        
		 private string cacheKey = "Setting_Cache_Key";
        public List<Setting> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Setting>;
            if (cache == null)
            {
                var items = _objectProxy.All();
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }
		 
        public Setting Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public Setting Get(Expression<Func<Setting, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Setting entity)
        {
            var exists = _objectProxy.Table.FirstOrDefault(c => c.Name.Equals(entity.Name));
            if (exists == null)
            {
                int kq = _objectProxy.Insert(entity);
                DataCache.RemoveCache(cacheKey);
                return kq;
            }
            return -1;
        }
		public int Insert2(Setting entity)
		{
            var exists = _objectProxy.Table.FirstOrDefault(c => c.Name.Equals(entity.Name));
            if (exists == null)
            {
                int kq = _objectProxy.Insert2(entity);
                DataCache.RemoveCache(cacheKey);
                return kq;
            }
            return -1;       			
		}		
		public int Insert(IEnumerable<Setting>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Setting entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Setting entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Setting, bool>> expression)
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
		public int Delete(IEnumerable<Setting>items)
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
                   "SettingId",
"Name",
"Value",

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
						
                    var SettingId = worksheet.Cells[iRow, GetColumnIndex(properties, "SettingId")].Value.ToInt();
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var Value = worksheet.Cells[iRow, GetColumnIndex(properties, "Value")].Value ?? string.Empty;


                    var entity = new Setting()
                    {
                        SettingId = SettingId,
Name = Name.ToString(),
Value = Value.ToString(),

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Setting> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Settings");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Setting");
				xmlWriter.WriteElementString("SettingId", null, entity.SettingId.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("Value", null, entity.Value.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Setting> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Setting");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "SettingId",
"Name",
"Value",

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
					worksheet.Cells[row, col].Value = entity.SettingId;
col++;
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.Value;
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

