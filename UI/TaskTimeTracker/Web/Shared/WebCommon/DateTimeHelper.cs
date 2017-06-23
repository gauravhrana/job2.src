using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace Shared.WebCommon.UI.Web
{
    
    public class DateTimeHelper
    {
        
        public static DateTime? FromUserDateFormatToDate(string dateValue)
        {
            DateTime? resultDateValue = null;

            if (!string.IsNullOrEmpty(dateValue))
            {
                // retrireved date value is in ApplicationCommon.ApplicationDateFormat format as string
                resultDateValue = DateTime.ParseExact(dateValue, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }

            return resultDateValue;
        }

        public static string FromApplicationDateFormatToUserDateFormat(string dateValue)
        {
            var resultDateValue = string.Empty;

			if (!string.IsNullOrEmpty(dateValue))
            {
                // retrireved date value is in ApplicationCommon.ApplicationDateFormat format as string
                var fromDateTime = DateTime.ParseExact(dateValue, ApplicationCommon.ApplicationDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                // convert to user date format to display
                resultDateValue = fromDateTime.ToString(SessionVariables.UserDateFormat);
            }

            return resultDateValue;
        }

        public static DateTime? FromApplicationDateFormatToDate(string dateValue)
        {
            DateTime? resultDateValue = null;

            if (!string.IsNullOrEmpty(dateValue))
            {
                // retrireved date value is in ApplicationCommon.ApplicationDateFormat format as string
                resultDateValue = DateTime.ParseExact(dateValue, ApplicationCommon.ApplicationDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }

            return resultDateValue;        
        }

        public static string FromUserDateFormatToApplicationDateFormat(string dateValue)
        {
            var resultDateValue = string.Empty;

            if (!string.IsNullOrEmpty(dateValue))
            {
                var fromDateTime = new DateTime();

                fromDateTime = DateTime.ParseExact(dateValue, SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
                resultDateValue = fromDateTime.ToString(ApplicationCommon.ApplicationDateFormat);

            }

            return resultDateValue;
        }

		public static string CovertDateFormatToJavascript()
		{
			string dateFormat = SessionVariables.UserDateFormat.Replace("MM", "mm").Replace("M", "m").Replace("mmmm", "MM").Replace("mmm", "M").Replace("dddd", "DD").Replace("ddd", "D").Replace("yy", "y");
			return dateFormat;
		}

        public static int GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            var months = 0;

			if(endDate > startDate)
				months = (endDate - startDate).Days / 30;
			else if (startDate > endDate)
				months = (startDate - endDate).Days / 30;

            return months;
        }
		
    }

}