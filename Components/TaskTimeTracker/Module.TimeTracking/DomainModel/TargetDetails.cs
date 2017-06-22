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
    [Table("TargetDetails")]
    public class TargetDetailsDataModel : BaseDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string TargetDetailsId                  = "TargetDetailsId";
            public const string PersonId						 = "PersonId";
            public const string ScheduleDetailActivityCategoryId = "ScheduleDetailActivityCategoryId";
            public const string EffectiveDate                    = "EffectiveDate";
            public const string TargetValue                      = "TargetValue";
            public const string CreatedDate						 = "CreatedDate";
            public const string UpdatedDate						 = "UpdatedDate";
            public const string ModifiedDate					 = "ModifiedDate";
            public const string CreatedByAuditId				 = "CreatedByAuditId";
            public const string ModifiedByAuditId				 = "ModifiedByAuditId";

        }
        public static readonly TargetDetailsDataModel Empty = new TargetDetailsDataModel();
		//[Key]

        [PrimaryKey, IncludeInSearch]
        public int? TargetDetailsId { get; set; }

        [@ForeignKey("ScheduleDetailActivityCategory"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
		public int? ScheduleDetailActivityCategoryId { get; set; }

        public int? PersonId { get; set; }        

        [DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
        public DateTime? EffectiveDate { get; set; }

        public Decimal? TargetValue { get; set; }

        [DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
        public DateTime? CreatedDate { get; set; }

        [DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
        public DateTime? UpdatedDate { get; set; }

        [DateTimeType(DateTimeTypeEnum.Date), JsonConverter(typeof(NullableDateConverter))]
        public DateTime? ModifiedDate { get; set; }

		[JsonConverter(typeof(NullableDateConverter)), SearchProperty]
		public int? CreatedByAuditId { get; set; }

		[JsonConverter(typeof(NullableDateConverter)), SearchProperty]
		public int? ModifiedByAuditId { get; set; }
       
    }

}

