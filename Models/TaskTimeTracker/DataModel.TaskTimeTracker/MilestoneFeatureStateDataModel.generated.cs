using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class MilestoneFeatureStateDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string MilestoneFeatureStateId = "MilestoneFeatureStateId";
		}

		public static readonly MilestoneFeatureStateDataModel Empty = new MilestoneFeatureStateDataModel();

		public int? MilestoneFeatureStateId { get; set; }

	}
}
