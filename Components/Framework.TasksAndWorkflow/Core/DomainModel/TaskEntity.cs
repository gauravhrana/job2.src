using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.TasksAndWorkFlow
{
    [Serializable]
	public class TaskEntityDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskEntityId = "TaskEntityId";
			public const string TaskEntityTypeId = "TaskEntityTypeId";
			public const string TaskEntityType = "TaskEntityType";
			public const string Active = "Active";
		}

        public static readonly TaskEntityDataModel Empty = new TaskEntityDataModel();
      
		public int? TaskEntityId { get; set; }
		public int? TaskEntityTypeId { get; set; }
		public string TaskEntityType { get; set; }
		public int? Active { get; set; }

	}
}
