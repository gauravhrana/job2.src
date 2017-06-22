using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataModel.TaskTimeTracker.TimeTracking
{
    [Serializable]
	[Table("ScheduleHistory")]
	public class ScheduleHistoryDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ScheduleHistoryId	= "ScheduleHistoryId";
			public const string RecordDate			= "RecordDate";
			public const string ScheduleId			= "ScheduleId";
			public const string Person				= "Person";
			public const string PersonId			= "PersonId";
			public const string WorkDate			= "WorkDate";
			public const string StartTime			= "StartTime";
			public const string EndTime				= "EndTime";
			public const string TotalHoursWorked	= "TotalHoursWorked";
			public const string ScheduleStateName   = "ScheduleStateName";
			public const string NextWorkDate		= "NextWorkDate";
			public const string NextWorkTime		= "NextWorkTime";
			public const string KnowledgeDate		= "KnowledgeDate";
			public const string AcknowledgedById	= "AcknowledgedById";
			public const string AcknowledgedBy		= "AcknowledgedBy";
		}

		public static readonly ScheduleHistoryDataModel Empty = new ScheduleHistoryDataModel();
		[Key]
		public int? ScheduleHistoryId	 { get; set; }
		public int? ScheduleId			 { get; set; }
		public int? PersonId			 { get; set; }
		public DateTime? RecordDate		 { get; set; }		
		public string Person			 { get; set; }
		public DateTime? WorkDate		 { get; set; }
		public DateTime? StartTime		 { get; set; }
		public DateTime? EndTime		 { get; set; }
		public Decimal? TotalHoursWorked { get; set; }
		public string ScheduleStateName  { get; set; }
		public DateTime? NextWorkDate    { get; set; }
		public DateTime? NextWorkTime	 { get; set; }
		public DateTime KnowledgeDate	 { get; set; }
		public int? AcknowledgedById	 { get; set; }
		public string AcknowledgedBy     { get; set; }		

		
	}
}
