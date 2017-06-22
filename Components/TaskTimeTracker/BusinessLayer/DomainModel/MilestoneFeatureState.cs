using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class MilestoneFeatureStateDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string MilestoneFeatureStateId = "MilestoneFeatureStateId";
		}

		public int? MilestoneFeatureStateId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"MilestoneFeatureStateId=" + MilestoneFeatureStateId
		}
	}
}
