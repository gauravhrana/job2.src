using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{

	[Serializable]
	public partial class ActivityStateDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ActivityStateId = "ActivityStateId";
		}

		public static readonly ActivityStateDataModel Empty = new ActivityStateDataModel();

		public int? ActivityStateId { get; set; }

	}
}
