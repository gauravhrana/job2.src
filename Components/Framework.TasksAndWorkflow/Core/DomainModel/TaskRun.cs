using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.TasksAndWorkFlow
{
    [Serializable]
    public class TaskRunDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{

            public const string TaskRunId        = "TaskRunId";
            public const string TaskScheduleId   = "TaskScheduleId";
            public const string TaskEntityId     = "TaskEntityId";
            public const string BusinessDate     = "BusinessDate";
            public const string StartTime        = "StartTime";
            public const string EndTime          = "EndTime";
            public const string RunBy            = "RunBy";
            public const string TaskEntity       = "TaskEntity";
        }

        public static readonly TaskRunDataModel Empty = new TaskRunDataModel();
          
        public int?         TaskRunId       { get; set; }
        public int?         TaskScheduleId  { get; set; }
        public int?         TaskEntityId    { get; set; }
        public DateTime?    BusinessDate    { get; set; }
        public DateTime?    StartTime       { get; set; }
        public DateTime?    EndTime         { get; set; }
        public string       RunBy           { get; set; }
        public string       TaskEntity      { get; set; }

    }
}