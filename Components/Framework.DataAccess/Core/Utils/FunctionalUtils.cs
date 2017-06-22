using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.CommonServices.BusinessDomain.Utils
{
    public static class FunctionalUtils
    {
        /// <summary>
        /// Extension method which adds ForEach construct to IEnumerables.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in items)
                action(item);
        }

        /// <summary>
        /// Creates a comma separated string from an IEnumerable collection
        /// </summary>        
        public static string ToCommaSeparatedString<T>(this IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            StringBuilder result = new StringBuilder();

            bool isFirstItem = true; 
            foreach (var item in items)
            {
                if (!isFirstItem)
                {
                    result.Append(", ");                    
                }
                result.Append(item);
                isFirstItem = false; 
            }

            return result.ToString();             
        }

        public static string EscapeMailTo(this string str)
        {
            if (str != null)
                return str.Replace("%", "%25");
            return str;

        }


        public new static bool Equals(object obj1, object obj2)
        {
            object dbNormalizedObj1 = GetDbNormalizedValue(obj1);
            object dbNormalizedObj2 = GetDbNormalizedValue(obj2);
            if (dbNormalizedObj1 == null && dbNormalizedObj2 == null)
                return true;

            if (dbNormalizedObj1 != null && dbNormalizedObj2 == null)
                return false;

            if (dbNormalizedObj1 == null && dbNormalizedObj2 != null)
                return false;

            return dbNormalizedObj1.Equals(dbNormalizedObj2);
        }

        public static object GetDbNormalizedValue(object obj1)
        {
            return obj1 == null || obj1 == DBNull.Value ? null : obj1; 
        }
    }
}
