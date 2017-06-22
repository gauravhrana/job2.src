using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{
	public class FunctionalityImageXFunctionalityImageAttributeDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string FunctionalityImageXFunctionalityImageAttributeId = "FunctionalityImageXFunctionalityImageAttributeId";
			public const string FunctionalityImageId = "FunctionalityImageId";
			public const string FunctionalityImage = "FunctionalityImage";
			public const string FunctionalityImageAttributeId = "FunctionalityImageAttributeId";
			public const string FunctionalityImageAttribute = "FunctionalityImageAttribute";
		}

		public int? FunctionalityImageXFunctionalityImageAttributeId { get; set; }
		public int? FunctionalityImageId { get; set; }
		public string FunctionalityImage { get; set; }
		public int? FunctionalityImageAttributeId { get; set; }
		public string FunctionalityImageAttribute { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; 
		}
	}
}
