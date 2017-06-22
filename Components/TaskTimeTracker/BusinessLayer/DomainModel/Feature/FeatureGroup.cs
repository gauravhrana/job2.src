using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{
	public class FeatureGroupDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FeatureGroupId = "FeatureGroupId";
		}

		public int? FeatureGroupId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"FeatureGroupId=" + FeatureGroupId
		}
	}
}
