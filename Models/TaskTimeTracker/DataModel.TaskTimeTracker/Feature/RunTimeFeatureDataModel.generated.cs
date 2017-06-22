using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{

	[Serializable]
	public partial class RunTimeFeatureDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string RunTimeFeatureId = "RunTimeFeatureId";
		}

		public static readonly RunTimeFeatureDataModel Empty = new RunTimeFeatureDataModel();

		public int? RunTimeFeatureId { get; set; }

	}
}
