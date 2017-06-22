using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{
	public class TaskAlgorithmItemDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string TaskAlgorithmItemId = "TaskAlgorithmItemId";
			public const string TaskAlgorithmId = "TaskAlgorithmId";
			public const string ActivityId = "ActivityId";
			public const string Activity = "Activity";
			public const string Description = "Description";
			public const string SortOrder = "SortOrder";
		}

		public static readonly TaskAlgorithmItemDataModel Empty = new TaskAlgorithmItemDataModel();

		public int? TaskAlgorithmItemId { get; set; }
		public int? TaskAlgorithmId { get; set; }
		public int? ActivityId { get; set; }
		public string Activity { get; set; }
		public string Description { get; set; }
		public int? SortOrder { get; set; }

	}
}
