using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;

namespace ColorLife.Core.FileManager
{
    public static class SortExtensions
    {
        public static void Sort<T>(this List<T> list, string sortExpression)
        {
            string[] sortExpressions = sortExpression.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            List<GenericComparer> comparers = new List<GenericComparer>();

            foreach (string sortExpress in sortExpressions)
            {
                string sortProperty = sortExpress.Trim().Split(' ')[0].Trim();
                string sortDirection = sortExpress.Trim().Split(' ')[1].Trim();

                Type type = typeof(T);
                PropertyInfo PropertyInfo = type.GetProperty(sortProperty);
                if (PropertyInfo == null)
                {
                    PropertyInfo[] props = type.GetProperties();
                    foreach (PropertyInfo info in props)
                    {
                        if (info.Name.ToString().ToLower() == sortProperty.ToLower())
                        {
                            PropertyInfo = info;
                            break;
                        }
                    }
                    if (PropertyInfo == null)
                    {
                        throw new Exception(String.Format("{0} is not a valid property of type: \"{1}\"", sortProperty, type.Name));
                    }
                }

                SortDirection SortDirection = SortDirection.Ascending;
                if (sortDirection.ToLower() == "asc" || sortDirection.ToLower() == "ascending")
                {
                    SortDirection = SortDirection.Ascending;
                }
                else if (sortDirection.ToLower() == "desc" || sortDirection.ToLower() == "descending")
                {
                    SortDirection = SortDirection.Descending;
                }
                else
                {
                    throw new Exception("Valid SortDirections are: asc, ascending, desc and descending");
                }

                comparers.Add(new GenericComparer { SortDirection = SortDirection, PropertyInfo = PropertyInfo, Comparers = comparers });
            }
            list.Sort(comparers[0].Compare);
        }



    }

    public class GenericComparer
    {
        public List<GenericComparer> Comparers { get; set; }
        int _level;

        public SortDirection SortDirection { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public int Compare<T>(T t1, T t2)
        {
            int _ret;

            if (_level >= Comparers.Count)
                return 0;

            object _t1Value = Comparers[_level].PropertyInfo.GetValue(t1, null);
            object _t2Value = Comparers[_level].PropertyInfo.GetValue(t2, null);

            if (t1 == null || _t1Value == null)
            {
                if (t2 == null || _t2Value == null)
                {
                    _ret = 0;
                }
                else
                {
                    _ret = -1;
                }
            }
            else
            {
                if (t2 == null || _t2Value == null)
                {
                    _ret = 1;
                }
                else
                {
                    _ret = ((IComparable)_t1Value).CompareTo(((IComparable)_t2Value));
                }
            }
            if (_ret == 0)
            {
                _level += 1;
                _ret = Compare(t1, t2);
                _level -= 1;
            }
            else
            {
                if (Comparers[_level].SortDirection == SortDirection.Descending)
                {
                    _ret *= -1;
                }
            }
            return _ret;
        }
    }
}
