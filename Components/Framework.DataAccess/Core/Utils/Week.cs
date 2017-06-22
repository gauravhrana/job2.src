using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.CommonServices.BusinessDomain.Utils
{
    public class Week
    {
        DateTime m_DateTime;

        public Week()
        {
            m_DateTime = DateTime.Now;
        }

        public Week(DateTime dateTime)
        {
            m_DateTime = dateTime;
        }

        public DateTime WeekStart
        {
            get
            {
                return new DateTime(m_DateTime.Year, m_DateTime.Month, m_DateTime.Day, 0, 0, 0, 0);
            }
        }

        public DateTime NextWeekStart
        {
            get
            {
                return WeekStart.AddDays(7);
            }
        }

        public bool IsWeekend
        {
            get { return m_DateTime.DayOfWeek == DayOfWeek.Saturday || m_DateTime.DayOfWeek == DayOfWeek.Sunday; }
        }

        public bool IsBusinessWeekend
        {
            // at This fund - calendarweekend and business weekeends are the same
            // if an asia office opens, this could come in handy
            get
            {
                bool result = false;
                TimeSpan tsOffset = m_DateTime - m_DateTime.Date;
                switch (m_DateTime.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        result = true;
                        break;
                    default:
                        result = false;
                        break;
                }
                return result;
            }
        }

    }
}
