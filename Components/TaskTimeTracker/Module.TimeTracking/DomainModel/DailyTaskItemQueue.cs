using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker.TimeTracking
{
    [Serializable]
	public class DailyTaskItemQueueDataModel : StandardDataModel
	{
		
		public class DataColumns : StandardDataColumns
		{
			
			public const string DailyTaskItemQueueId		= "DailyTaskItemQueueId";
			public const string DailyTaskItemQueueStatusId	= "DailyTaskItemQueueStatusId";
			public const string DailyTaskItemQueueStatus    = "DailyTaskItemQueueStatus";
			public const string BusinessDate				= "BusinessDate";
			public const string AssignedBy					= "AssignedBy";
			public const string AssignedTo					= "AssignedTo";
			public const string BusinessDateMin             = "BusinessDateMin";
			public const string BusinessDateMax             = "BusinessDateMax";

		}

		public static readonly DailyTaskItemQueueDataModel Empty = new DailyTaskItemQueueDataModel();

		[Key]
		public int? DailyTaskItemQueueId			{ get; set; }
		public int? DailyTaskItemQueueStatusId		{ get; set; }
		public DateTime? BusinessDate				{ get; set; }
		public string AssignedBy					{ get; set; }
		public string AssignedTo					{ get; set; }
		public string DailyTaskItemQueueStatus		{ get; set; }
		public DateTime? BusinessDateMin			{ get; set; }
		public DateTime? BusinessDateMax			{ get; set; }

	}
}
