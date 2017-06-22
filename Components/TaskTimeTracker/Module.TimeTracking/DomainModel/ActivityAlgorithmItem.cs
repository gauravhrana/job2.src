using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{
	public class ActivityAlgorithmItemDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ActivityAlgorithmItemId		= "ActivityAlgorithmItemId";
			public const string ActivityId					= "ActivityId";
			public const string Activity					= "Activity";
			public const string Description					= "Description";
			public const string SortOrder					= "SortOrder";
		}

		public int? ActivityAlgorithmItemId					{ get; set; }
		public int? ActivityId								{ get; set; }
		public string Activity								{ get; set; }
		public string Description							{ get; set; }
		public int? SortOrder								{ get; set; }

		public string ToURLQuery()
		{
			return String.Empty; 
		}
	}
}
