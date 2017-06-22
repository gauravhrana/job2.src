using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Task
{
	
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

		public int? TaskXActivityInstanceId { get; set; }		
		public int? TaskId { get; set; }
		public int? ActivityId { get; set; }
		public string Task { get; set; }
		public string Activity { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"TaskId=" + TaskId
		}
	}
}
