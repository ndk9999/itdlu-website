/* FileName: AccountLogService.cs
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
using DLUProject.Data;

namespace DLUProject.Services
{
    /// <summary>
    /// Represents a AccountLogService
    /// </summary>
    public class AccountLogService : IServices<AccountLog>
    {
        private IRepository< AccountLog> _objectProxy;
        public AccountLogService(IRepository<AccountLog> proxy)
        {
            this._objectProxy = proxy;
        }
        public IQueryable<AccountLog> Table { get { return _objectProxy.Table; } }
        public List<AccountLog> All()
        {
            return _objectProxy.All();
        }
        public AccountLog Get(object id)
        {
            return _objectProxy.Get(id);
        }
        public AccountLog Get(Expression<Func<AccountLog, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(AccountLog entity)
        {
            return _objectProxy.Insert(entity);
        }
        public int Insert2(AccountLog entity)
        {
            return _objectProxy.Insert2(entity);
        }
        public int Insert(IEnumerable<AccountLog> items)
        {
            return _objectProxy.Insert(items);
        }
        public int Update(AccountLog entity)
        {
            return _objectProxy.Update(entity);
        }
        public int Delete(AccountLog entity)
        {
            return _objectProxy.Delete(entity);
        }
        public int Delete(Expression<Func<AccountLog, bool>> expression)
        {
            return _objectProxy.Delete(expression);
        }
        public int Delete(object id)
        {
            return _objectProxy.Delete(id);
        }
        public int Delete(IEnumerable<AccountLog> items)
        {
            return _objectProxy.Delete(items);
        }
        public int Delete()
        {
            return _objectProxy.Delete();
        }
        public IQueryable<AccountLog> Paging(int pageSize, int pageNumber, out int totalItems)
        {
            throw new NotImplementedException();
        }
        public IQueryable<AccountLog> Paging(IQueryable<AccountLog> items, int pageSize, int pageNumber, out int totalItems)
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
                   "LogID",
"AccountID",
"LevelError",
"Source",
"ComputerName",
"IPAddress",
"MACAddress",
"Url",
"UserAgent",
"LoggedDate",
"Detail",

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

                    var LogID = worksheet.Cells[iRow, GetColumnIndex(properties, "LogID")].Value.ToInt();
                    var AccountID = worksheet.Cells[iRow, GetColumnIndex(properties, "AccountID")].Value.ToInt();
                    var LevelError = worksheet.Cells[iRow, GetColumnIndex(properties, "LevelError")].Value ?? string.Empty;
                    var Source = worksheet.Cells[iRow, GetColumnIndex(properties, "Source")].Value ?? string.Empty;
                    var ComputerName = worksheet.Cells[iRow, GetColumnIndex(properties, "ComputerName")].Value ?? string.Empty;
                    var IPAddress = worksheet.Cells[iRow, GetColumnIndex(properties, "IPAddress")].Value ?? string.Empty;
                    var MACAddress = worksheet.Cells[iRow, GetColumnIndex(properties, "MACAddress")].Value ?? string.Empty;
                    var Url = worksheet.Cells[iRow, GetColumnIndex(properties, "Url")].Value ?? string.Empty;
                    var UserAgent = worksheet.Cells[iRow, GetColumnIndex(properties, "UserAgent")].Value ?? string.Empty;
                    var LoggedDate = worksheet.Cells[iRow, GetColumnIndex(properties, "LoggedDate")].Value.ToDateTime();
                    var Detail = worksheet.Cells[iRow, GetColumnIndex(properties, "Detail")].Value ?? string.Empty;


                    var entity = new AccountLog()
                    {
                        LogID = LogID,
                        AccountID = AccountID,

                        Source = Source.ToString(),
                        ComputerName = ComputerName.ToString(),
                        IPAddress = IPAddress.ToString(),
                        MACAddress = MACAddress.ToString(),
                        Url = Url.ToString(),
                        UserAgent = UserAgent.ToString(),
                        LoggedDate = LoggedDate,
                        Detail = Detail.ToString(),

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
        }
        public string ExportToXml(List<AccountLog> items)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("AccountLogs");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("AccountLog");
                xmlWriter.WriteElementString("LogID", null, entity.LogID.ToString());
                xmlWriter.WriteElementString("AccountID", null, entity.AccountID.ToString());
                xmlWriter.WriteElementString("LevelError", null, entity.LevelError.ToString());
                xmlWriter.WriteElementString("Source", null, entity.Source.ToString());
                xmlWriter.WriteElementString("ComputerName", null, entity.ComputerName.ToString());
                xmlWriter.WriteElementString("IPAddress", null, entity.IPAddress.ToString());
                xmlWriter.WriteElementString("MACAddress", null, entity.MACAddress.ToString());
                xmlWriter.WriteElementString("Url", null, entity.Url.ToString());
                xmlWriter.WriteElementString("UserAgent", null, entity.UserAgent.ToString());
                xmlWriter.WriteElementString("LoggedDate", null, entity.LoggedDate.ToString());
                xmlWriter.WriteElementString("Detail", null, entity.Detail.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }
        public void ExportToXlsx(Stream stream, List<AccountLog> items)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("AccountLog");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "LogID",
"AccountID",
"LevelError",
"Source",
"ComputerName",
"IPAddress",
"MACAddress",
"Url",
"UserAgent",
"LoggedDate",
"Detail",

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
                    worksheet.Cells[row, col].Value = entity.LogID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.AccountID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.LevelError;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Source;
                    col++;
                    worksheet.Cells[row, col].Value = entity.ComputerName;
                    col++;
                    worksheet.Cells[row, col].Value = entity.IPAddress;
                    col++;
                    worksheet.Cells[row, col].Value = entity.MACAddress;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Url;
                    col++;
                    worksheet.Cells[row, col].Value = entity.UserAgent;
                    col++;
                    worksheet.Cells[row, col].Value = entity.LoggedDate;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Detail;
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

        public List<AccountLog> GetByAccountID(int AccountID)
        {
            return _objectProxy.All().Where(c => c.AccountID == AccountID).ToList();
        }

    }
}

