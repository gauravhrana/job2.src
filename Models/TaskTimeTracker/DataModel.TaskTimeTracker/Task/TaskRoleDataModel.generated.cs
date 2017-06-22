using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Task
{

	[Serializable]
	public partial class TaskRoleDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TaskRoleId = "TaskRoleId";
		}

		public static readonly TaskRoleDataModel Empty = new TaskRoleDataModel();

		public int? TaskRoleId { get; set; }

	}
}
