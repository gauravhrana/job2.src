using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityXFunctionalityDevelopmentStepDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string FunctionalityXFunctionalityDevelopmentStepId = "FunctionalityXFunctionalityDevelopmentStepId";
			public const string FunctionalityId								 = "FunctionalityId";
			public const string FunctionalityDevelopmentStepId				 = "FunctionalityDevelopmentStepId";
			public const string Functionality								 = "Functionality";
			public const string FunctionalityDevelopmentStep				 = "FunctionalityDevelopmentStep";
			public const string SortOrder									 = "SortOrder";
			public const string Version										 = "Version";
		}

		public static readonly FunctionalityXFunctionalityDevelopmentStepDataModel Empty = new FunctionalityXFunctionalityDevelopmentStepDataModel();

		public int? FunctionalityXFunctionalityDevelopmentStepId { get; set; }
		public int? FunctionalityId								 { get; set; }
		public int? FunctionalityDevelopmentStepId               { get; set; }
		public string Functionality								 { get; set; }
		public string FunctionalityDevelopmentStep				 { get; set; }
		public int? SortOrder									 { get; set; }
		public string Version									 { get; set; }

	}
}
