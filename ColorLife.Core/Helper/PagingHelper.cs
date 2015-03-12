using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ColorLife.Core.Helper
{
    public static class PagingHelper<T> where T :  class
    {
        public static IEnumerable<T> Paging(IEnumerable<T> items, int pageSize, int pageNumber, out int totalItems)
        {
            totalItems = items.Count();
            var list = items.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return list.AsQueryable();
        }
        public static IQueryable<T> Paging(IQueryable<T> items, int pageSize, int pageNumber, out int totalItems)
        {
            totalItems = items.Count();
            var list = items.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return list.AsQueryable();
        }
        public static List<T> Paging(List<T> items, int pageSize, int pageNumber, out int totalItems)
        {
            totalItems = items.Count();
            var list = items.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return list.ToList();
        }
    }
}
