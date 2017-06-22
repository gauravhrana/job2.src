using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker.Task
{
    [Serializable]
    [Table("TaskXApplicationUser")]
	public class TaskXApplicationUserDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskXApplicationUserId = "TaskXApplicationUserId";
			public const string TaskId = "TaskId";
			public const string ApplicationUserId = "ApplicationUserId";
			public const string Task = "Task";
			public const string ApplicationUser = "ApplicationUser";
			public const string TaskStatusTypeId = "TaskStatusTypeId";
			public const string StartDate = "StartDate";
			public const string DueDate = "DueDate";
			public const string CompletedDate = "CompletedDate";
		}

        public static readonly TaskXApplicationUserDataModel Empty = new TaskXApplicationUserDataModel();

        [Key]
		public int? TaskXApplicationUserId { get; set; }
		public int? TaskId { get; set; }
		public int? ApplicationUserId { get; set; }
		public int? TaskStatusTypeId { get; set; }
		public int? StartDate { get; set; }
		public int? DueDate { get; set; }
		public int? CompletedDate { get; set; }
		public string Task { get; set; }
		public string ApplicationUser { get; set; }

	}
}
