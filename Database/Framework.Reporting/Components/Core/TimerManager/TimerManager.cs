using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Library.CommonServices.Utils.TimerManager
{
    public class TimerManager : IDisposable
    {        
        #region attributes
        private const int PROCESS_INTERVAL = 1000;

        private static TimerManager sm_Manager;
        private static object sm_Lock = new object(); 

        private List<TimerDescription> m_TimerDescriptions;
        private Thread m_TimersProcessingThread;
        private bool m_ShouldRunThread = true;
        #endregion

        public TimerManager()
        {
            m_TimerDescriptions = new List<TimerDescription>();            
            m_TimersProcessingThread = new Thread(new ThreadStart(ProcessTimers));
            m_TimersProcessingThread.IsBackground = true; 
            m_TimersProcessingThread.Start(); 
        }

        public Guid AddIntervalTimer(ulong interval, TimerCallback callback, TimerCallbackType callbackType)
        {
            lock (m_TimerDescriptions)
            {
                TimerDescription timerDescription = new TimerDescription(interval, callback, callbackType);
                m_TimerDescriptions.Add(timerDescription);

                return timerDescription.Id;
            }
        }

        public Guid AddAbsoluteTimer(DateTime timerTime, TimerCallback callback, TimerCallbackType callbackType)
        {
            lock (m_TimerDescriptions)
            {
                TimerDescription timerDescription = new TimerDescription(timerTime, callback, callbackType);
                m_TimerDescriptions.Add(timerDescription);

                return timerDescription.Id;
            }
        }

        public Guid AddAbsoluteTimer(DateTime timerTime, TimerFrequency timerFrequency, TimerCallback callback, TimerCallbackType callbackType)
        {
            lock (m_TimerDescriptions)
            {
                TimerDescription timerDescription = new TimerDescription(timerTime, timerFrequency, callback, callbackType);
                m_TimerDescriptions.Add(timerDescription);

                return timerDescription.Id;
            }
        }

        public bool RemoveTimer(Guid timerId)
        {
            bool removed = false;

            lock (m_TimerDescriptions)
            {
                TimerDescription timerToRemove = null;
                foreach (TimerDescription timerDescription in m_TimerDescriptions)
                {
                    if (timerDescription.Equals(timerId))
                    {
                        timerToRemove = timerDescription;
                        break; 
                    }
                }

                if (timerToRemove != null)
                {
                    m_TimerDescriptions.Remove(timerToRemove);
                    removed = true; 
                }
            }

            return removed; 
        }

        public void Stop()
        {
            lock (m_TimerDescriptions)
            {
                m_ShouldRunThread = false; 
            }
        }

        public void ProcessTimers()
        {
            while (m_ShouldRunThread)
            {
                lock (m_TimerDescriptions)
                {
                    foreach (TimerDescription timerDescription in m_TimerDescriptions)
                    {
                        try 
                        {
                            timerDescription.Update();
                        }
                        catch {}
                    }
                }
                Thread.Sleep(PROCESS_INTERVAL);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Stop(); 
        }

        #endregion

        /// <summary>
        /// Provides a singleton instance, which is the mostly likely way this will be used. 
        /// </summary>
        public static TimerManager Instance
        {
            get
            {
                if (sm_Manager == null)
                {
                    lock (sm_Lock)
                    {
                        if (sm_Manager == null)
                        {
                            sm_Manager = new TimerManager(); 
                        }
                    }
                }

                return sm_Manager; 
            }
        }

        /// <summary>
        /// Stop the manager
        /// </summary>        
        public static bool Close()
        {
            if (sm_Manager != null)
            {
                lock (sm_Lock)
                {
                    if (sm_Lock != null)
                    {
                        sm_Manager.Dispose();
                        sm_Manager = null;
                        return true; 
                    }
                }
            }

            return false; 
        }
    }
}
