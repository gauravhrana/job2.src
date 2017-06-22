using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{

	[Serializable]
	public partial class FeatureDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string FeatureId = "FeatureId";
		}

		public static readonly FeatureDataModel Empty = new FeatureDataModel();

		public int? FeatureId { get; set; }

	}
}
