using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.TasksAndWorkFlow
{
    [Serializable]
	public class TaskScheduleTypeDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskScheduleTypeId = "TaskScheduleTypeId";
			public const string Active = "Active";
		}

        public static readonly TaskScheduleTypeDataModel Empty = new TaskScheduleTypeDataModel();
        
		public int? TaskScheduleTypeId { get; set; }
		public int? Active { get; set; }

	}
}
