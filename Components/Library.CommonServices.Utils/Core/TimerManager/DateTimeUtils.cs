using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.CommonServices.Utils.TimerManager
{
    public static class DateTimeUtils
    {
        
        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsDayOfWeek(DateTime date)
        {
            return !IsWeekend(date); 
        }

        public static DateTime AdjustToWeekday(DateTime date)
        {
            DateTime adjustedDateTime = date;

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    adjustedDateTime = date.AddDays(1d);
                    break; 

                case DayOfWeek.Saturday:
                    adjustedDateTime = date.AddDays(2d);
                    break;

                default:
                    break; 
            }

            return adjustedDateTime; 
        }

        public static DateTime AdjustToWeekend(DateTime date)
        {
            DateTime adjustedDateTime = date;

            //Adjust for a weekday
            if ((int)date.DayOfWeek > 0 && (int)date.DayOfWeek < 6)
            {
                int dayOffset = (int)DayOfWeek.Saturday - (int)date.DayOfWeek;
                adjustedDateTime = date.AddDays(dayOffset); 
            }

            return adjustedDateTime; 
        }

        /// <summary>
        /// Gets the next weekday for the date passed in
        /// </summary>
        /// <param name="date">date to set next weekday for</param>
        /// <returns>next weekday</returns>
        public static DateTime SetNextWeekday(DateTime date)
        {
            DateTime nextWeekday = date; 
            //from monday to thursday
            if ((int)date.DayOfWeek >= 0 && (int)date.DayOfWeek < 5)
            {
                nextWeekday = date.AddDays(1d);                  
            }
            else if (date.DayOfWeek == DayOfWeek.Friday)
            {
                nextWeekday = date.AddDays(3d);
            }
            else
            {
                //Only Saturday is not covered            
                nextWeekday = date.AddDays(2d);
            }

            return nextWeekday; 
        }

        public static DateTime SetNextWeekend(DateTime date)
        {
            //Adjust for a weekday
            if ((int)date.DayOfWeek > 0 && (int)date.DayOfWeek < 6)
            {
                return AdjustToWeekend(date);
            }

            DateTime nextWeekend;
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                nextWeekend = date.AddDays(6d);
            }
            else
            {
                nextWeekend = date.AddDays(1d);
            }

            return nextWeekend; 
        }

        
    }
}
