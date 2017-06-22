using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{

	[Serializable]
	public partial class FeatureRuleCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string FeatureRuleCategoryId = "FeatureRuleCategoryId";
		}

		public static readonly FeatureRuleCategoryDataModel Empty = new FeatureRuleCategoryDataModel();

		public int? FeatureRuleCategoryId { get; set; }

	}
}
