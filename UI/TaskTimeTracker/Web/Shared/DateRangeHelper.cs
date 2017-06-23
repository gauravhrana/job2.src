using System;
using System.Collections.Generic;

namespace Shared.UI.Web
{
	public class DateRangeHelper
	{
		public static DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, 1);
		}

		public static DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
		{
			var firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
			return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
		}

		public static string[] FillUpDate(string strDateRange,string dateformatRange)
		{
			var fromDate = String.Empty;
			var toDate = String.Empty;
			
			
			var dtNow = DateTime.Now;

            if (strDateRange == "All" || strDateRange == "Custom" || strDateRange == "None")
			{
				fromDate = String.Empty;
				toDate = String.Empty;
			}

			if (strDateRange == "Today")
			{
				fromDate = dtNow.ToString(dateformatRange);
				toDate = dtNow.ToString(dateformatRange);
			}
			else if (strDateRange == "This Week")
			{
				var dayToday = DateTime.Today;
				var dayFirst = dayToday.AddDays(1 - dayToday.DayOfWeek.GetHashCode());
				var dayLast = dayToday.AddDays(7 - dayToday.DayOfWeek.GetHashCode());
				fromDate = dayFirst.ToString(dateformatRange);
				toDate = dayLast.ToString(dateformatRange);
			}
			else if (strDateRange == "This Week-to-date")
			{
				var dayToday = DateTime.Today;
				var dayFirst = dayToday.AddDays(1 - dayToday.DayOfWeek.GetHashCode());

				fromDate = dayFirst.ToString(dateformatRange);
				toDate = DateTime.Now.ToString(dateformatRange);
			}

			else if (strDateRange == "This Month")
			{
				var dtStartMonth = FirstDayOfMonthFromDateTime(dtNow);
				var dtEndMonth = LastDayOfMonthFromDateTime(dtNow);
				fromDate = dtStartMonth.ToString(dateformatRange);
				toDate = dtEndMonth.ToString(dateformatRange);
			}
			else if (strDateRange == "This Month-to-date")
			{
				var dtStartMonth = FirstDayOfMonthFromDateTime(dtNow);
				fromDate = dtStartMonth.ToString(dateformatRange);
				toDate = dtNow.ToString(dateformatRange);
			}
			else if (strDateRange == "Last Month")
			{
				var prevMonth = dtNow.AddMonths(-1).Month;
				var year = dtNow.AddMonths(-1).Year;
				var daysInPrevMonth = DateTime.DaysInMonth(year, prevMonth);
				var firstDayPrevMonth = new DateTime(year, prevMonth, 1);
				var lastDayPrevMonth = new DateTime(year, prevMonth, daysInPrevMonth);

				fromDate = firstDayPrevMonth.ToString(dateformatRange);
				toDate = lastDayPrevMonth.ToString(dateformatRange);
			}
			else if (strDateRange == "Last Month-to-date")
			{
				var prevMonth = dtNow.AddMonths(-1).Month;
				var year = dtNow.AddMonths(-1).Year;
				var firstDayPrevMonth = new DateTime(year, prevMonth, 1);
				fromDate = firstDayPrevMonth.ToString(dateformatRange);
				toDate = dtNow.ToString(dateformatRange);
			}
			else if (strDateRange == "Last Week")
			{
				var date = dtNow.AddDays(-7);
				while (date.DayOfWeek != DayOfWeek.Monday)
					date = date.AddDays(-1);

				var startDate = date;
				var endDate = date.AddDays(7);

				fromDate = startDate.ToString(dateformatRange);
				toDate = endDate.ToString(dateformatRange);
			}

			else if (strDateRange == "Last Week-to-date")
			{
				var date = dtNow.AddDays(-7);
				while (date.DayOfWeek != DayOfWeek.Monday)
					date = date.AddDays(-1);

				var startDate = date;

				fromDate = startDate.ToString(dateformatRange);
				toDate = dtNow.ToString(dateformatRange);
			}
			else if (strDateRange == "Yesterday")
			{
				var dtyesterday = dtNow.Date.AddDays(-1);
				fromDate = dtyesterday.ToString(dateformatRange);
				toDate = dtyesterday.ToString(dateformatRange);
			}
			else if (strDateRange == "This Quarter")
			{
				var startingMonth = (dtNow.Month - 1) / 3;
				startingMonth *= 3;
				startingMonth++;
				var firstDayThisQuarter = new DateTime(dtNow.Year, startingMonth, 1);
				var lastDayOfQuarter = firstDayThisQuarter.AddMonths(3).AddDays(-1);
				fromDate = firstDayThisQuarter.ToString(dateformatRange);
				toDate = lastDayOfQuarter.ToString(dateformatRange);
			}
			else if (strDateRange == "This Quarter-to-date")
			{
				var startingMonth = (dtNow.Month - 1) / 3;
				startingMonth *= 3;
				startingMonth++;
				var firstDayThisQuarter = new DateTime(dtNow.Year, startingMonth, 1);
				fromDate = firstDayThisQuarter.ToString(dateformatRange);
				toDate = dtNow.ToString(dateformatRange);
			}
			else if (strDateRange == "This Year")
			{
				var firstDay = new DateTime(DateTime.Now.Year, 1, 1);
				var lastDay = new DateTime(DateTime.Now.Year, 12, 31);
				fromDate = firstDay.ToString(dateformatRange);
				toDate = lastDay.ToString(dateformatRange);
			}
			else if (strDateRange == "This Year-to-date")
			{
				var firstDay = new DateTime(DateTime.Now.Year, 1, 1);
				fromDate = firstDay.ToString(dateformatRange);
				toDate = dtNow.ToString(dateformatRange);
			}

			else if (strDateRange == "Last Quarter")
			{
				fromDate = new DateTime(dtNow.Year, dtNow.Month - ((dtNow.Month - 1) % 3), 1).AddMonths(-3).ToString(dateformatRange);
				toDate = new DateTime(dtNow.Year, dtNow.Month - ((dtNow.Month - 1) % 3), 1).AddDays(-1).ToString(dateformatRange);				

			}
			else if (strDateRange == "Last Quarter-to-date")
			{
				fromDate = new DateTime(dtNow.Year, dtNow.Month - ((dtNow.Month - 1) % 3), 1).AddMonths(-3).ToString(dateformatRange);
				toDate = dtNow.ToString(dateformatRange);
			}
			else if (strDateRange == "Last Year")
			{
				var lastYearStart = new DateTime(DateTime.Now.Year - 1, 1, 1);
				var lastYearEnd = lastYearStart.AddMonths(12).AddDays(-1);

				fromDate = lastYearStart.ToString(dateformatRange);
				toDate = lastYearEnd.ToString(dateformatRange);
			}
			else if (strDateRange == "Last Year-to-date")
			{
				var lastYearStart = new DateTime(DateTime.Now.Year - 1, 1, 1);
				fromDate = lastYearStart.ToString(dateformatRange);
				toDate = dtNow.ToString(dateformatRange);
			}
			else if (strDateRange == "Next Week")
			{
				var date = dtNow.AddDays(+7);
				while (date.DayOfWeek != DayOfWeek.Monday)
					date = date.AddDays(-1);

				var startDate = date;
				var endDate = date.AddDays(7);
				fromDate = startDate.ToString(dateformatRange);
				toDate = endDate.ToString(dateformatRange);
			}

			else if (strDateRange == "Next 4 Weeks")
			{
				var date = dtNow.AddDays(+7);
				while (date.DayOfWeek != DayOfWeek.Monday)
					date = date.AddDays(-1);

				var startDate = date;
				var endDate = date.AddDays(28);
				fromDate = startDate.ToString(dateformatRange);
				toDate = endDate.ToString(dateformatRange);
			}

			else if (strDateRange == "Next Month")
			{
				var nextMonth = dtNow.AddMonths(+1).Month;
				var year = dtNow.AddMonths(+1).Year;
				var daysInNextMonth = DateTime.DaysInMonth(year, nextMonth);
				var firstDayNextMonth = new DateTime(year, nextMonth, 1);
				var lastDayNextMonth = new DateTime(year, nextMonth, daysInNextMonth);
				fromDate = firstDayNextMonth.ToString(dateformatRange);
				toDate = lastDayNextMonth.ToString(dateformatRange);
			}

			else if (strDateRange == "Next Quarter")
			{
				var startingMonth = (dtNow.Month - 1) / 3;
				startingMonth *= 3;
				startingMonth++;
				var firstDayNextQuarter = new DateTime(dtNow.Year, startingMonth + 3, 1);
				var lastDayOfNextQuarter = firstDayNextQuarter.AddMonths(3).AddDays(-1);
				fromDate = firstDayNextQuarter.ToString(dateformatRange);
				toDate = lastDayOfNextQuarter.ToString(dateformatRange);
			}

			else if (strDateRange == "Next Year")
			{
				var nextYearStart = new DateTime(DateTime.Now.Year + 1, 1, 1);
				var nextYearEnd = nextYearStart.AddMonths(12).AddDays(-1);

				fromDate = nextYearStart.ToString(dateformatRange);
				toDate = nextYearEnd.ToString(dateformatRange);
			}

			var dates = new List<string>();
			dates.Add(fromDate);
			dates.Add(toDate);
			return dates.ToArray();
		}
	}
}