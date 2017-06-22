using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.CommonServices.Utils.TimerManager
{
    public enum TimerType
    {
        Interval, 
        Absolute
    }

    public enum TimerFrequency
    {
        Weekdays,
        Everyday, 
        Weekend
    }


    public enum TimerCallbackType
    {
        Synchronized, 
        NewThread, 
        ThreadPool
    }
}
