using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Task
{

	[Serializable]
	public partial class TaskTypeDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TaskTypeId = "TaskTypeId";
		}

		public static readonly TaskTypeDataModel Empty = new TaskTypeDataModel();

		public int? TaskTypeId { get; set; }

	}
}
