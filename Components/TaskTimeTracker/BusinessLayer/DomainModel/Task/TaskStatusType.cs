using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker.Task
{
	public class TaskStatusTypeDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskStatusTypeId = "TaskStatusTypeId";
		}

		public int? TaskStatusTypeId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"TaskStatusTypeId=" + TaskStatusTypeId
		}
	}
}
