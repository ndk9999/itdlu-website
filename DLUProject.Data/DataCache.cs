using System;
using System.Web;
using System.Collections.Generic;

namespace DLUProject.Data
{
    public class DataCache
    {
        private static string GenerateKey<T>()
        {
            return typeof(T).Name + "_Cache_Key";
        }
        public static List<T> Cache<T>(Func<List<T>> GetData)
        {
            return Cache<T>(GenerateKey<T>(), GetData);
        }
        public static List<T> Cache<T>(string cacheKey, Func<List<T>> GetData)
        {
            var data = (List<T>)GetCache(cacheKey);
            if (data == null)
            {
                data = GetData();
                data.TrimExcess();
                SetCache(cacheKey, data, DateTime.Now.AddHours(1));
            }
            return data;
        }
        public static void RemoveCache<T>()
        {
            RemoveCache(GenerateKey<T>());
        }
        public static T GetCache<T>(string cacheKey)
        {
            return (T)HttpRuntime.Cache[cacheKey];
        }
        public static object GetCache(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey];
        }
        public static void RemoveCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            if (!Convert.ToBoolean(objCache[cacheKey] == null))
                objCache.Remove(cacheKey);
        }
        public static void SetCache(string cacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(cacheKey, objObject);
        }
        public static void SetCache(string cacheKey, object objObject, DateTime AbsoluteExpiration)
        {
            HttpRuntime.Cache.Insert(cacheKey, objObject, null, AbsoluteExpiration, TimeSpan.Zero);
        }
        public static void SetCache(string cacheKey, object objObject, int SlidingExpiration)
        {
            HttpRuntime.Cache.Insert(cacheKey, objObject, null, DateTime.MaxValue, TimeSpan.FromSeconds((double)SlidingExpiration));
        }
        public static void SetCache(string cacheKey, object objObject, System.Web.Caching.CacheDependency objDependency)
        {
            HttpRuntime.Cache.Insert(cacheKey, objObject, objDependency);
        }
    }
}
