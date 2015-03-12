/* FileName: BlogCategoryService.cs
Project Name: ColorBlogProject
Date Created: 28/6/2014 7:16:15 PM
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
using System.Xml;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


using ColorLife.Core.Helper;
using OfficeOpenXml.Style;
using DLUProject.Domain;

using System.Drawing;
using DLUProject.Data;
using BLToolkit.Aspects;


namespace DLUProject.Services
{
    /// <summary>
    /// Represents a BlogCategoryService
    /// </summary>
    public class CategoryService : IServices<Category>
    {
        private IRepository<Category> _objectProxy;
        public CategoryService(IRepository<Category> repository)
        {
            this._objectProxy = repository;
        }
        private string cacheKey = "Category_Cache_Key";
        public IQueryable<Category> Table
        {
            get { return _objectProxy.Table; }
        }
        [Cache(MaxMinutes=1440)]
        public List<Category> All()
        {
            var cache = DataCache.GetCache(cacheKey) as List<Category>;
            if (cache == null)
            {
                var listX = _objectProxy.Table.Select(c => new Category
                {
                    CategoryID = c.CategoryID,
                    Name = c.Name,
                    Alias = c.Alias,
                    Image = c.Image,
                    Description = c.Description,
                    IsPublished = c.IsPublished,
                    MetaDescription = c.MetaDescription,
                    MetaKeywords = c.MetaKeywords,
                    MetaTitle = c.MetaTitle,
                    ParentID = c.ParentID,
                    SortOrder = c.SortOrder,
                    // Aticles = c.Aticles
                }).OrderBy(c => c.SortOrder).ToList();

                DataCache.SetCache(cacheKey, listX, DateTime.Now.AddDays(1));
                return listX;
            }
            return cache;
        }
        public Category Get(object id)
        {
            return _objectProxy.Get(id);
        }
        public Category Get(Expression<Func<Category, bool>> expression)
        {
            return _objectProxy.Get(expression);
        }
        public int Insert(Category entity)
        {
            int kq = _objectProxy.Insert(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Insert2(Category entity)
        {
            int kq = _objectProxy.Insert2(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Insert(IEnumerable<Category> items)
        {
            return _objectProxy.Insert(items);
        }
        public int Update(Category entity)
        {
            int kq = _objectProxy.Update(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(Category entity)
        {
            int kq = _objectProxy.Delete(entity);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(Expression<Func<Category, bool>> expression)
        {
            return _objectProxy.Delete(expression);
        }
        public int Delete(object id)
        {
          
            int kq = _objectProxy.Delete(id);
            DataCache.RemoveCache(cacheKey);
            return kq;
        }
        public int Delete(IEnumerable<Category> items)
        {
            return _objectProxy.Delete(items);
        }
        public int Delete()
        {
            return _objectProxy.Delete();
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
                   "CategoryID",
"Name",
"Alias",
"Image",
"Description",
"ParentID",
"SortOrder",
"IsPublished",
"MetaTitle",
"MetaDescription",
"MetaKeywords",

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

                    int CategoryID = worksheet.Cells[iRow, GetColumnIndex(properties, "CategoryID")].Value.ToInt();
                    string Name = worksheet.Cells[iRow, GetColumnIndex(properties, "Name")].Value.ToString();
                    string Alias = worksheet.Cells[iRow, GetColumnIndex(properties, "Alias")].Value.ToString();
                    string Image = worksheet.Cells[iRow, GetColumnIndex(properties, "Image")].Value.ToString();
                    string Description = worksheet.Cells[iRow, GetColumnIndex(properties, "Description")].Value.ToString();
                    int ParentID = worksheet.Cells[iRow, GetColumnIndex(properties, "ParentID")].Value.ToInt();
                    int SortOrder = worksheet.Cells[iRow, GetColumnIndex(properties, "SortOrder")].Value.ToInt();
                    bool IsPublished = worksheet.Cells[iRow, GetColumnIndex(properties, "IsPublished")].Value.ToBool();
                    string MetaTitle = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaTitle")].Value.ToString();
                    string MetaDescription = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaDescription")].Value.ToString();
                    string MetaKeywords = worksheet.Cells[iRow, GetColumnIndex(properties, "MetaKeywords")].Value.ToString();


                    var entity = new Category()
                                        {
                                            CategoryID = CategoryID,
                                            Name = Name,
                                            Alias = Alias,
                                            Image = Image,
                                            Description = Description,
                                            ParentID = ParentID,
                                            SortOrder = SortOrder,
                                            IsPublished = IsPublished,
                                            MetaTitle = MetaTitle,
                                            MetaDescription = MetaDescription,
                                            MetaKeywords = MetaKeywords,

                                        };

                    _objectProxy.Insert(entity);
                    //next row
                    iRow++;
                }
            }
        }
        public IQueryable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            return _objectProxy.Find(predicate);
        }
        public string ExportToXml(List<Category> items)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("BlogCategorys");
            xmlWriter.WriteAttributeString("Version", "1.0");

            foreach (var entity in items)
            {
                xmlWriter.WriteStartElement("BlogCategory");
                xmlWriter.WriteElementString("CategoryID", null, entity.CategoryID.ToString());
                xmlWriter.WriteElementString("Name", null, entity.Name.ToString());
                xmlWriter.WriteElementString("Alias", null, entity.Alias.ToString());
                xmlWriter.WriteElementString("Image", null, entity.Image.ToString());
                xmlWriter.WriteElementString("Description", null, entity.Description.ToString());
                xmlWriter.WriteElementString("ParentID", null, entity.ParentID.ToString());
                xmlWriter.WriteElementString("SortOrder", null, entity.SortOrder.ToString());
                xmlWriter.WriteElementString("IsPublished", null, entity.IsPublished.ToString());
                xmlWriter.WriteElementString("MetaTitle", null, entity.MetaTitle.ToString());
                xmlWriter.WriteElementString("MetaDescription", null, entity.MetaDescription.ToString());
                xmlWriter.WriteElementString("MetaKeywords", null, entity.MetaKeywords.ToString());

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }
        public void ExportToXlsx(Stream stream, List<Category> items)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("BlogCategory");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "CategoryID",
"Name",
"Alias",
"Image",
"Description",
"ParentID",
"SortOrder",
"IsPublished",
"MetaTitle",
"MetaDescription",
"MetaKeywords",

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
                    worksheet.Cells[row, col].Value = entity.CategoryID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Name;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Alias;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Image;
                    col++;
                    worksheet.Cells[row, col].Value = entity.Description;
                    col++;
                    worksheet.Cells[row, col].Value = entity.ParentID;
                    col++;
                    worksheet.Cells[row, col].Value = entity.SortOrder;
                    col++;
                    worksheet.Cells[row, col].Value = entity.IsPublished;
                    col++;
                    worksheet.Cells[row, col].Value = entity.MetaTitle;
                    col++;
                    worksheet.Cells[row, col].Value = entity.MetaDescription;
                    col++;
                    worksheet.Cells[row, col].Value = entity.MetaKeywords;
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
