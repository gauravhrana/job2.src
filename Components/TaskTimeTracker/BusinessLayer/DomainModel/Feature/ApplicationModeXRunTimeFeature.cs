using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{
	public class ApplicationModeXRunTimeFeatureDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ApplicationModeId = "ApplicationModeId";
			public const string ApplicationModeXRunTimeFeatureId = "ApplicationModeXRunTimeFeatureId";
			public const string RunTimeFeatureId = "RunTimeFeatureId";

			public const string ApplicationMode = "ApplicationMode";
			public const string RunTimeFeature = "RunTimeFeature";
		}

		public int? ApplicationModeXRunTimeFeatureId { get; set; }
		public int? ApplicationModeId { get; set; }
		public int? RunTimeFeatureId { get; set; }

		public string ApplicationMode { get; set; }
		public string RunTimeFeature { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"ApplicationModeId=" + ApplicationModeId
		}
	}
}


