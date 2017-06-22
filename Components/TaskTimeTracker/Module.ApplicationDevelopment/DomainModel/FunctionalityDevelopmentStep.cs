using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityDevelopmentStepDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FunctionalityDevelopmentStepId = "FunctionalityDevelopmentStepId";
			public const string DateCreated					   = "DateCreated";
			public const string DateModified                   = "DateModified";			
		}

		public static readonly FunctionalityDevelopmentStepDataModel Empty = new FunctionalityDevelopmentStepDataModel();

		public int? FunctionalityDevelopmentStepId { get; set; }
		public DateTime? DateCreated			   { get; set; }
		public DateTime? DateModified              { get; set; }	

	}
}
