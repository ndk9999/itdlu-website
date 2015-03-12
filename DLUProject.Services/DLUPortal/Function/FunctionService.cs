/* FileName: FunctionService.cs
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
    /// Represents a FunctionService
    /// </summary>
    public class FunctionService : IServices<Function>
    {
		private  IRepository< Function> _objectProxy;
        private IRepository<WorkGroup> _wkRepository;
        public FunctionService(IRepository<Function> proxy, IRepository<WorkGroup> wkRepository)
        {
            this._objectProxy = proxy;
            this._wkRepository = wkRepository;
          
        }      
		public IQueryable<Function> Table {  get { return _objectProxy.Table; }}
        public List<Function> All()
        {
            return _objectProxy.Table.Select(c => new Function
            {
                FunctionID = c.FunctionID,
                WorkGroupID = c.WorkGroupID,
                Name = c.Name,
                SortOrder = c.SortOrder,
                IsEnabled = c.IsEnabled,
                Image = c.Image,
                Url = c.Url,
                WorkGroup = c.WorkGroup,
             //   Breadcrumb = GetFormattedBreadCrumb(c.WorkGroup, ">>") + ">>" + c.Name
            }).OrderBy(c => c.WorkGroupID).ThenBy(c => c.SortOrder).ToList();
        }
        private string GetFormattedBreadCrumb(WorkGroup WorkGroup, string separator = ">>")
        {
            if (WorkGroup == null)
                throw new ArgumentNullException("WorkGroup");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedWorkGroupIds = new List<int>() { };

            while (WorkGroup != null && !alreadyProcessedWorkGroupIds.Contains(WorkGroup.WorkGroupID)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = WorkGroup.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", WorkGroup.Name, separator, result);
                }

                alreadyProcessedWorkGroupIds.Add(WorkGroup.WorkGroupID);

                WorkGroup = _wkRepository.Get(WorkGroup.ParentID);

            }
            return result;
        }
        public Function Get(object id)
		{
			 return _objectProxy.Get(id);
		}
		public Function Get(Expression<Func<Function, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Function entity)
		{
            

            int kq = _objectProxy.Insert(entity);
            

            return kq;          			
		}	
		public int Insert2(Function entity)
		{
            
            int kq = _objectProxy.Insert2(entity);
             

            return kq;                    			
		}		
		public int Insert(IEnumerable<Function>items)
		{			
            return _objectProxy.Insert(items);        
		}
		public int Update(Function entity)
		{
            

            int kq = _objectProxy.Update(entity);

           

            return kq;
		}
		public int Delete(Function entity)
		{			
            return _objectProxy.Delete(entity);
		}
		public int Delete(Expression<Func<Function, bool>> expression)
		{
			return _objectProxy.Delete(expression);
		}
        public int Delete(object id)
		{
             

            int kq = _objectProxy.Delete(id);
             

            return kq;
		}
		public int Delete(IEnumerable<Function>items)
		{			
            return _objectProxy.Delete(items);            
		}
        public int Delete()
		{
			return _objectProxy.Delete();
		}
		public IQueryable<Function> Paging(int pageSize, int pageNumber, out int totalItems)
        {
            throw new NotImplementedException();
        }
		public IQueryable<Function> Paging(IQueryable<Function>items, int pageSize, int pageNumber, out int totalItems)
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
                   "FunctionID",
"WorkGroupID",
"Name",
"Image",
"Url",
"SortOrder",
"IsEnabled",

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
						
                    var FunctionID = worksheet.Cells[iRow, GetColumnIndex(properties, "FunctionID")].Value.ToInt();
var WorkGroupID = worksheet.Cells[iRow, GetColumnIndex(properties, "WorkGroupID")].Value.ToInt();
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value ?? string.Empty;
var Url = worksheet.Cells[iRow, GetColumnIndex(properties, "Url")].Value ?? string.Empty;
var SortOrder = worksheet.Cells[iRow, GetColumnIndex(properties, "SortOrder")].Value.ToInt();
var IsEnabled = worksheet.Cells[iRow, GetColumnIndex(properties, "IsEnabled")].Value.ToBool();


                    var entity = new Function()
                    {
                        FunctionID = FunctionID,
WorkGroupID = WorkGroupID,
Name = Name.ToString(),
Image = Image.ToString(),
Url = Url.ToString(),
SortOrder = SortOrder,
IsEnabled = IsEnabled,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Function> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Functions");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Function");
				xmlWriter.WriteElementString("FunctionID", null, entity.FunctionID.ToString());
xmlWriter.WriteElementString("WorkGroupID", null, entity.WorkGroupID.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
xmlWriter.WriteElementString("Url", null, entity.Url.ToString());
xmlWriter.WriteElementString("SortOrder", null, entity.SortOrder.ToString());
xmlWriter.WriteElementString("IsEnabled", null, entity.IsEnabled.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Function> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Function");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "FunctionID",
"WorkGroupID",
"Name",
"Image",
"Url",
"SortOrder",
"IsEnabled",

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
					worksheet.Cells[row, col].Value = entity.FunctionID;
col++;
worksheet.Cells[row, col].Value = entity.WorkGroupID;
col++;
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.Image;
col++;
worksheet.Cells[row, col].Value = entity.Url;
col++;
worksheet.Cells[row, col].Value = entity.SortOrder;
col++;
worksheet.Cells[row, col].Value = entity.IsEnabled;
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

	public List<Function>GetByWorkGroupID(int WorkGroupID)
{
   return _objectProxy.All().Where(c => c.WorkGroupID == WorkGroupID).ToList();
}
	
    }
}

