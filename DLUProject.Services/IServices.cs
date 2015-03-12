/* FileName: IService.cs
Project Name: DLUProject
Date Created: 16/5/2014 1:53:43 PM
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DLUProject.Domain;
namespace DLUProject.Services
{
	/// <summary>
    /// Represents a IServices
    /// </summary>
    public interface IServices<T> where T:class
    {
        IQueryable<T> Table { get; }
        List<T> All();
		T Get(object id);
		T Get(Expression<Func<T, bool>> expression);
		int Insert(T entity);
		int Insert2(T entity);
		int Insert(IEnumerable<T>list);
		int Update(T entity);		
		int Delete(T entity);
		int Delete(object id);
		int Delete(IEnumerable<T>list);
		int Delete(Expression<Func<T, bool>> expression);
		int Delete();		
    }
}

