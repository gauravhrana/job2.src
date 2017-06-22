using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker.Task
{
	public class TaskDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskId = "TaskId";
			public const string TaskTypeId = "TaskTypeId";
			public const string TaskType = "TaskType";		
		}

		public int? TaskId { get; set; }
		public int? TaskTypeId { get; set; }
		public string TaskType { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"TaskId=" + TaskId
		}
	}
}
