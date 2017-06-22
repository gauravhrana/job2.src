using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker.Task
{
	public class TaskTypeDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskTypeId = "TaskTypeId";
		}

		public int? TaskTypeId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"TaskTypeId=" + TaskTypeId
		}
	}
}
