using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{

	[Serializable]
	public partial class FeatureRuleStatusDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string FeatureRuleStatusId = "FeatureRuleStatusId";
		}

		public static readonly FeatureRuleStatusDataModel Empty = new FeatureRuleStatusDataModel();

		public int? FeatureRuleStatusId { get; set; }

	}
}
