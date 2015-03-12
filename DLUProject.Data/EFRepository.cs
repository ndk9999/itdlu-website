
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading;
namespace DLUProject.Data
{
    public class EFRepository<T> : IDisposable,  IRepository<T> where T : class
    {
        DbContext _dbContext;

        public EFRepository(EFDataContext dataContext)
        {
            _dbContext = dataContext;
            if (_dbContext == null)
            {
                LazyInitializer.EnsureInitialized(ref _dbContext);
            }
        }        
        public IQueryable<T> Table
        {
            get { return _dbContext.Set<T>(); }
        }

        public List<T> All()
        {
            return Table.ToList();
        }

        public T Get(object id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                return Table.Where(expression).SingleOrDefault();
            }
            return null;
        }

        public int Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Insert2(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Insert(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;


            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;             
            return _dbContext.SaveChanges();
        }

        public int Delete(object id)
        {
            throw new NotImplementedException();
        }

        public int Delete(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public int Delete(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        //public IQueryable<T> Paging(int pageSize, int pageNumber, out int totalItems)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<T> Paging(IQueryable<T> items, int pageSize, int pageNumber, out int totalItems)
        //{
        //    throw new NotImplementedException();
        //}

        public void Dispose()
        {
            _dbContext.Dispose();
        }


        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
