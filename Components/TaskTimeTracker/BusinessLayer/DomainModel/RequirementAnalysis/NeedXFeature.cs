using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
	public class NeedXFeatureDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string NeedXFeatureId	= "NeedXFeatureId";
			public const string NeedId			= "NeedId";			
			public const string FeatureId		= "FeatureId";

			public const string Need			= "Need";
			public const string Feature			= "Feature";
		}

		public int? NeedXFeatureId { get; set; }
		public int? NeedId { get; set; }
		public int? FeatureId { get; set; }

		public string Need { get; set; }
		public string Feature { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"NeedId=" + NeedId
		}
	}
}
