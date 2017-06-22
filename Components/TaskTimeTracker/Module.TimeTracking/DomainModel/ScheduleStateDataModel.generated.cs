using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{

	[Serializable]
	public partial class ScheduleStateDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ScheduleStateId = "ScheduleStateId";
		}

		public static readonly ScheduleStateDataModel Empty = new ScheduleStateDataModel();

		public int? ScheduleStateId { get; set; }

	}
}
