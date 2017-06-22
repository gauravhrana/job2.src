using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.Priority
{
	public partial class TaskPriorityXApplicationUserDataModel
	{
		public class DataColumns : BaseDataModel.BaseDataColumns
		{
			public const string TaskPriorityXApplicationUserId	 = "TaskPriorityXApplicationUserId";
			public const string TaskId							 = "TaskId";
			public const string TaskPriorityTypeId				 = "TaskPriorityTypeId";
			public const string ApplicationUserId				 = "ApplicationUserId";
			public const string ApplicationUser					 = "ApplicationUser";

			public const string Task							 = "Task";
			public const string TaskPriorityType				 = "TaskPriorityType";

			public const string UpdatedByApplicationUser		 = "UpdatedByApplicationUser";			
		}

        public static readonly TaskPriorityXApplicationUserDataModel Empty = new TaskPriorityXApplicationUserDataModel();

        public int?		 TaskPriorityXApplicationUserId		{ get; set; }
		public int?		 TaskId								{ get; set; }
		public int?		 TaskPriorityTypeId					{ get; set; }
		public int?		 ApplicationUserId					{ get; set; }


	}
}
