using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Framework.Components.DataAccess;
using Newtonsoft.Json;

namespace DataModel.TaskTimeTracker.TimeTracking
{
	[Serializable]
	[Table("Schedule")]
	public class ScheduleDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{
			public const string ScheduleId = "ScheduleId";
			public const string PersonId = "PersonId";
			public const string ScheduleStateId = "ScheduleStateId";
			public const string Person = "Person";
			public const string ScheduleStateName = "ScheduleStateName";
			public const string WorkDate = "WorkDate";
			public const string StartTime = "StartTime";
			public const string EndTime = "EndTime";
			public const string TotalHoursWorked = "TotalHoursWorked";
			public const string PlannedHours = "PlannedHours";
			public const string ComputedHours = "ComputedHours";
			public const string NextWorkDate = "NextWorkDate";
			public const string NextWorkTime = "NextWorkTime";
			public const string FromSearchDate = "FromSearchDate";
			public const string ToSearchDate = "ToSearchDate";
			public const string IsUpdated = "IsUpdated";
			public const int ScheduleTimeSpentConstant = 1;
			public const string ExcludeItems = "ExcludeItems";
		}

		public static readonly ScheduleDataModel Empty = new ScheduleDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? ScheduleId { get; set; }

		[@ForeignKey("ApplicationUser", "AuthenticationAndAuthorization"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
		public int? PersonId { get; set; }

		[@ForeignKey("ScheduleState"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
		public int? ScheduleStateId { get; set; }


		[JsonConverter(typeof(NullableDateConverter)), SearchProperty]
		public int? ExcludeItems { get; set; }

        [DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
 		public DateTime? WorkDate { get; set; }

        [DateTimeType(DateTimeTypeEnum.Time), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? StartTime { get; set; }

        [DateTimeType(DateTimeTypeEnum.Time), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? EndTime { get; set; }

		public decimal? TotalHoursWorked { get; set; }
		public decimal? PlannedHours { get; set; }
		
		public decimal? ComputedHours { get; set; }

        [DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? NextWorkDate { get; set; }

		[DateTimeType(DateTimeTypeEnum.Time), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? NextWorkTime { get; set; }

		[ForeignKeyName("ApplicationUser", "PersonId", "ApplicationUserId", "ApplicationUserName", "AuthenticationAndAuthorization"), OnlyProperty]
		public string Person { get; set; }

		[ForeignKeyName("ScheduleStateName", "ScheduleStateId", "ScheduleStateId", "Name"), OnlyProperty]
		public string ScheduleStateName { get; set; }

		[IncludeInSearch,JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? FromSearchDate { get; set; }

		[IncludeInSearch,JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? ToSearchDate { get; set; }

	}
}
