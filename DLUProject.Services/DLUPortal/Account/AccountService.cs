/* FileName: AccountService.cs
Project Name: ColorLife.Data
Date Created: 2/7/2014 8:32:28 AM
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
using System.Web;
using DLUProject.Data;

namespace DLUProject.Services
{
	/// <summary>
    /// Represents a AccountService
    /// </summary>
    public class AccountService : IServices<Account>
    {
		private IRepository<  Account> _objectProxy;

        public AccountService(IRepository<Account> proxy)
        {
            this._objectProxy = proxy;
           
        }      
		public IQueryable<Account> Table {  get { return _objectProxy.Table; }}
        public List<Account> All()
		{
			return _objectProxy.All();
		}
        public Account Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public Account Get(Expression<Func<Account, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Account entity)
        {
            var exit = _objectProxy.Table.FirstOrDefault(c => c.Email.Equals(entity.Email));
            if (exit == null)
                return _objectProxy.Insert(entity);
            return -9999;
        }
		public int Insert2(Account entity)
		{
            var exit = _objectProxy.Table.FirstOrDefault(c => c.Email.Equals(entity.Email));
            if (exit == null)
                return _objectProxy.Insert2(entity);
            return -9999;		
		}		
		public int Insert(IEnumerable<Account>items)
		{			
            return _objectProxy.Insert(items);        
		}
		public int Update(Account entity)
		{
			return _objectProxy.Update(entity);
		}
		public int Delete(Account entity)
		{			
            return _objectProxy.Delete(entity);
		}
		public int Delete(Expression<Func<Account, bool>> expression)
		{
			return _objectProxy.Delete(expression);
		}
        public int Delete(object id)
		{
          

            int kq = _objectProxy.Delete(id);
            
          
            return kq;
		}
		public int Delete(IEnumerable<Account>items)
		{			
            return _objectProxy.Delete(items);            
		}
        public int Delete()
		{
			return _objectProxy.Delete();
		}
		public IQueryable<Account> Paging(int pageSize, int pageNumber, out int totalItems)
        {
            throw new NotImplementedException();
        }
		public IQueryable<Account> Paging(IQueryable<Account>items, int pageSize, int pageNumber, out int totalItems)
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
                   "AccountID",
                    "Email",
                    "Password",
                    "FirstName",
                    "LastName",
                    "IsApproved",
                    "IsLockedOut",
                    "LoginFailedCount",
                    "LastLoginDate",
                    "DateCreated",

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

                    var AccountID = worksheet.Cells[iRow, GetColumnIndex(properties, "AccountID")].Value.ToInt();
                    var Email = worksheet.Cells[iRow, GetColumnIndex(properties, "Email")].Value ?? string.Empty;
                    var Password = worksheet.Cells[iRow, GetColumnIndex(properties, "Password")].Value ?? string.Empty;
                    var FirstName = worksheet.Cells[iRow, GetColumnIndex(properties, "FirstName")].Value ?? string.Empty;
                    var LastName = worksheet.Cells[iRow, GetColumnIndex(properties, "LastName")].Value ?? string.Empty;
                    var IsApproved = worksheet.Cells[iRow, GetColumnIndex(properties, "IsApproved")].Value.ToBool();
                    var IsLockedOut = worksheet.Cells[iRow, GetColumnIndex(properties, "IsLockedOut")].Value.ToBool();
                    var LoginFailedCount = worksheet.Cells[iRow, GetColumnIndex(properties, "LoginFailedCount")].Value.ToInt();
                    var LastLoginDate = worksheet.Cells[iRow, GetColumnIndex(properties, "LastLoginDate")].Value.ToDateTime();
                    var DateCreated = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCreated")].Value.ToDateTime();


                    var entity = new Account()
                    {
                        AccountID = AccountID,
                        Email = Email.ToString(),
                        Password = Password.ToString().EncodePassword(),
                        FirstName = FirstName.ToString(),
                        LastName = LastName.ToString(),
                        IsApproved = IsApproved,
                        IsLockedOut = IsLockedOut,
                        LoginFailedCount = LoginFailedCount,
                        LastLoginDate = LastLoginDate,
                        DateCreated = DateCreated,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
        }
		public string ExportToXml(List<Account> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Accounts");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Account");
				xmlWriter.WriteElementString("AccountID", null, entity.AccountID.ToString());
xmlWriter.WriteElementString("Email", null, entity.Email.ToString());
xmlWriter.WriteElementString("Password", null, string.Empty);
xmlWriter.WriteElementString("FirstName", null, entity.FirstName.ToString());
xmlWriter.WriteElementString("LastName", null, entity.LastName.ToString());
xmlWriter.WriteElementString("IsApproved", null, entity.IsApproved.ToString());
xmlWriter.WriteElementString("IsLockedOut", null, entity.IsLockedOut.ToString());
xmlWriter.WriteElementString("LoginFailedCount", null, entity.LoginFailedCount.ToString());
xmlWriter.WriteElementString("LastLoginDate", null, entity.LastLoginDate.ToString());
xmlWriter.WriteElementString("DateCreated", null, entity.DateCreated.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Account> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Account");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "AccountID",
                        "Email",
                        "Password",
                        "FirstName",
                        "LastName",
                        "IsApproved",
                        "IsLockedOut",
                        "LoginFailedCount",
                        "LastLoginDate",
                        "DateCreated",

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
					worksheet.Cells[row, col].Value = entity.AccountID;
col++;
worksheet.Cells[row, col].Value = entity.Email;
col++;
worksheet.Cells[row, col].Value = string.Empty;
col++;
worksheet.Cells[row, col].Value = entity.FirstName;
col++;
worksheet.Cells[row, col].Value = entity.LastName;
col++;
worksheet.Cells[row, col].Value = entity.IsApproved;
col++;
worksheet.Cells[row, col].Value = entity.IsLockedOut;
col++;
worksheet.Cells[row, col].Value = entity.LoginFailedCount;
col++;
worksheet.Cells[row, col].Value = entity.LastLoginDate.ToDateTime();
col++;
worksheet.Cells[row, col].Value = entity.DateCreated.ToDateTime();
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

