using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Task
{

	[Serializable]
	public partial class TaskStatusTypeDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TaskStatusTypeId = "TaskStatusTypeId";
		}

		public static readonly TaskStatusTypeDataModel Empty = new TaskStatusTypeDataModel();

		public int? TaskStatusTypeId { get; set; }

	}
}
