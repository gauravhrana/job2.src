using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Configuration;


namespace Framework.CommonServices.BusinessDomain.Utils
{
    #region class DataHelper
    public class DataHelper
    {
        
        // should really be const strings to take advantage of speed / performance
        //Common Column Names
        
        /// <summary>
        /// Private constructor to prevend direct Instantiations
        /// </summary>
        private DataHelper()
        {
        }


        #region Get...
        public static string GetString(object obj)
        {
            if (obj == null || obj is DBNull)
                return String.Empty;
            return obj.ToString();
        }

        public static char GetChar(object obj)
        {
            if (obj == null || obj is DBNull || obj.ToString() == "")
                return char.MinValue;
            return Convert.ToChar(obj);
        }

        public static char GetCharFromANSI(object obj)
        {
            if (obj == null || obj is DBNull || obj.ToString() == "")
                return char.MinValue;
            return Convert.ToChar(obj);
        }

        public static double GetDouble(object obj)
        {
            if (obj is DBNull)
                return 0.0;
            return Convert.ToDouble(obj);
        }

        public static decimal GetDecimal(object obj)
        {
            return GetDecimal(obj, 0);
        }

        public static decimal GetDecimal(object obj, decimal defaultValue)
        {
            if (obj is decimal)
                return (decimal)obj;

            if (obj == null || obj is DBNull)
                return defaultValue;

            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static decimal GetDecimal(object obj, decimal defaultValue, out bool isNewValue)
        {
            decimal newValue = GetDecimal(obj, defaultValue);
            isNewValue = (newValue != defaultValue) ? true : false;
            return newValue;
        }

        public static decimal? GetDecimalNullable(object obj, decimal? defaultValue)
        {
            if (obj is decimal?)
                return (decimal?)obj;


            if (obj == null || obj is DBNull)
                return defaultValue;

            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }



        public static int GetInt(object obj)
        {
            return GetInt(obj, 0);
        }

        public static int GetInt(object obj, int defaultValue)
        {
            if (obj is int)
                return (int)obj;


            if (obj == null || obj is DBNull)
                return defaultValue;

            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static int? GetIntNullable(object obj, int? defaultValue)
        {
            if (obj is int?)
                return (int?)obj;


            if (obj == null || obj is DBNull)
                return defaultValue;

            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static long GetLong(object obj)
        {
            return GetLong(obj, 0);
        }


        public static long GetLong(object obj, long defaultValue)
        {
            if (obj is long)
                return (long)obj;

            if (obj == null || obj is DBNull)
                return defaultValue;

            try
            {
                return Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }


        public static DateTime GetDateTime(object obj)
        {
            return GetDateTime(obj, DateTime.MinValue);
        }


        public static DateTime GetDateTime(object obj, DateTime defaultValue)
        {
            if (obj is DateTime)
                return (DateTime)obj;

            if (obj == null || obj is DBNull)
                return defaultValue;

            try
            {
                return Convert.ToDateTime(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }



        public static DateTime GetDateFromDateID(object obj)
        {
            if (obj is DBNull)
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
            else
            {
                s = s.Replace("'", "");
                return "'" + s + "'";
            }
        }

        public static string GetDBNumeric(double d, bool nullable)
        {
            if (nullable && d == 0)
                return "null";
            else
                return d.ToString();
        }

        public static string GetDBNumeric(decimal d, bool nullable)
        {
            if (nullable && d == 0)
                return "null";
            else
                return d.ToString();
        }

        public static string GetDBNumeric(int d, bool nullable)
        {
            if (nullable && d == 0)
                return "null";
            else
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
            else
                return DateTimeUtils.GetDateIdFromDateTime(dt).ToString();
        }

        public static char? GetNullableChar(object obj)
        {
            if (obj is DBNull || obj.ToString() == "")
                return null;
            return (char?)Convert.ToChar(obj);
        }

        public static double? GetNullableDouble(object obj)
        {
            if (obj is DBNull)
                return null;
            return (double?)Convert.ToDouble(obj);
        }

        public static decimal? GetNullableDecimal(object obj)
        {
            if (obj is DBNull)
                return null;
            return (decimal?)Convert.ToDecimal(obj);
        }

        public static int? GetNullableInt(object obj)
        {
            if (obj is DBNull)
                return null;
            return (int?)Convert.ToInt32(obj);
        }

        public static long? GetNullableLong(object obj)
        {
            if (obj is DBNull)
                return null;
            return (long?)Convert.ToInt64(obj);
        }

        public static DateTime? GetNullableDateTime(object obj)
        {
            if (obj is DBNull)
                return null;
            return Convert.ToDateTime(obj);
        }

        public static bool? GetNullableBool(object obj)
        {
            if (obj is DBNull)
                return null;
            return (bool?)(Convert.ToInt16(obj) == 0 ? false : true);
        }

        public static object GetValueOrDBNull(object val)
        {
            if (val == null)
                return DBNull.Value;
            if (val.GetType() == typeof(string)) return string.IsNullOrEmpty((string)val) ? DBNull.Value : val;

            return val;
        }

        public static object GetValueOrDefault(object val, object dval)
        {
            if (val == null)
                return dval ?? DBNull.Value;
            if (val.GetType() == typeof(string)) return string.IsNullOrEmpty((string)val) ? DBNull.Value : val;

            return val;
        }
        #endregion Get...


        #region GetAppSettings
        public static string GetStringAppSettings(string settingName, string defaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[settingName];

            if (sValue != null && sValue.Length > 0)
            {
                return sValue;
            }
            else
                return defaultValue;
        }

        public static int GetIntAppSettings(string settingName, int defaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[settingName];

            if (sValue != null && sValue.Length > 0)
            {
                int value;
                if (int.TryParse(sValue.Trim(), out value))
                    return value;
                else
                    return defaultValue;
            }
            else
                return defaultValue;
        }
        public static bool GetBoolAppSettings(string settingName, bool defaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[settingName];

            if (sValue != null && sValue.Length > 0)
            {
                bool value;
                if (bool.TryParse(sValue.Trim(), out value))
                    return value;
                else
                    return defaultValue;
            }
            else
                return defaultValue;
        }

        public static decimal GetDecimalAppSettings(string settingName, decimal defaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[settingName];

            if (sValue != null && sValue.Length > 0)
            {
                decimal value;
                if (decimal.TryParse(sValue.Trim(), out value))
                    return value;
                else
                    return defaultValue;
            }
            else
                return defaultValue;
        }

        #endregion GetAppSettings

        //Restricted export column list
        public static string GetExportColumnName(string columnName)
        {

            string columnHeader = "";

                return columnHeader;

        }
        public static Dictionary<string, string> ParseDBConnectionString(string dbConnectionString)
        {
            Dictionary<string, string> dbConnectionStringParams = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(dbConnectionString) == false)
            {
                string[] dbConnectionValues = dbConnectionString.Split(';');

                if (dbConnectionValues != null)
                {
                    foreach (string dbConnectionValue in dbConnectionValues)
                    {
                        string[] dbConnectionValuePair = dbConnectionValue.Trim().Split('=');
                        if (dbConnectionValuePair.Length == 2)
                            dbConnectionStringParams[dbConnectionValuePair[0]] = dbConnectionValuePair[1];
                        else if (dbConnectionValuePair.Length == 1)
                            dbConnectionStringParams[dbConnectionValuePair[0]] = "";
                    }
                }
            }

            return dbConnectionStringParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Distinct(string values)
        {
            if (string.IsNullOrEmpty(values))
                return String.Empty;

            string[] listValues = values.Split(',');
            if (listValues.Length == 1)
                return values;

            HashSet<string> uniqueValues = new HashSet<string>();
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < listValues.Length; index++)
            {
                string value = listValues[index].Trim();

                if (string.IsNullOrEmpty(value) || uniqueValues.Contains(value))
                    continue;

                uniqueValues.Add(value);
                if (index > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(value);

            }
            return stringBuilder.ToString();

        }

        public static int GetNumberOfRows(DataSet dataSet, string tableName)
        {
            return GetNumberOfRows(dataSet.Tables[tableName]);
        }

        public static int GetNumberOfRows(DataTable table)
        {
            return table == null ? 0 : table.Rows.Count;
        }

        public static int[] StringToInts(string myString)
        {
            var ints = new List<int>();

            if (myString != null)
            {
                var strings = myString.Split(',');

                foreach (var s in strings)
                {
                    int i;
                    if (int.TryParse(s.Trim(), out i))
                    {
                        ints.Add(i);
                    }
                }
            }
            return ints.ToArray();
        }

        public static int[] StringToInts(string myString, bool addZero)
        {
            var ints = new List<int>();
            if (myString != null)
            {
                var strings = myString.Split(',');

                foreach (var s in strings)
                {
                    int i;
                    if (int.TryParse(s.Trim(), out i))
                    {
                        ints.Add(i);
                    }
                }
            }

            if (addZero)
                ints.Add(0);
            return ints.ToArray();
        }
    }
    #endregion class DataHelper

    #region class StaticClientInfo
    public class StaticClientInfo
    {
        private string m_UserName;
        private string m_HostName;
        private string m_RemotingPort;
        private string m_version;

        private StaticClientInfo()
        {
        }


        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        public string HostName
        {
            get { return m_HostName; }
            set { m_HostName = value; }
        }

        public string RemotingPort
        {
            get { return m_RemotingPort; }
            set { m_RemotingPort = value; }
        }

        public string ClientID
        {
            get
            {
				return String.Empty;//return string.Format("{0}/{1}/{2}/{3}", m_HostName, m_UserName, m_RemotingPort, Application.ProductVersion).ToUpper();
            }
        }

        public string Version
        {
            get { return m_version; }
            set { m_version = value; }
        }


        public static StaticClientInfo CreateInstance(string clientID)
        {
            var staticClientInfo = new StaticClientInfo();

            string[] arrStaticClientInfo = clientID.Split('/');
            if (arrStaticClientInfo != null)
            {
                if (arrStaticClientInfo.Length > 0)
                    staticClientInfo.HostName = arrStaticClientInfo[0];
                if (arrStaticClientInfo.Length > 1)
                    staticClientInfo.UserName = arrStaticClientInfo[1];
                if (arrStaticClientInfo.Length > 2)
                    staticClientInfo.RemotingPort = arrStaticClientInfo[2];
                if (arrStaticClientInfo.Length > 3)
                    staticClientInfo.Version = arrStaticClientInfo[3];
            }
            return staticClientInfo;
        }
    }
    #endregion class StaticClientInfo

    #region class EnumUtil
    public static class EnumUtil<T>
    {

        public static T Parse(string stringValue, T defaultValue)
        {
            return Parse(stringValue, defaultValue, false);
        }


        public static T Parse(string stringValue, T defaultValue, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(stringValue.Trim()) == false)
                try
                {
                    return Parse(stringValue, ignoreCase);

                }
                catch (Exception)
                {
                    return defaultValue;
                }
            else
                return defaultValue;
        }


        public static T Parse(string stringValue)
        {
            return (T)Enum.Parse(typeof(T), stringValue);
        }

        public static T Parse(string stringValue, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), stringValue, ignoreCase);
        }
    }
    #endregion class EnumUtil
}