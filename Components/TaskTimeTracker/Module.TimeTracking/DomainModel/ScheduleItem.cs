using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.TimeTracking.DomainModel
{
	[Table("ScheduleItem")]
	public class ScheduleItemDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ScheduleItemId		= "ScheduleItemId";
			public const string ScheduleId			= "ScheduleId";
			public const string TaskFormulationId	= "TaskFormulationId";
			public const string TotalTimeSpent		= "TotalTimeSpent";
		}

		public static readonly ScheduleItemDataModel Empty = new ScheduleItemDataModel();

		[Key]
		public int? ScheduleItemId		{ get; set; }
		public int? ScheduleId			{ get; set; }
		public int? TaskFormulationId	{ get; set; }
		public Decimal? TotalTimeSpent	{ get; set; }

		
	}
}
