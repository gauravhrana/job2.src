using System;
using System.Globalization;

namespace Framework.CommonServices.Utils
{
	/// <summary>
	/// Summary description for DateTimeUtils.
	/// </summary>
	public static class DateTimeUtils2
	{
		// Contains date id's
		public static DateTime ExcelBaseDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);

		public static string FormatDateTimeToKey()
		{
			return FormatDateTimeToKey(DateTime.Now);
		}

		public static string FormatDateTimeToKey(DateTime dateTime)
		{
			var dateTimeStamp = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

			dateTimeStamp = dateTimeStamp.Replace(":", String.Empty).Replace(" ", String.Empty).Replace(":", String.Empty);
			dateTimeStamp = dateTimeStamp.Replace("-", String.Empty);

			return dateTimeStamp;
		}


		public static string FormatDateTime(DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public static string FormatShortDateTime(DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd");
		}

		public static string FormatTimeSpan(double milliseconds)
		{
			return milliseconds >= 120000
					   ? string.Format("{0} mins", Math.Round(milliseconds / 60000))
					   : string.Format("{0} seconds", Math.Round(milliseconds / 1000));
		}

		public static int GetYYYYMMDDFromDate(DateTime date)
		{
			var dtf = date.ToString("yyyyMMdd");
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


		public static DateTime GetDateFromDateId(int dateId)
		{
			int year = Convert.ToInt32(dateId / 10000);
			int month = Convert.ToInt32((dateId - year * 10000) / 100);
			int day = dateId % 100;
			return new DateTime(year, month, day);
		}

		public static int GetDateIdFromDateTime(DateTime dt)
		{
			return dt.Year * 10000 + dt.Month * 100 + dt.Day;
		}

		/// <summary>
		/// converts local time to UTC
		/// </summary>
		/// <param name="date"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static DateTime ToUTC(DateTime date, int offset)
		{
			return date.AddMinutes(offset);
		}

		/// <summary>
		/// Converts UTC to local time
		/// </summary>
		/// <param name="date"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static DateTime ToLocalTime(DateTime date, int offset)
		{
			return date.AddMinutes(-offset);
		}

		public static DateTime GetMonthStartDate(DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime GetNextMonthStartDate(DateTime date)
		{
			return GetMonthStartDate(date).AddMonths(1);
		}

		public static DateTime GetMonthEndDate(DateTime date)
		{
			return GetNextMonthStartDate(date).AddDays(-1);
		}

		public static DateTime GetCurrentMonthStartDate()
		{
			return GetMonthStartDate(DateTime.Today);
		}

		public static DateTime GetCurrentMonthEndDate()
		{
			return GetMonthEndDate(DateTime.Today);
		}
	}
}