using System;
using System.Data;
using System.Diagnostics;

namespace Library.CommonServices.Utils
{
    public static class DBUtils
    {
        public static string GetString(object obj)
        {
            if (obj == null || obj is DBNull)
                return String.Empty;
            return obj.ToString();
        }

        public static string GetNullableString(object obj)
        {
            if (obj == null || obj is DBNull)
                return null;
            return obj.ToString();
        }

        public static string GetTrimmedNullableString(object obj)
        {
            if (obj is DBNull || obj == null)
                return null;
            return GetTrimmedString(obj);
        }

        public static string GetTrimmedString(object obj)
        {
            if (obj == null || obj is DBNull)
                return String.Empty;
            return obj.ToString().Trim();
        }

        public static int? GetNullableInt(object obj)
        {
            if (obj == null || obj is DBNull)
                return null;
            return Convert.ToInt32(obj);
        }

        public static double GetDouble(object obj)
        {
            if (obj is DBNull || obj == null || obj.ToString() == String.Empty)
                return 0.0;
            return Convert.ToDouble(obj);
        }

        public static double? GetNullableDouble(object obj)
        {
            if (obj == null || obj is DBNull)
                return null;
            return Convert.ToDouble(obj);
        }

        public static decimal GetDecimal(object obj)
        {
            if (obj is DBNull || obj == null || obj.ToString() == String.Empty)
                return 0;
            return Convert.ToDecimal(obj);
        }

        public static decimal? GetNullableDecimal(object obj)
        {
            if (obj == null || obj is DBNull)
                return null;
            return Convert.ToDecimal(obj);
        }

        public static int GetInt(object obj)
        {
            if (obj is DBNull || obj == null || obj.ToString() == String.Empty)
                return 0;
            return Convert.ToInt32(obj);
        }

        public static long GetLong(object obj)
        {
            if (obj is DBNull || obj == null || obj.ToString() == String.Empty)
                return 0;
            return Convert.ToInt64(obj);
        }

        public static DateTime GetDateTime(object obj)
        {
            if (obj == null || obj is DBNull)
                return DateTime.MinValue;
            return Convert.ToDateTime(obj);
        }

        public static DateTime? GetNullableDateTime(object obj)
        {
            if (obj == null || obj is DBNull)
                return null;
            return Convert.ToDateTime(obj);
        }

        /*
		public static DateTime GetDateFromDateID(object obj)
		{
			if (obj == null || obj is DBNull)
				return DateTime.MinValue;

			return DateTimeUtils.GetDateFromDateId(GetInt(obj));
		}
         * */

        public static bool GetBool(object obj)
        {
            if (obj == null || obj is DBNull)
                return false;
            return Convert.ToInt16(obj) == 0 ? false : true;
        }

        public static string GetDBString(string s, bool nullable)
        {
            if (nullable && s.Trim() == String.Empty)
                return "null";
            s = s.Replace("'", "");
            return "'" + s + "'";
        }

        public static string GetDBNumeric(double d, bool nullable)
        {
            if (nullable && d == 0)
                return "null";
            return d.ToString();
        }

        public static string GetDBNumeric(decimal d, bool nullable)
        {
            if (nullable && d == 0)
                return "null";
            return d.ToString();
        }

        public static string GetDBNumeric(int d, bool nullable)
        {
            if (nullable && d == 0)
                return "null";
            return d.ToString();
        }

        public static string GetDBBool(bool b)
        {
            return (b) ? "1" : "0";
        }

        public static DataTable Pivot(DataTable dt, string keyColumn, string pivotNameColumn, string pivotValueColumn)
        {
            /*
             *  keyColumn - 
             *  pivotNameColumn - column containing values to spread across as column headers
             *	pivotValueColumn - values going under the new columns
             * 
             */

            var tmp = new DataTable();
            DataRow r;
            string LastKey = "//dummy//";
            int i, pValIndex, pNameIndex;
            
            bool FirstRow = true;

            // Add non-pivot columns to the data table:
            pValIndex = dt.Columns[pivotValueColumn].Ordinal;
            pNameIndex = dt.Columns[pivotNameColumn].Ordinal;

            for (i = 0; i <= dt.Columns.Count - 1; i++)
            {
                if (i != pValIndex && i != pNameIndex)
                {
                    //System.Type tp = dt.Columns[i].DataType;
                    tmp.Columns.Add(dt.Columns[i].ColumnName, dt.Columns[i].DataType); // GetType());           
                }
            }

            r = tmp.NewRow();

            // now, fill up the table with the data:
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                // see if we need to start a new row
                if (dt.Rows[x][keyColumn].ToString() != LastKey)
                {
                    // if this isn't the very first row, we need to add the last one to the table
                    if (!FirstRow)
                        tmp.Rows.Add(r);
                    r = tmp.NewRow();
                    FirstRow = false;
                    // Add all non-pivot column values to the new row:
                    for (i = 0; i <= dt.Columns.Count - 3; i++)
                        r[i] = dt.Rows[x][tmp.Columns[i].ColumnName];
                    LastKey = dt.Rows[x][keyColumn].ToString();
                }
                // assign the pivot values to the proper column; add new columns if needed:
                string s = dt.Rows[x][pNameIndex].ToString();
                if (!tmp.Columns.Contains(s))
                    tmp.Columns.Add(s, dt.Columns[pValIndex].DataType);
                r[s] = dt.Rows[x][pValIndex];
            }

            // add that final row to the datatable:
            tmp.Rows.Add(r);

            return tmp;
        }

        public static void DumpDataTable(DataTable dt, int iMaxRows)
        {
            if (iMaxRows == 0)
                iMaxRows = dt.Rows.Count;

            for (int y = 0; y < dt.Columns.Count; y++)
                Console.Write(dt.Columns[y].ColumnName + "\t");
            Console.Write("\r\n");

            for (int x = 0; x < iMaxRows; x++)
            {
                for (int y = 0; y < dt.Columns.Count; y++)
                    Console.Write(dt.Rows[x][y] + "\t");

                Console.Write("\r\n");
            }
        }

        public static void DumpDataTable(DataTable dt)
        {
            DumpDataTable(dt, 0);
        }


        public static Object SetNullDateTime(DateTime DateField)
        {
            object param = DateField;

            if (DateField == default(DateTime))
                param = DBNull.Value;

            return param;
        }

        public static Object SetNullDecimal(Decimal DateField)
        {
            object param = DateField;

            if (DateField == default(Decimal))
                param = DBNull.Value;

            return param;
        }

        public static Object SetNullInt(int DateField)
        {
            object param = DateField;

            if (DateField == default(int))
                param = DBNull.Value;

            return param;
        }
    }
}