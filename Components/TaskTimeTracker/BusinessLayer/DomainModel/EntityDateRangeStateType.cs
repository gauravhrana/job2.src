using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class EntityDateRangeStateTypeDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string EntityDateRangeStateTypeId = "EntityDateRangeStateTypeId";
		}

		public int? EntityDateRangeStateTypeId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty;
		}
	}
}
