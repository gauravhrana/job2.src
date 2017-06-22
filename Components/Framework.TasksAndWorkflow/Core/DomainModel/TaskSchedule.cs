using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.TasksAndWorkFlow
{
    [Serializable]
	public class TaskScheduleDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskScheduleId = "TaskScheduleId";
			public const string TaskScheduleTypeId = "TaskScheduleTypeId";
			public const string TaskEntityId = "TaskEntityId";
			public const string TaskScheduleType = "TaskScheduleType";
			public const string TaskEntity = "TaskEntity";    
		}

        public static readonly TaskScheduleDataModel Empty = new TaskScheduleDataModel();

		public int? TaskScheduleId { get; set; }
		public int? TaskScheduleTypeId { get; set; }
		public int? TaskEntityId { get; set; }
		public string TaskScheduleType { get; set; }
		public string TaskEntity { get; set; }

	}
}
