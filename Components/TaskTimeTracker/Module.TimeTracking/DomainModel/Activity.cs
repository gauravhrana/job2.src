using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{
	public class ActivityDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ActivityId = "ActivityId";
			public const string LayerId = "LayerId";
			public const string Layer = "Layer";
		}

		public static readonly ActivityDataModel Empty = new ActivityDataModel();

		public int? ActivityId { get; set; }
		public int? LayerId { get; set; }
		public string Layer { get; set; }

	}
}
