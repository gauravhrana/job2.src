using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{

	[Serializable]
	public partial class ScheduleDetailActivityCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ScheduleDetailActivityCategoryId = "ScheduleDetailActivityCategoryId";
		}

		public static readonly ScheduleDetailActivityCategoryDataModel Empty = new ScheduleDetailActivityCategoryDataModel();

		public int? ScheduleDetailActivityCategoryId { get; set; }

	}
}
