using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Library.CommonServices.Utils
{
    public enum ErrorHandling
    {
        ThrowException,
        DoNotThrowException
    }

    public static class DataHelper
    {
        #region Get...

        public static string GetString(object obj)
        {
            if (obj == null || obj is DBNull)
                return String.Empty;
            return obj.ToString();
        }


        public static string GetString(object obj, string defaultValue)
        {
            if (obj == null || obj is DBNull)
                return defaultValue;
            return obj.ToString();
        }


        public static char GetChar(object obj)
        {
            if (obj == null || obj is DBNull || obj.ToString() == "")
                return char.MinValue;
            return Convert.ToChar(obj);
        }

        public static double GetDouble(object obj)
        {
            if (obj == null || obj is DBNull)
                return 0.0;
            return Convert.ToDouble(obj);
        }

        public static decimal GetDecimal(object obj)
        {
            if (obj == null || obj is DBNull)
                return 0;
            return Convert.ToDecimal(obj);
        }

        public static decimal? GetNullableDecimal(object obj)
        {
            if (obj == null || obj is DBNull || obj.ToString() == "")
                return null;
            return Convert.ToDecimal(obj);
        }

        public static double? GetNullableDouble(object obj)
        {
            if (obj == null || obj is DBNull || obj.ToString() == "")
                return null;
            return Convert.ToDouble(obj);
        }

        public static decimal GetDecimal(object obj, decimal defaultValue)
        {
            if (obj == null || obj is DBNull)
                return defaultValue;

            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return defaultValue;
            }
        }


        public static int GetInt(object obj)
        {
            if (obj == null || obj is DBNull)
                return 0;
            return Convert.ToInt32(obj);
        }

        public static int? GetNullableInt(object obj)
        {
            if (obj == null || obj is DBNull || obj.ToString() == "")
                return null;
            return Convert.ToInt32(obj);
        }

        public static int GetInt(object obj, int defaultValue)
        {
            if (obj == null || obj is DBNull)
                return defaultValue;
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long GetLong(object obj)
        {
            if (obj == null || obj is DBNull)
                return 0;
            return Convert.ToInt64(obj);
        }

        public static DateTime GetDateTime(object obj)
        {
            if (obj == null || obj is DBNull)
                return DateTime.MinValue;
            return Convert.ToDateTime(obj);
        }

        public static DateTime? GetDateTimeNullable(object obj)
        {
            if (obj == null || obj is DBNull)
                return null;
            return Convert.ToDateTime(obj);
        }


        public static DateTime GetDateTime(object obj, DateTime defaultValue)
        {
            if (obj == null || obj is DBNull)
                return defaultValue;
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime ParseDateTime(object obj)
        {
            if (obj == null || obj is DBNull)
                return DateTime.MinValue;
            return DateTime.Parse(obj.ToString());
        }

        public static object ParseDBDateTime(object obj)
        {
            if (obj == null || obj is DBNull)
                return DBNull.Value;
            return DateTime.Parse(obj.ToString());
        }

        public static DateTime GetDateFromDateID(object obj)
        {
            if (obj == null || obj is DBNull)
                return DateTime.MinValue;

            return DateTimeUtils.GetDateFromDateId(GetInt(obj));
        }


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

        public static object SaveToDBNumeric(decimal? d)
        {
            return d.HasValue? (object) d.Value : DBNull.Value;
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

        public static string GetDBDateID(DateTime dt, bool nullable)
        {
            if (nullable && dt == DateTime.MinValue)
                return "null";
            return DateTimeUtils.GetDateIdFromDateTime(dt).ToString();
        }

        #endregion Get...

        #region Read From DataRow

        public static decimal GetFieldDecimal(object dataItem, string column)
        {
            object obj = GetField(dataItem, column);
            return GetDecimal(obj);
        }

        public static string GetFieldString(object dataItem, string column)
        {
            object obj = GetField(dataItem, column);
            return GetString(obj);
        }

        public static string GetTrimmedString(object value, int maxLength)
        {
            string str = Convert.ToString(value);
            if (str.Length > maxLength)
                return string.Format("{0}...", str.Substring(0, maxLength));
            return str;
        }

        public static string GetFieldString(object dataItem, string column, int maxLength)
        {
            object obj = GetField(dataItem, column);

            string str = GetString(obj);

            if (str.Length > maxLength)
                return string.Format("{0}...", str.Substring(0, maxLength));
            return str;
        }

        public static int GetFieldInt(object dataItem, string column)
        {
            object obj = GetField(dataItem, column);
            return GetInt(obj);
        }

        public static DateTime GetFieldDateTime(object dataItem, string column)
        {
            object obj = GetField(dataItem, column);
            return GetDateTime(obj);
        }

        public static object GetField(object dataItem, string column)
        {
            var drv = dataItem as DataRowView;
            if (drv != null)
                return drv[column];
            var dr = dataItem as DataRow;
            if (dr != null)
                return dr[column];
            return DBNull.Value;
        }

        #endregion Read From DataRow

        #region ToString

        public static string DecimalToString(decimal data, string format, string defaultValue)
        {
            if (data == 0)
                return defaultValue;
            return data.ToString(format);
        }


        public static string DateToString(DateTime data, string format, string defaultValue)
        {
            if (data == DateTime.MinValue)
                return defaultValue;
            return data.ToString(format);
        }

        #endregion ToString


        private static object InternalConvertTo(Type type, object input)
        {
            //If our target type is ENUM, we will try to parse the input value
            //to the target Enum value.
            if (type.IsEnum)
                return EnumHelper.Parse(type, input.ToString());

            //If property type is NULLABLE, we will need to convert the DB value
            //to underlying type of the NULLABLE type, otherwise straight up conversion.
            //E.g. our property is of type Nullable<int>
            //In that case, we can not pass typeof(Nullable<int>) to Convert.ChangeType method.
            //We need to pass the typeof(int) to Convert.ChangeType method.
            var targetType = IsNullableType(type)
                                 ? Nullable.GetUnderlyingType(type)
                                 : type;

            return Convert.IsDBNull(input) ? null : Convert.ChangeType(input, targetType);
        }

        public static object ConvertTo(Type type, object input, ErrorHandling errorOption)
        {
            try
            {
                return InternalConvertTo(type, input);
            }
            catch (Exception)
            {
                if (errorOption == ErrorHandling.DoNotThrowException)
                    return null;
                throw;
            }
        }

        public static object ConvertTo(Type type, object input)
        {
            return ConvertTo(type, input, ErrorHandling.ThrowException);
        }

        public static T ConvertTo<T>(object input, ErrorHandling errorOption)
        {
            var output = ConvertTo(typeof(T), input, errorOption);
            if (errorOption == ErrorHandling.DoNotThrowException && output == null)
                return default(T);

            return (T)output;
        }

        public static T ConvertTo<T>(object input)
        {
            return ConvertTo<T>(input, ErrorHandling.DoNotThrowException);
        }


        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
                                                 GetGenericTypeDefinition().Equals
                                                 (typeof(Nullable<>)));
        }

        #region GetAppSettings

        public static int GetIntAppSettings(string settingName, int defaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[settingName];

            if (!string.IsNullOrEmpty(sValue))
            {
                int value;
                if (int.TryParse(sValue.Trim(), out value))
                    return value;
                return defaultValue;
            }
            return defaultValue;
        }

        public static bool GetBoolAppSettings(string settingName, bool defaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[settingName];

            if (!string.IsNullOrEmpty(sValue))
            {
                bool value;
                if (bool.TryParse(sValue.Trim(), out value))
                    return value;
                return defaultValue;
            }
            return defaultValue;
        }

        public static decimal GetDecimalAppSettings(string settingName, decimal defaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[settingName];

            if (!string.IsNullOrEmpty(sValue))
            {
                decimal value;
                if (decimal.TryParse(sValue.Trim(), out value))
                    return value;
                return defaultValue;
            }
            return defaultValue;
        }

        public static string GetAppSettings(string settingName, string defaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[settingName];

            if (!string.IsNullOrEmpty(sValue))
                return sValue;
            return defaultValue;
        }

        #endregion GetAppSettings

        public static Dictionary<string, string> ParseDBConnectionString(string dbConnectionString)
        {
            var dbConnectionStringParams = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(dbConnectionString))
            {
                string[] dbConnectionValues = dbConnectionString.Split(';');
                foreach (string dbConnectionValue in dbConnectionValues)
                {
                    string[] dbConnectionValuePair = dbConnectionValue.Trim().Split('=');
                    if (dbConnectionValuePair.Length == 2)
                        dbConnectionStringParams[dbConnectionValuePair[0]] = dbConnectionValuePair[1];
                    else if (dbConnectionValuePair.Length == 1)
                        dbConnectionStringParams[dbConnectionValuePair[0]] = "";
                }
            }

            return dbConnectionStringParams;
        }
    }
}