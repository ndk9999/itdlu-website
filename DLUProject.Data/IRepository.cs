/* FileName: IRepository.cs
Project Name: DLUProject
Date Created: 13/5/2014 4:24:27 PM
Description: Auto-generated
Version: 1.0.0.0
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

namespace DLUProject.Data
{
    /// <summary>
    /// Represents a IRepository
    /// </summary>
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
         
        List<T> All();
        T Get(object id);       
        T Get(Expression<Func<T, bool>> expression);       
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        int Insert(T entity);
        int Insert2(T entity);
        int Insert(IEnumerable<T> items);
        int Update(T entity);
        int Delete(T entity);
        int Delete(object id);
        int Delete(IEnumerable<T> items);
        int Delete(Expression<Func<T, bool>> expression);
        int Delete();
       // IQueryable<T> Paging(int pageSize, int pageNumber, out int totalItems);
       // IQueryable<T> Paging(IQueryable<T> items, int pageSize, int pageNumber, out int totalItems);
        // #BODY_CLASS_EXTEND#		
    }
}

