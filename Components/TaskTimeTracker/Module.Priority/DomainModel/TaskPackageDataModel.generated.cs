using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Priority
{

	[Serializable]
	public partial class TaskPackageDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TaskPackageId = "TaskPackageId";
		}

		public static readonly TaskPackageDataModel Empty = new TaskPackageDataModel();

		public int? TaskPackageId { get; set; }

	}
}
