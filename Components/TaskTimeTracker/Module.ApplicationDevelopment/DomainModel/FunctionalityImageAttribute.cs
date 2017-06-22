using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityImageAttributeDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FunctionalityImageAttributeId = "FunctionalityImageAttributeId";
			public const string FunctionalityImageId = "FunctionalityImageId";
			public const string FunctionalityImage = "FunctionalityImage";
		}

		public static readonly FunctionalityImageAttributeDataModel Empty = new FunctionalityImageAttributeDataModel();

		public int? FunctionalityImageAttributeId { get; set; }
		public int? FunctionalityImageId { get; set; }
		public string FunctionalityImage { get; set; }

	}
}
