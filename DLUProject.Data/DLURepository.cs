/* FileName: QuanLyKTXRepository.cs
Project Name: DLUProject
Date Created: 12/5/2014 1:15:38 PM
Description: Auto-generated
Version: 1.0.0.03
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DynamicExpression = System.Linq.Dynamic.DynamicExpression;
using BLToolkit.Mapping;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using BLToolkit.Data.Linq;
 
namespace DLUProject.Data
{
	/// <summary>
    /// Represents a QuanLyKTXRepository
    /// </summary>
    public partial class DLURepository<T> : IRepository<T> where T : class
    {
        private DLUDataContext _dbContext;
        public DLURepository(DLUDataContext dataContext)
        {
            _dbContext = dataContext;
        }
        
        public IQueryable<T> Table
        {            
           
            get { return _dbContext.GetTable<T>(); }
        }
        
        public List<T> All()
        {            
            return Table.ToList();
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.GetTable<T>().Where(predicate);
        }
        public T Get(object id)
        {
            return Table.SingleOrDefault<T>(GenerateExpression(id));
        }
        
        public T Get(Expression<Func<T, bool>> expression)
        {
            return Table.SingleOrDefault<T>(expression);
        }

        public int Insert(T entity)
        {
            object rs = _dbContext.InsertWithIdentity<T>(entity);
            return int.Parse(rs.ToString());
        }
        public int Insert2(T entity)
        {
            return _dbContext.Insert<T>(entity);
        }
        public int Insert(IEnumerable<T> items)
        {
            return _dbContext.InsertOrReplace(items);
        }

        public int Update(T entity)
        {
            return _dbContext.Update<T>(entity);
        }

        public int Delete(T entity)
        {
            return _dbContext.Delete<T>(entity);
        }

        public int Delete(object id)
        {
            return Table.Delete<T>(GenerateExpression(id));
        }

        public int Delete(IEnumerable<T> items)
        {
            using (var db = new DbManager())
            {
                return db.Delete<T>(items);
            }
        }

        public int Delete(Expression<Func<T, bool>> expression)
        {
            return Table.Delete<T>(expression);
        }

        public int Delete()
        {
            return Table.Delete();
        }
        //public IQueryable<T> Paging(int pageSize, int pageNumber, out int totalItems)
        //{           
        //    totalItems = All().Count();
        //    var list = All().Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        //    return list.AsQueryable();
        //}
        //public IQueryable<T> Paging(IQueryable<T>items,int pageSize, int pageNumber, out int totalItems)
        //{
        //    totalItems = items.Count();
        //    var list = items.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        //    return list.AsQueryable();
        //}
        protected static Expression<Func<T, bool>> GenerateExpression(params object[] keys)
        {
            MemberMapper[] keyList = typeof(T).GetKeyFieldList<T>();
            if (keyList.Length == 0)
            {
                throw new DataAccessException(string.Format("No primary key field(s) in the type '{0}'.", typeof(T).FullName));
            }
            int n = keys.Length;
            string[] expressions = new string[n];
            object[] convertKeys = new object[n];
            for (int i = 0; i < n; i++)
            {
                MemberMapper mm = keyList[i];
                expressions[i] = string.Format("{0}=@{1}", mm.Name, i);
                convertKeys[i] = keys[i].ChangeTypeTo(mm.Type);
            }
            return DynamicExpression.ParseLambda<T, bool>(string.Join(" and ", expressions), convertKeys);
        }
    }
}


