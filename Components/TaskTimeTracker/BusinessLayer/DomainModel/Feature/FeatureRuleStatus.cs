using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{
	public class FeatureRuleStatusDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FeatureRuleStatusId = "FeatureRuleStatusId";
		}

		public int? FeatureRuleStatusId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"FeatureRuleStatusId=" + FeatureRuleStatusId
		}
	}
}
