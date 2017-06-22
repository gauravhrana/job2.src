using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{

	[Serializable]
	public partial class FeatureGroupDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string FeatureGroupId = "FeatureGroupId";
		}

		public static readonly FeatureGroupDataModel Empty = new FeatureGroupDataModel();

		public int? FeatureGroupId { get; set; }

	}
}
