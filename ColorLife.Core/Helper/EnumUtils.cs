﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ColorLife.Core.Helper
{
    public class EnumUtils<T>
    {
        public static string GetDescription(T enumValue, string defDesc)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return defDesc;
        }

        public static string GetDescription(T enumValue)
        {
            return GetDescription(enumValue, string.Empty);
        }

        public static T FromDescription(string description)
        {
            Type t = typeof(T);
            foreach (FieldInfo fi in t.GetFields())
            {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    foreach (DescriptionAttribute attr in attrs)
                    {
                        if (attr.Description.Equals(description))
                            return (T)fi.GetValue(null);
                    }
                }
            }
            return default(T);
        }
        public static T ToEnum(string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }
        public static T ToEnum(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
            
        }
    }
}