using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{
	public class ActivityAlgorithmDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ActivityAlgorithmId = "ActivityAlgorithmId";
		}

		public int? ActivityAlgorithmId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"ActivityAlgorithmId=" + ActivityAlgorithmId
		}
	}
}
