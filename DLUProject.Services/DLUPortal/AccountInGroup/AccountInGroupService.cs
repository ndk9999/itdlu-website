/* FileName: AccountInGroupService.cs
Project Name: ColorLife.Data
Date Created: 16/5/2014 2:12:35 PM
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
    /// Represents a AccountInGroupService
    /// </summary>
    public class AccountInGroupService : IServices<AccountInGroup>
    {
		private  IRepository< AccountInGroup> _objectProxy;
        public AccountInGroupService(IRepository<AccountInGroup> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<AccountInGroup> Table {  get { return _objectProxy.Table; }}
        public List<AccountInGroup> All()
		{
			return _objectProxy.All();
		}
        public AccountInGroup Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public AccountInGroup Get(Expression<Func<AccountInGroup, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(AccountInGroup entity)
        {
            var exists = _objectProxy.Table.FirstOrDefault(c => c.GroupID.Equals(entity.GroupID) && c.AccountID.Equals(entity.AccountID));
            if (exists == null)
                return _objectProxy.Insert(entity);
            return -1;
        }
		public int Insert2(AccountInGroup entity)
		{
            var exists = _objectProxy.Table.FirstOrDefault(c => c.GroupID.Equals(entity.GroupID) && c.AccountID.Equals(entity.AccountID));
            if (exists == null)
                return _objectProxy.Insert2(entity);
            return -1;           			
		}		
		public int Insert(IEnumerable<AccountInGroup>items)
		{			
            return _objectProxy.Insert(items);        
		}
		public int Update(AccountInGroup entity)
		{
			return _objectProxy.Update(entity);
		}
		public int Delete(AccountInGroup entity)
		{			
            return _objectProxy.Delete(entity);
		}
		public int Delete(Expression<Func<AccountInGroup, bool>> expression)
		{
			return _objectProxy.Delete(expression);
		}
        public int Delete(object id)
		{
			return _objectProxy.Delete(id);
		}
		public int Delete(IEnumerable<AccountInGroup>items)
		{			
            return _objectProxy.Delete(items);            
		}
        public int Delete()
		{
			return _objectProxy.Delete();
		}
		public IQueryable<AccountInGroup> Paging(int pageSize, int pageNumber, out int totalItems)
        {
            throw new NotImplementedException();
        }
		public IQueryable<AccountInGroup> Paging(IQueryable<AccountInGroup>items, int pageSize, int pageNumber, out int totalItems)
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
                   "AccountInGroupID",
"AccountID",
"GroupID",

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
						
                    int AccountInGroupID = worksheet.Cells[iRow, GetColumnIndex(properties, "AccountInGroupID")].Value.ToInt();
int AccountID = worksheet.Cells[iRow, GetColumnIndex(properties, "AccountID")].Value.ToInt();
int GroupID = worksheet.Cells[iRow, GetColumnIndex(properties, "GroupID")].Value.ToInt();


                    var entity = new AccountInGroup()
                    {
                        AccountInGroupID = AccountInGroupID,
AccountID = AccountID,
GroupID = GroupID,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<AccountInGroup> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("AccountInGroups");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("AccountInGroup");
				xmlWriter.WriteElementString("AccountInGroupID", null, entity.AccountInGroupID.ToString());
xmlWriter.WriteElementString("AccountID", null, entity.AccountID.ToString());
xmlWriter.WriteElementString("GroupID", null, entity.GroupID.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<AccountInGroup> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("AccountInGroup");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "AccountInGroupID",
"AccountID",
"GroupID",

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
					worksheet.Cells[row, col].Value = entity.AccountInGroupID;
col++;
worksheet.Cells[row, col].Value = entity.AccountID;
col++;
worksheet.Cells[row, col].Value = entity.GroupID;
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

