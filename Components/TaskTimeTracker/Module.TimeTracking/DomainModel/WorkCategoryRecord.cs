using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.TaskTimeTracker.TimeTracking
{

    public class SpecialData
    {
        public string   WeekNo;
        public string   Value;
    }

    public class WorkCategoryRecord
    {
        public string           Name;
        public SpecialData      []Info;
        public string           Total;
    }
}
