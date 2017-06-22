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
	[Table("ScheduleDetail")]
	public class ScheduleDetailDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ScheduleDetailId	             = "ScheduleDetailId";
			public const string ScheduleId			             = "ScheduleId";
			public const string InTime				             = "InTime";
			public const string OutTime			                 = "OutTime";
			public const string Message				             = "Message";
			public const string ScheduleDetailActivityCategoryId = "ScheduleDetailActivityCategoryId";
			public const string CreatedDate			             = "CreatedDate";
			public const string ModifiedDate		             = "ModifiedDate";
			public const string CreatedByAuditId	             = "CreatedByAuditId";
			public const string ModifiedByAuditId	             = "ModifiedByAuditId";
			public const string PersonId			             = "PersonId";
			public const string Person				             = "Person";
			public const string FromSearchDate                   = "FromSearchDate";
			public const string ToSearchDate                     = "ToSearchDate";
			public const string WorkDate			             = "WorkDate";
			public const string DateDiffHrs                      = "DateDiffHrs";
			public const string EmailAddress		             = "EmailAddress";
			public const string ScheduleDetailActivityCategory   = "ScheduleDetailActivityCategory";
            public const string WorkTicket                       = "WorkTicket";
            public const int ScheduleDetailTimeSpentConstant     = 1; 
		}

		public static readonly ScheduleDetailDataModel Empty = new ScheduleDetailDataModel();

		[Key]
		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter))]
		public int? ScheduleDetailId					{ get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter))]
		public int? ScheduleId							{ get; set; }

		[DateTimeType(DateTimeTypeEnum.Time)]
		public DateTime? InTime							{ get; set; }

		[DateTimeType(DateTimeTypeEnum.Time)]
		public DateTime? OutTime						{ get; set; }

		public string Message							{ get; set; }

		[@ForeignKey("ScheduleDetailActivityCategory"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]		
		public int? ScheduleDetailActivityCategoryId	{ get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? FromSearchDate					{ get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? ToSearchDate					{ get; set; }

		[@ForeignKey("ApplicationUser","AuthenticationAndAuthorization"),IncludeInSearch,JsonConverter(typeof(NullableIntConverter))]
		public int? PersonId							{ get; set; }

		[ForeignKeyName("ApplicationUser", "PersonId", "ApplicationUserId", "ApplicationUserName", "AuthenticationAndAuthorization"), OnlyProperty]
		public string Person							{ get; set; }

		
		public decimal? DateDiffHrs						{ get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter))]
		public DateTime? WorkDate						{ get; set; }

		public string EmailAddress						{ get; set; }

		[ForeignKeyName("ScheduleDetailActivityCategory", "ScheduleDetailActivityCategoryId", "ScheduleDetailActivityCategoryId", "Name"), OnlyProperty]
		public string ScheduleDetailActivityCategory	{ get; set; }

		public string WorkTicket                        { get;set; }
	}
}
