using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;

namespace Framework.CommonServices.BusinessDomain.Utils
{
	/// <summary>
	/// Summary description for DateTimeUtils.
	/// </summary>
	public sealed class DateTimeUtils
	{
		// private constructor: Singleton does not get instantiated
		private DateTimeUtils()	{}

		// Contains date id's
		private static ListDictionary m_OfficeHolidayMap;
		public static DateTime ExcelBaseDate = new DateTime(1900,1,1,0,0,0,0);

		static DateTimeUtils()
		{
			m_OfficeHolidayMap = new ListDictionary();

			//foreach (OfficeLocation ol in Enum.GetValues(typeof(OfficeLocation)))
			//    m_OfficeHolidayMap.Add(ol, new Hashtable());
		}

		//// takes a list of holiday dateid's
		//public static void InitHolidays(OfficeLocation ol, IList holidays)
		//{
		//    Hashtable h = (Hashtable)m_OfficeHolidayMap[ol];
		//    h.Clear();
			
		//    foreach (int dateId in holidays)
		//        h.Add(dateId, true);
		//}


		//public static bool AreHolidaysInitialized()
		//{
		//    return ((Hashtable)m_OfficeHolidayMap[OfficeLocation.NYC]).Count > 0;
		//}

		public static string FormatDateTime(DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public static string FormatTimeSpan(double milliseconds)
		{
			return milliseconds >= 120000
				? string.Format("{0} mins", Math.Round(milliseconds / 60000))
				: string.Format("{0} seconds", Math.Round(milliseconds / 1000));
		}

		public static int GetYYYYMMDDFromDate(DateTime date)
		{

			string dtf = date.ToString("yyyyMMdd");
			return int.Parse(dtf);
		}

		public static DateTime GetDateFromYYYYMMDD(string yyyymmdd)
		{
			double dateId;
			if (yyyymmdd.Length != 8 
				|| !Double.TryParse(yyyymmdd, NumberStyles.Integer, null, out dateId))
				return DateTime.MinValue;

			return GetDateFromDateId(Convert.ToInt32(dateId));
		}

		//public static DateTime GetPreviousBusinessDate(DateTime dt)
		//{
		//    DateTime tempDate = dt.AddDays(-1);
		//    while (IsHoliday(tempDate))
		//        tempDate = tempDate.AddDays(-1);
		//    return tempDate;
		//}

		public static DateTime GetNextBusinessDate(DateTime dt)
		{
			DateTime tempDate = dt.AddDays(1);
            
            //while (IsHoliday(tempDate))
            //    tempDate = tempDate.AddDays(1);

            // JP, 20Jun2007 - temp. fix while we don't have holiday days listed
            // instead we return the next weekday

            while (tempDate.DayOfWeek == DayOfWeek.Saturday || tempDate.DayOfWeek == DayOfWeek.Sunday)
                tempDate = tempDate.AddDays(1);

            return tempDate;

		}

        public static int GetNextBusinessDateId(int dtId)
        {
            // JP, 20Jun2007 - returns the next BusinessDateId given a DateId
            return GetDateIdFromDateTime(GetNextBusinessDate(GetDateFromDateId(dtId)));
        }

		//public static DateTime AddBusinessDays(DateTime dt, int NumDays)
		//{
		//    DateTime dtVal = dt;
		//    if(NumDays > 0)
		//    {
		//        while(NumDays-- > 0)
		//            dtVal = DateTimeUtils.GetNextBusinessDate(dtVal);
		//    }
		//    else
		//    {
		//        while(NumDays++ < 0)
		//            dtVal = DateTimeUtils.GetPreviousBusinessDate(dtVal);
		//    }
		//    return dtVal;
		//}

		//public static bool IsHoliday(DateTime dt)
		//{
		//    return IsHoliday(OfficeLocation.NYC, dt);
		//}

		//public static bool IsHoliday(OfficeLocation ol, DateTime dt)
		//{
		//    if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
		//        return true;
		//    if (((Hashtable)m_OfficeHolidayMap[ol]).Contains(GetDateIdFromDateTime(dt)))
		//        return true;
		//    return false;
		//}

		//public static bool IsBusinessDate(DateTime dt)
		//{
		//    return !IsHoliday(dt);
		//}

		//public static bool IsBusinessDate(OfficeLocation ol, DateTime dt)
		//{
		//    return !IsHoliday(ol, dt);
		//}

		public static DateTime GetDateFromDateId(int dateId)
		{
			int year = Convert.ToInt32(dateId / 10000);
			int month = Convert.ToInt32((dateId - year * 10000)/100);
			int day = dateId % 100;
			return new DateTime(year, month, day);
		}

		public static int GetDateIdFromDateTime(DateTime dt)
		{
			return dt.Year * 10000 + dt.Month * 100 + dt.Day;
		}
	}
}
