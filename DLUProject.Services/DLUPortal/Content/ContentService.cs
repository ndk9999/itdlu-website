/* FileName: ContentService.cs
Project Name: DLUProject
Date Created: 18/11/2014 10:53:56 AM
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
    /// Represents a ContentService
    /// </summary>
    public class ContentService : IServices<Content>
    {
		private  IRepository<Content> _objectProxy;      
        public ContentService(IRepository<Content> proxy)
        {
            this._objectProxy = proxy;
        }      
		public IQueryable<Content> Table {  get { return _objectProxy.Table; }}
        
		private string cacheKey = "Content_Cache_Key";
        public List<Content> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Content>;
            if (cache == null)
            {
                var items = _objectProxy.Table.Select(c => new Content
                {
                    SortOrder = c.SortOrder,
                    ContentID = c.ContentID,
                    MetaDescription = c.MetaDescription,
                    CategoryID = c.CategoryID,
                    DatePublished = c.DatePublished,
                    State = c.State,
                    Hits = c.Hits,
                    IsPublished = c.IsPublished,
                    Image = c.Image,
                    DateCreated = c.DateCreated,
                    DisplayFlags = c.DisplayFlags,
                    Category = c.Category,
                    CheckoutBy = c.CheckoutBy,
                    Alias = c.Alias,
                    ContentType = c.ContentType,
                    CreatedById = c.CreatedById,
                    DateCheckout = c.DateCheckout,
                    DateModified = c.DateModified,
                    Description = c.Description,
                    FullText = c.FullText,
                    IsCommentEnable = c.IsCommentEnable,
                    IsDeleted = c.IsDeleted,
                    MetaKeywords = c.MetaKeywords,
                    MetaTitle = c.MetaTitle,
                    ModifiedBy = c.ModifiedBy,
                    Name = c.Name,
                    Slug = c.Slug,
                    Author = c.Author,
                    Source = c.Source,
                    DepartmentID = c.DepartmentID
                }).OrderByDescending(c => c.ContentID).ThenBy(c => c.SortOrder).ToList();
                DataCache.SetCache(cacheKey, items, DateTime.Now.AddDays(1));
                return items;
            }
            return cache;
        }

        public Content Get(object id)
        {
            return _objectProxy.All().SingleOrDefault(c => c.ContentID.Equals(id));
        }
		public Content Get(Expression<Func<Content, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Content entity)
		{			 			
             int kq =  _objectProxy.Insert(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;
		}	
		public int Insert2(Content entity)
		{		
			int kq =  _objectProxy.Insert2(entity);            			
			 DataCache.RemoveCache(cacheKey);
			 return kq;        			
		}		
		public int Insert(IEnumerable<Content>items)
		{			
			 
            int kq = _objectProxy.Insert(items);    
			DataCache.RemoveCache(cacheKey);
			 return kq;    			
		}
		public int Update(Content entity)
		{
			 
			int kq= _objectProxy.Update(entity);
			DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Content entity)
		{			
			 
           int kq = _objectProxy.Delete(entity);
		   DataCache.RemoveCache(cacheKey);
			 return kq;    
		}
		public int Delete(Expression<Func<Content, bool>> expression)
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
		public int Delete(IEnumerable<Content>items)
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
                   "ContentID",
"CategoryID",
"Name",
"Alias",
"Slug",
"Description",
"MetaTitle",
"MetaDescription",
"MetaKeywords",
"FullText",
"Image",
"CreatedById",
"ModifiedBy",
"Hits",
"IsPublished",
"IsCommentEnable",
"State",
"DisplayFlags",
"CheckoutBy",
"DateCreated",
"DateModified",
"DateCheckout",
"DatePublished",
"ContentType",
"SortOrder",
"IsDeleted",

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
						
                    var ContentID = worksheet.Cells[iRow, GetColumnIndex(properties, "ContentID")].Value.ToInt();
var CategoryID = worksheet.Cells[iRow, GetColumnIndex(properties, "CategoryID")].Value.ToInt();
var Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value ?? string.Empty;
var Alias = worksheet.Cells[iRow, GetColumnIndex(properties, "Alias")].Value ?? string.Empty;
var Slug = worksheet.Cells[iRow, GetColumnIndex(properties, "Slug")].Value ?? string.Empty;
var Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value ?? string.Empty;
var MetaTitle = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaTitle")].Value ?? string.Empty;
var MetaDescription = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaDescription")].Value ?? string.Empty;
var MetaKeywords = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaKeywords")].Value ?? string.Empty;
var FullText = worksheet.Cells[iRow, GetColumnIndex(properties, "FullText")].Value ?? string.Empty;
var Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value ?? string.Empty;
var CreatedById = worksheet.Cells[iRow, GetColumnIndex(properties, "CreatedById")].Value.ToInt();
var ModifiedBy = worksheet.Cells[iRow, GetColumnIndex(properties, "ModifiedBy")].Value ?? string.Empty;
var Hits = worksheet.Cells[iRow, GetColumnIndex(properties, "Hits")].Value.ToInt();
var IsPublished = worksheet.Cells[iRow, GetColumnIndex(properties, "IsPublished")].Value.ToBool();
var IsCommentEnable = worksheet.Cells[iRow, GetColumnIndex(properties, "IsCommentEnable")].Value.ToBool();
var State = worksheet.Cells[iRow, GetColumnIndex(properties, "State")].Value.ToInt();
var DisplayFlags = worksheet.Cells[iRow, GetColumnIndex(properties, "DisplayFlags")].Value.ToInt();
var CheckoutBy = worksheet.Cells[iRow, GetColumnIndex(properties, "CheckoutBy")].Value ?? string.Empty;
var DateCreated = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCreated")].Value.ToDateTime();
var DateModified = worksheet.Cells[iRow, GetColumnIndex(properties, "DateModified")].Value.ToDateTime();
var DateCheckout = worksheet.Cells[iRow, GetColumnIndex(properties, "DateCheckout")].Value.ToDateTime();
var DatePublished = worksheet.Cells[iRow, GetColumnIndex(properties, "DatePublished")].Value.ToDateTime();
var ContentType = worksheet.Cells[iRow, GetColumnIndex(properties, "ContentType")].Value ?? string.Empty;
var SortOrder = worksheet.Cells[iRow, GetColumnIndex(properties, "SortOrder")].Value.ToInt();
var IsDeleted = worksheet.Cells[iRow, GetColumnIndex(properties, "IsDeleted")].Value.ToBool();


                    var entity = new Content()
                    {
                        ContentID = ContentID,
CategoryID = CategoryID,
Name = Name.ToString(),
Alias = Alias.ToString(),
Slug = Slug.ToString(),
Description = Description.ToString(),
MetaTitle = MetaTitle.ToString(),
MetaDescription = MetaDescription.ToString(),
MetaKeywords = MetaKeywords.ToString(),
FullText = FullText.ToString(),
Image = Image.ToString(),
CreatedById = CreatedById,
ModifiedBy = ModifiedBy.ToString(),
Hits = Hits,
IsPublished = IsPublished,
IsCommentEnable = IsCommentEnable,
State = State,
DisplayFlags = DisplayFlags,
CheckoutBy = CheckoutBy.ToString(),
DateCreated = DateCreated,
DateModified = DateModified,
DateCheckout = DateCheckout,
DatePublished = DatePublished,
ContentType = ContentType.ToString(),
SortOrder = SortOrder,
IsDeleted = IsDeleted,

                    };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
		}
		public string ExportToXml(List<Content> items)
		{
			var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Contents");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("Content");
				xmlWriter.WriteElementString("ContentID", null, entity.ContentID.ToString());
xmlWriter.WriteElementString("CategoryID", null, entity.CategoryID.ToString());
xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
xmlWriter.WriteElementString("Alias", null, entity.Alias.ToString());
xmlWriter.WriteElementString("Slug", null, entity.Slug.ToString());
xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
xmlWriter.WriteElementString("MetaTitle", null, entity.MetaTitle.ToString());
xmlWriter.WriteElementString("MetaDescription", null, entity.MetaDescription.ToString());
xmlWriter.WriteElementString("MetaKeywords", null, entity.MetaKeywords.ToString());
xmlWriter.WriteElementString("FullText", null, entity.FullText.ToString());
xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
xmlWriter.WriteElementString("CreatedById", null, entity.CreatedById.ToString());
xmlWriter.WriteElementString("ModifiedBy", null, entity.ModifiedBy.ToString());
xmlWriter.WriteElementString("Hits", null, entity.Hits.ToString());
xmlWriter.WriteElementString("IsPublished", null, entity.IsPublished.ToString());
xmlWriter.WriteElementString("IsCommentEnable", null, entity.IsCommentEnable.ToString());
xmlWriter.WriteElementString("State", null, entity.State.ToString());
xmlWriter.WriteElementString("DisplayFlags", null, entity.DisplayFlags.ToString());
xmlWriter.WriteElementString("CheckoutBy", null, entity.CheckoutBy.ToString());
xmlWriter.WriteElementString("DateCreated", null, entity.DateCreated.ToString());
xmlWriter.WriteElementString("DateModified", null, entity.DateModified.ToString());
xmlWriter.WriteElementString("DateCheckout", null, entity.DateCheckout.ToString());
xmlWriter.WriteElementString("DatePublished", null, entity.DatePublished.ToString());
xmlWriter.WriteElementString("ContentType", null, entity.ContentType.ToString());
xmlWriter.WriteElementString("SortOrder", null, entity.SortOrder.ToString());
xmlWriter.WriteElementString("IsDeleted", null, entity.IsDeleted.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
		}
		public void ExportToXlsx(Stream stream, List<Content> items)
        {			
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Content");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "ContentID",
"CategoryID",
"Name",
"Alias",
"Slug",
"Description",
"MetaTitle",
"MetaDescription",
"MetaKeywords",
"FullText",
"Image",
"CreatedById",
"ModifiedBy",
"Hits",
"IsPublished",
"IsCommentEnable",
"State",
"DisplayFlags",
"CheckoutBy",
"DateCreated",
"DateModified",
"DateCheckout",
"DatePublished",
"ContentType",
"SortOrder",
"IsDeleted",

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
					worksheet.Cells[row, col].Value = entity.ContentID;
col++;
worksheet.Cells[row, col].Value = entity.CategoryID;
col++;
worksheet.Cells[row, col].Value = entity.Name;
col++;
worksheet.Cells[row, col].Value = entity.Alias;
col++;
worksheet.Cells[row, col].Value = entity.Slug;
col++;
worksheet.Cells[row, col].Value = entity.Description;
col++;
worksheet.Cells[row, col].Value = entity.MetaTitle;
col++;
worksheet.Cells[row, col].Value = entity.MetaDescription;
col++;
worksheet.Cells[row, col].Value = entity.MetaKeywords;
col++;
worksheet.Cells[row, col].Value = entity.FullText;
col++;
worksheet.Cells[row, col].Value = entity.Image;
col++;
worksheet.Cells[row, col].Value = entity.CreatedById;
col++;
worksheet.Cells[row, col].Value = entity.ModifiedBy;
col++;
worksheet.Cells[row, col].Value = entity.Hits;
col++;
worksheet.Cells[row, col].Value = entity.IsPublished;
col++;
worksheet.Cells[row, col].Value = entity.IsCommentEnable;
col++;
worksheet.Cells[row, col].Value = entity.State;
col++;
worksheet.Cells[row, col].Value = entity.DisplayFlags;
col++;
worksheet.Cells[row, col].Value = entity.CheckoutBy;
col++;
worksheet.Cells[row, col].Value = entity.DateCreated;
col++;
worksheet.Cells[row, col].Value = entity.DateModified;
col++;
worksheet.Cells[row, col].Value = entity.DateCheckout;
col++;
worksheet.Cells[row, col].Value = entity.DatePublished;
col++;
worksheet.Cells[row, col].Value = entity.ContentType;
col++;
worksheet.Cells[row, col].Value = entity.SortOrder;
col++;
worksheet.Cells[row, col].Value = entity.IsDeleted;
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

