using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.TasksAndWorkFlow
{
    [Serializable]
	public class TaskEntityTypeDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskEntityTypeId = "TaskEntityTypeId";
			public const string Active = "Active";
		}

        public static readonly TaskEntityTypeDataModel Empty = new TaskEntityTypeDataModel();
      
		public int? TaskEntityTypeId { get; set; }
		public int? Active { get; set; }

	}
}
