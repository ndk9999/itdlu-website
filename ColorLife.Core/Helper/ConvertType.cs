using System;
using System.Globalization;

namespace ColorLife.Core.Helper
{
    public static class ConvertType
    {
        public static int ToInt(object obj, int defaultValue)
        {
            try { return int.Parse(obj.ToString()); }
            catch { return defaultValue; }
        }
        public static int ToInt(this object obj)
        {
            return ToInt(obj, -1);
        }
        public static double ToDouble(object obj, double defaultValue)
        {
            try { return double.Parse(obj.ToString()); }
            catch { return defaultValue; }
        }
        public static double ToDouble(this object obj)
        {
            return ToDouble(obj, -1);
        }
        public static float ToFloat(object obj, float defaultValue)
        {
            try { return float.Parse(obj.ToString()); }
            catch { return defaultValue; }
        }
        public static float ToFloat(this object obj)
        {
            return ToFloat(obj, -1);
        }
        public static short ToShort(object obj, short defaultValue)
        {
            try { return short.Parse(obj.ToString()); }
            catch { return defaultValue; }
        }
        public static short ToShort(this object obj)
        {
            return ToShort(obj, -1);
        }
        public static long ToLong(object obj, long defaultValue)
        {
            try { return long.Parse(obj.ToString()); }
            catch { return defaultValue; }
        }
        public static long ToLong(this object obj)
        {
            return ToLong(obj, -1);
        }

        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {           
            try
            {
                return Convert.ToDateTime(obj.ToString());
            }
            catch { return defaultValue; }
        }
        public static DateTime ToDateTime(this object obj)
        {
            return ToDateTime(obj, DateTime.MinValue);
        }
        public static DateTime ToDateTime(this object obj, string culture)
        {
            try
            {
                CultureInfo cultureX = new CultureInfo(culture);
                return Convert.ToDateTime(obj.ToString(), cultureX);
            }
            catch
            {
                return ToDateTime(obj, DateTime.MinValue);
            }
        }
        public static bool ToBool(object obj, bool defaultValue)
        {
            try { return Convert.ToBoolean(obj.ToString()); }
            catch { return defaultValue; }
        }
        public static bool ToBool(this object obj)
        {
            return ToBool(obj, false);
        }
        public static decimal ToDecimal(object obj, decimal defaultValue)
        {
            try { return Convert.ToDecimal(obj.ToString()); }
            catch { return defaultValue; }
        }
        public static decimal ToDecimal(this object obj)
        {
            return ToDecimal(obj, 0);
        }
        public static T ToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        public static T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
