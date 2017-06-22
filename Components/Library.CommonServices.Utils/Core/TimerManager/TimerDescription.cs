using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Library.CommonServices.Utils.TimerManager
{
    public class TimerDescription
    {
        private Guid m_TimerId;
        private DateTime m_TimerDateTime;
        private TimerFrequency m_TimerFrequency;
        private Thread m_CallingThread; 
        private TimerType m_TimerType;
        private TimerCallback m_Callback;
        private TimerCallbackType m_CallbackType;
        private ulong m_MsecInterval;
        private DateTime m_TimerCalcTime; 

        private TimerDescription(TimerCallback callback, TimerCallbackType callbackType)
        {
            m_Callback = callback;
            m_TimerId = Guid.NewGuid();
            m_CallbackType = callbackType;
            m_TimerCalcTime = DateTime.Now; 
        }

        internal TimerDescription(DateTime time, TimerCallback callback, TimerCallbackType callbackType)
            : this(time, TimerFrequency.Everyday, callback, callbackType )
        {
        }

        internal TimerDescription(DateTime time, TimerFrequency frequency, 
            TimerCallback callback, TimerCallbackType callbackType)
            : this(callback, callbackType)
        {
            m_TimerDateTime = time;
            m_TimerFrequency = frequency;
            m_TimerType = TimerType.Absolute;
            FirstTimeToFire();
        }

        internal TimerDescription(ulong secs, TimerCallback callback, TimerCallbackType callbackType)
            : this(callback, callbackType)
        {
            if (secs == 0)
            {
                throw new ArgumentException("Interval cannot be 0");
            }

            m_MsecInterval = secs * 1000;
            m_TimerCalcTime = DateTime.Now.AddMilliseconds(m_MsecInterval);
            //TODO: Sandip added 
            m_TimerType = TimerType.Interval;
        }

        private void FirstTimeToFire()
        {
            switch (m_TimerFrequency)
            {
                case TimerFrequency.Everyday:
                    m_TimerCalcTime = m_TimerDateTime;
                    break; 

                case TimerFrequency.Weekdays:
                    m_TimerCalcTime = DateTimeUtils.AdjustToWeekday(m_TimerDateTime);
                    break; 

                case TimerFrequency.Weekend:
                    m_TimerCalcTime = DateTimeUtils.AdjustToWeekend(m_TimerDateTime);
                    break; 

                default:
                    break;
            }
        }

        private void NextTimeToFire()
        {
            switch (m_TimerFrequency)
            {
                case TimerFrequency.Everyday:
                    m_TimerCalcTime = m_TimerCalcTime.AddDays(1d);
                    break;

                case TimerFrequency.Weekdays:
                    m_TimerCalcTime = DateTimeUtils.SetNextWeekday(m_TimerCalcTime);
                    break;

                case TimerFrequency.Weekend:
                    m_TimerCalcTime = DateTimeUtils.SetNextWeekend(m_TimerCalcTime);
                    break;

                default:
                    break;
            }
        }

        

        public Guid Id
        {
            get { return m_TimerId; }
        }

        internal bool Update()
        {
            switch (m_TimerType)
            {
                case TimerType.Interval:
                    return ProcessIntervalTimer();

                case TimerType.Absolute:
                    return ProcessAbsoluteTimer();
 
                default:
                    break; 
            }

            return false; 
        }
        
        private bool ProcessIntervalTimer() 
        {
            bool result = true; 
            DateTime currentTime = DateTime.Now;
            if (currentTime >=
                m_TimerCalcTime)
            {
                result = FireTimer();
                m_TimerCalcTime = currentTime.AddMilliseconds(m_MsecInterval);
            }

            return result;
        }

        private bool ProcessAbsoluteTimer()
        {
            bool result = true;
            DateTime currentTime = DateTime.Now;
            if (currentTime >= m_TimerCalcTime)
            {
                result = FireTimer();
                NextTimeToFire();
            }

            return result;
        }

        private bool FireTimer()
        {
            bool result = true;
            try
            {
                switch (m_CallbackType)
                {
                    case TimerCallbackType.Synchronized:
                        OnFired(null);
                        result = true;
                        break;

                    case TimerCallbackType.NewThread:
                        m_CallingThread = new Thread(new ParameterizedThreadStart(OnFired));
                        m_CallingThread.Start(null);
                        result = true; 
                        break; 

                    case TimerCallbackType.ThreadPool:                        
                        ThreadPool.QueueUserWorkItem(new WaitCallback(OnFired), null);
                        result = true;
                        break; 

                    default:
                        break; 
                }
            }
            catch
            {
                result = false; 
            }

            return result; 
        }

        private void OnFired(object noArg)
        {
            m_Callback.Invoke();             
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TimerDescription) || !(obj is Guid))
            {
                return false; 
            }

            if( obj is TimerDescription) 
            {
                this.m_TimerId.Equals(((TimerDescription)obj).m_TimerId);
            }

            return this.m_TimerId.Equals((Guid)obj);
        }

        public override int GetHashCode()
        {
            return this.m_TimerId.GetHashCode(); 
        }

        public override string ToString()
        {
            return m_TimerId.ToString(); 
        }
    }
}
