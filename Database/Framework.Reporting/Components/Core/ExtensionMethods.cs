using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Drawing;

namespace Library.CommonServices.Utils
{
    public static class ExtensionMethods
    {
        public static void SetPrimaryKey(this DataTable inputTable, IEnumerable<string> columnList)
        {
            if (inputTable == null)
                throw new ArgumentNullException("inputTable");

            var primaryKeyColumnList = new List<DataColumn>();
            foreach (var column in columnList)
            {
                var dataColumn = inputTable.Columns[column];

                if (dataColumn == null)
                    throw new InvalidOperationException(string.Format("Can not find column {0} in input table.", column));

                primaryKeyColumnList.Add(dataColumn);
            }

            inputTable.PrimaryKey = primaryKeyColumnList.ToArray();
        }

        public static string ToCsv<T>(this IEnumerable<T> list)
        {
            return Join(list, ", ");
        }

        public static string Join<T>(this IEnumerable<T> list, string separator)
        {
            if (list == null)
                return String.Empty;

            var localList = list.ToList();
            var output = new string[localList.Count];

            var index = 0;
            foreach (var item in localList)
            {
                output[index] = item.ToString();
                index++;
            }

            return string.Join(separator, output);
        }

        public static int GetDateId(this DateTime input)
        {
            return DateTimeUtils.GetDateIdFromDateTime(input);
        }

        public static DateTime GetDate(this int dateId)
        {
            return DateTimeUtils.GetDateFromDateId(dateId);
        }

        

        /// <summary>
        /// Removes the last character from the supplied string.
        /// </summary>
        public static string RemoveLastChar(this string source)
        {
            return source.Remove(source.Length - 1);
        }

        /// <summary>
        /// If the object has an Item property with a string index it returns the value at the given index
        /// </summary>
        public static object GetItemValue(this object source, string index)
        {
            PropertyInfo pi = source.GetType().GetProperty("Item", typeof(object), new[] { typeof(string) });
            return pi.GetValue(source, new[] { index });
        }

        /// <summary>
        /// If the object has an Item property with a int index it returns the value at the given index
        /// </summary>
        public static object GetItemValue(this object source, int index)
        {
            PropertyInfo pi = source.GetType().GetProperty("Item", typeof(object), new[] { typeof(string) });
            return pi.GetValue(source, new object[] { index });
        }

        /// <summary>
        /// If the object has an Item property with a string index it sets the value at the given index
        /// </summary>
        public static void SetItemValue(this object source, string index, object value)
        {
            PropertyInfo pi = source.GetType().GetProperty("Item", typeof(object), new[] { typeof(string) });
            pi.SetValue(source, value, new[] { index });
        }

        /// <summary>
        /// If the object has an Item property with a int index it sets the value at the given index
        /// </summary>
        public static void SetItemValue(this object source, int index, object value)
        {
            PropertyInfo pi = source.GetType().GetProperty("Item", typeof(object), new[] { typeof(string) });
            pi.SetValue(source, value, new object[] { index });
        }


        /// <summary>
        /// Surrounds the supplied string with double quotes.
        /// For example, if you pass c:\\Documents And Settings\\ it will return "c:\\Documents And Settings\\".
        /// </summary>
        public static string EncloseInDoubleQuotes(this string source)
        {
            return String.Format("\"{0}\"", source);
        }

        /// <summary>
        /// Surrounds the supplied string with single quotes.
        /// For example, if you pass hello it will return 'hello'.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncloseInSingleQuotes(this string source)
        {
            return String.Format("'{0}'", source);
        }

        /// <summary>
        /// Formats the date as a SQL literal by enclosing it in #.
        /// </summary>
        public static string ToSqlDateLiteral(this DateTime date)
        {
            return String.Format("#{0}#", date.ToString("yyyy-MM-dd hh:mm:ss"));
        }

        /// <summary>
        /// Pretty prints a data table's schema.
        /// </summary>
        public static void PrintSchema(this DataTable dataTable)
        {
            int colCount = dataTable.Columns.Count;
            StringBuilder output = new StringBuilder(colCount);
            for (var i = 0; i < colCount; i++)
            {
                DataColumn column = dataTable.Columns[i];
                output.AppendFormat("[{0}] {1} ({2} {3}){4}",
                    column.Ordinal,
                    column.ColumnName,
                    column.DataType,
                    column.MaxLength,
                    Environment.NewLine);
            }
            Console.WriteLine(output.ToString());
        }

        /// <summary>
        /// Pretty prints a dictionary.
        /// </summary>
        public static void PrettyPrint<K, V>(this IDictionary<K, V> dict)
        {
            StringBuilder output = new StringBuilder(dict.Keys.Count);
            foreach (var item in dict)
            {
                output.AppendFormat("{0}: {1}{2}", item.Key, item.Value, Environment.NewLine);
            }
            Console.WriteLine(output.ToString());
        }

        /// <summary>
        /// Uses reflection to obtain the Description attribute of any enum.
        /// </summary>
        public static string Description(this Enum enumObj)
        {
            FieldInfo fi = enumObj.GetType().GetField(enumObj.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            return enumObj.ToString();
        }

        /// <summary>
        /// Returns the specified number of elements from the source.
        /// </summary>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Converts an enumerable to an array of objects.
        /// </summary>
        public static object[] ToObjectArray<T>(this IEnumerable<T> source)
        {
            List<object> li = new List<object>();
            foreach (var item in source)
                li.Add(item);
            return li.ToArray();
        }

        /// <summary>
        /// Writes a DataTable to a CSV.
        /// </summary>
        public static void ToCsv(this DataTable dt, string filePath, bool append, bool printHeaders)
        {
            FileUtils.WriteCSVFile(dt, filePath, append, printHeaders);
        }

        public static decimal Min(this decimal? val1, decimal? val2)
        {
            if (val1.HasValue && val2.HasValue)
                return Math.Min(val1.Value, val2.Value);
            if (val1.HasValue)
                return val1.Value;
            if (val2.HasValue)
                return val2.Value;
            return decimal.MaxValue;
        }

        public static decimal Max(this decimal? val1, decimal? val2)
        {
            if (val1.HasValue && val2.HasValue)
                return Math.Max(val1.Value, val2.Value);
            if (val1.HasValue)
                return val1.Value;
            if (val2.HasValue)
                return val2.Value;
            return decimal.MinValue;
        }

        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue ret;
            
            if (!dictionary.TryGetValue(key, out ret))
            {
                ret = new TValue();
                dictionary[key] = ret;
            }

            return ret;
        }

        public static byte[] ToByteArray(this string val)
        {
            var encoding=new ASCIIEncoding();
            return encoding.GetBytes(val);
        }

        public static byte[] ToByteArray(this Bitmap bitmap,ImageFormat format)
        {
            var ms = new MemoryStream();
            bitmap.Save(ms, format);
            return ms.GetBuffer();
        }
    }
}