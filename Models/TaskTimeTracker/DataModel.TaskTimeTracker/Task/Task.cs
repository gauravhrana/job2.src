using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataModel.TaskTimeTracker.Task
{
    [Serializable]
    [Table("Task")]
	public class TaskDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskId       = "TaskId";
			public const string TaskTypeId   = "TaskTypeId";
			public const string TaskType     = "TaskType";		
		}

        public static readonly TaskDataModel Empty = new TaskDataModel();

        [Key]
		public int? TaskId { get; set; }
		public int? TaskTypeId { get; set; }
		public string TaskType { get; set; }
		
	}
}
