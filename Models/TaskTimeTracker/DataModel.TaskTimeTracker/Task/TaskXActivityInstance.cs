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
    [Table("TaskXActivityInstance")]
	public class TaskXActivityInstanceDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskXActivityInstanceId = "TaskXActivityInstanceId";
			public const string TaskId		= "TaskId";			
			public const string ActivityId	= "ActivityId";
			public const string Task		= "Task";
			public const string Activity	= "Activity";
		}

        public static readonly TaskXActivityInstanceDataModel Empty = new TaskXActivityInstanceDataModel();

        [Key]
		public int? TaskXActivityInstanceId { get; set; }		
		public int? TaskId { get; set; }
		public int? ActivityId { get; set; }
		public string Task { get; set; }
		public string Activity { get; set; }

	}
}
