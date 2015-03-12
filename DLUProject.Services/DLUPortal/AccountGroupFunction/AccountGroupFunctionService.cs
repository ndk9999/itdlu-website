/* FileName: AccountGroupFunctionService.cs
Project Name: ColorLife.Data
Date Created: 2/7/2014 8:32:27 AM
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
 


using ColorLife.Core.Helper;
using OfficeOpenXml.Style;

using DLUProject.Data;
namespace DLUProject.Services
{
	/// <summary>
    /// Represents a AccountGroupFunctionService
    /// </summary>
    public class AccountGroupFunctionService : IServices<AccountGroupFunction>
    {
		private  IRepository< AccountGroupFunction> _objectProxy;
        public AccountGroupFunctionService(IRepository<AccountGroupFunction> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<AccountGroupFunction> Table {  get { return _objectProxy.Table; }}
        public List<AccountGroupFunction> All()
		{
			return _objectProxy.All();
		}
        public AccountGroupFunction Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public AccountGroupFunction Get(Expression<Func<AccountGroupFunction, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(AccountGroupFunction entity)
		{			       
             return _objectProxy.Insert(entity);            			
		}	
		public int Insert2(AccountGroupFunction entity)
		{			       
             return _objectProxy.Insert2(entity);            			
		}		
		public int Insert(IEnumerable<AccountGroupFunction>items)
		{			
            return _objectProxy.Insert(items);        
		}
		public int Update(AccountGroupFunction entity)
		{
			return _objectProxy.Update(entity);
		}
		public int Delete(AccountGroupFunction entity)
		{			
            return _objectProxy.Delete(entity);
		}
		public int Delete(Expression<Func<AccountGroupFunction, bool>> expression)
		{
			return _objectProxy.Delete(expression);
		}
        public int Delete(object id)
		{
			return _objectProxy.Delete(id);
		}
		public int Delete(IEnumerable<AccountGroupFunction>items)
		{			
            return _objectProxy.Delete(items);            
		}
        public int Delete()
		{
			return _objectProxy.Delete();
		}
		public IQueryable<AccountGroupFunction> Paging(int pageSize, int pageNumber, out int totalItems)
        {
            throw new NotImplementedException();
        }
		public IQueryable<AccountGroupFunction> Paging(IQueryable<AccountGroupFunction>items, int pageSize, int pageNumber, out int totalItems)
        {
            throw new NotImplementedException();
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
                   "GroupID",
"FunctionID",

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
						
                    var GroupID = worksheet.Cells[iRow, GetColumnIndex(properties, "GroupID")].Value.ToInt();
var FunctionID = worksheet.Cells[iRow, GetColumnIndex(properties, "FunctionID")].Value.ToInt();


                    var entity = new AccountGroupFunction()
                    {
                        GroupID = GroupID,
FunctionID = FunctionID,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<AccountGroupFunction> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("AccountGroupFunctions");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("AccountGroupFunction");
				xmlWriter.WriteElementString("GroupID", null, entity.GroupID.ToString());
xmlWriter.WriteElementString("FunctionID", null, entity.FunctionID.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<AccountGroupFunction> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("AccountGroupFunction");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "GroupID",
"FunctionID",

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
					worksheet.Cells[row, col].Value = entity.GroupID;
col++;
worksheet.Cells[row, col].Value = entity.FunctionID;
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

