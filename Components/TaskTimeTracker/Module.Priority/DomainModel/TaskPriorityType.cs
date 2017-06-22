using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Priority
{
    [Serializable]
	public class TaskPriorityTypeDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskPriorityTypeId = "TaskPriorityTypeId";

			public const string Weight = "Weight";
		}

        public static readonly TaskPriorityTypeDataModel Empty = new TaskPriorityTypeDataModel();

		public int? TaskPriorityTypeId { get; set; }

		public Decimal? Weight { get; set; }
		
	}
}
