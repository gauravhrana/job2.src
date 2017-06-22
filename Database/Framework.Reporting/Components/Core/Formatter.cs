using System;
using System.Data;
using System.Globalization;
using System.Threading;

namespace Library.CommonServices.Utils
{
    public class Formatter
    {
        /// <summary>
        /// 
        /// </summary>
        private Formatter()
        {
        }

        public static string GetDecimalEx(object dataItem, string column, string format, string nullValue)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return nullValue;
            return Convert.ToDecimal(val).ToString(format);
        }

        public static string GetQuantity(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "n", "0");
        }

        public static string GetDecimal(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "n", "0");
        }

        public static string GetFormattedPct(decimal dPct)
        {
            return dPct.ToString("#,##0.0;-#,##0.0;0.0") + "%";
        }

        public static string GetFormattedAmount(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "#,#;(#,#);-", "-");
        }

        public static string GetFormattedAmount(decimal? value, string defaultValue)
        {
            if (value == null)
                return defaultValue;

            return Convert.ToDecimal(value).ToString("#,#;(#,#);0");
        }


        public static string GetFormattedAmount(decimal? dVal)
        {
            decimal dFormat = 0;
            if (dVal != null)
                dFormat = dVal.Value;
            return dFormat.ToString("#,#;(#,#);-");
        }

        public static string GetFormattedPct(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "#,##0.0;-#,##0.0;0.0", "0.0") + "%";
        }

        public static string GetFormattedPctEx(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "#,##0.0#;-#,##0.0#;0.0", "0.0") + "%";
        }

        public static string GetMoney(decimal? dVal)
        {
            decimal dFormat = 0;
            if (dVal != null)
                dFormat = dVal.Value;
            return dFormat.ToString("#,##0.00;(#,##0.00);0");
        }

        public static string GetMoney(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "#,##0.00;(#,##0.00);0", "0");
        }

        public static string Get2DigitDecimal(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "#,##0.00;", "");
        }
        public static string GetunformattedPrice(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "###0.00;0", "0");
        }
        public static string GetunformattedPrice2(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, "###0.00;-###0.00;0.00", "0");
        }
        public static string GetBps(object dataItem, string column, string nullValueString)
        {
            return GetDecimalEx(dataItem, column, CommonStringFormats.MoneyAccountingFormat2, nullValueString);
        }

        public static string GetPnl(object dataItem, string column)
        {
            return GetDecimalEx(dataItem, column, CommonStringFormats.MoneyAccountingFormat, "0");
        }


        public static string GetString(object dataItem, string column)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return String.Empty;
            return val.ToString();
        }

        public static string GetDate(object dataItem, string column)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return String.Empty;
            return Convert.ToDateTime(val).ToString("d");
        }

        public static string GetDateTime(object dataItem, string column)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return String.Empty;
            return Convert.ToDateTime(val).ToString();
        }

        public static object GetDateTimeObject(object dataItem, string column, string format)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return null;
            return Convert.ToDateTime(val).ToString(format);
        }

        public static string GetLocalDateTime(object dataItem, string column)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return String.Empty;
            return Convert.ToDateTime(val).ToLocalTime().ToString();
        }

        public static string GetTime(object dataItem, string column)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return String.Empty;
            return Convert.ToDateTime(val).ToString("t");
        }

        public static string GetDateStrFromInt(object dataItem, string column)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return String.Empty;
            string date = val.ToString();
            return string.Format("{1}/{2}/{0}",
                                 date.Substring(0, 4), date.Substring(4, 2), date.Substring(6, 2));
        }

        public static bool GetBoolBit(object dataItem, string column)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return false;
            return Convert.ToInt16(val) != 0;
        }

        public static string GetHr(object dataItem, string column)
        {
            int minutes = GetInteger(dataItem, column);
            if (minutes != 0)
            {
                int hour = minutes / 60;
                return (Get12Hr(hour));
            }
            return "";
        }

        public static string GetMin(object dataItem, string column)
        {
            int minutes = GetInteger(dataItem, column);

            if (minutes != 0)
            {
                if ((minutes % 60) < 10)
                    return ("0" + (minutes % 60));
                return (minutes % 60).ToString();
            }

            return "";
        }

        public static string GetAP(object dataItem, string column)
        {
            int minutes = GetInteger(dataItem, column);
            if (minutes != 0)
            {
                if (minutes <= 719)
                    return "AM";
                return "PM";
            }
            return "";
        }

        public static string GetYesNo(object dataItem, string column)
        {
            if (GetBoolBit(dataItem, column))
                return "Yes";
            return "No";
        }


        public static int GetInteger(object dataItem, string column)
        {
            object val = GetObject(dataItem, column);
            if (val == DBNull.Value)
                return 0;
            return Convert.ToInt32(val);
        }

        public static int GetNullInt(string txt)
        {
            if (txt != "")
                return Convert.ToInt32(txt);
            return default(int);
        }

        public static DateTime GetNullDateTime(String txt)
        {
            if (txt != "")
                return Convert.ToDateTime(txt);
            return default(DateTime);
        }

        public static string Get12Hr(int hour)
        {
            string r_Hour;

            if ((hour == 0) || (hour == 12))
                r_Hour = "12";
            else if ((hour > 0) && (hour < 12))
                r_Hour = hour.ToString();
            else if ((hour > 12) && (hour < 24))
                r_Hour = (hour - 12).ToString();
            else
                r_Hour = "0";


            if (Convert.ToInt16(r_Hour) < 10)
                return ("0" + r_Hour);
            return r_Hour;
        }

        public static int Get24Hr(int hour, string apm)
        {
            if (hour == 12)
            {
                if (apm == "AM")
                    return 0;
                return 12;
            }
            if ((hour > 0) && (hour < 12))
            {
                if (apm == "AM")
                    return hour;
                return (12 + hour);
            }
            return 0;
        }

        public static int GetMinutes(int hour, int min, string apm)
        {
            int hr = Get24Hr(hour, apm);
            return ((hr * 60) + min);
        }

        public static string GetInt(object dataItem, string column)
        {
            return GetInteger(dataItem, column).ToString();
        }

        public static object GetObject(object dataItem, string column)
        {
            var drv = dataItem as DataRowView;
            if (drv != null)
                return drv[column];
            var dr = dataItem as DataRow;
            if (dr != null)
                return dr[column];
            return DBNull.Value;
        }

        public string GetGridTime(object dataItem, string column)
        {
            string str = GetHr(dataItem, column);
            if (str == "")
                return "";
            return (str + ":" + GetMin(dataItem, column) + " " + GetAP(dataItem, column));
        }

        public static string ToTitleCase(string inputString)
        {
            CultureInfo cultureInfo =
                Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(inputString.ToLower());
        }

		public static string FormatCode(string _input)
		{
			return _input.ToUpper().Replace(" ", "_");

		}
    }
}