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
    [Table("TaskPackageXOwnerXTask")]
	public class TaskPackageXOwnerXTaskDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskPackageXOwnerXTaskId     = "TaskPackageXOwnerXTaskId";
			public const string TaskId                       = "TaskId";
			public const string ApplicationUserId            = "ApplicationUserId";
			public const string Task                         = "Task";
			public const string TaskPackage                  = "TaskPackage";
			public const string ApplicationUser              = "ApplicationUser";
			public const string TaskPackageId                = "TaskPackageId";
		}

        public static readonly TaskPackageXOwnerXTaskDataModel Empty = new TaskPackageXOwnerXTaskDataModel();

        [Key]
		public int? TaskPackageXOwnerXTaskId { get; set; }
		public int? TaskId { get; set; }
		public int? ApplicationUserId { get; set; }
		public int? TaskPackageId { get; set; }
		public string TaskPackage { get; set; }
		public string Task { get; set; }
		public string ApplicationUser { get; set; }

	}
}
