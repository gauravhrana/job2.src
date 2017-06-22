using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class EntityDateRangeStateTypeDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string EntityDateRangeStateTypeId = "EntityDateRangeStateTypeId";
		}

		public static readonly EntityDateRangeStateTypeDataModel Empty = new EntityDateRangeStateTypeDataModel();

		public int? EntityDateRangeStateTypeId { get; set; }

	}
}
