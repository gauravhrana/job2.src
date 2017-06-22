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
	[Table("ScheduleEmail")]
	public class ScheduleEmailDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{
			public const string ScheduleEmailId = "ScheduleEmailId";
			public const string ScheduleId = "ScheduleId";
			public const string SentDate = "SentDate";
			public const string SentTo = "SentTo";
			public const string FromSearchDate = "FromSearchDate";
			public const string ToSearchDate = "ToSearchDate";
			public const string CreatedDate = "CreatedDate";
			public const string UpdatedDate = "UpdatedDate";
			//public const string LastAction = "LastAction";
			public const string ModifiedDate = "ModifiedDate";
			public const string CreatedByAuditId = "CreatedByAuditId";
			public const string ModifiedByAuditId = "ModifiedByAuditId";
			
		}
		public static readonly ScheduleEmailDataModel Empty = new ScheduleEmailDataModel();
		[Key]

		[PrimaryKey, IncludeInSearch]
		public int? ScheduleEmailId { get; set; }

		[@ForeignKey("Schedule"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
		public int? ScheduleId { get; set; }

		[DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? SentDate { get; set; }

		public string SentTo { get; set; }
		[DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? CreatedDate { get; set; }
		[DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? UpdatedDate { get; set; }
		//public string  LastAction { get; set; }
		[DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? ModifiedDate { get; set; }
		[JsonConverter(typeof(NullableDateConverter)), SearchProperty]
		public int? CreatedByAuditId { get; set; }
		[JsonConverter(typeof(NullableDateConverter)), SearchProperty]
		public int? ModifiedByAuditId { get; set; }
		[IncludeInSearch, JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? FromSearchDate { get; set; }

		[IncludeInSearch, JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? ToSearchDate { get; set; }

		
	}
}

