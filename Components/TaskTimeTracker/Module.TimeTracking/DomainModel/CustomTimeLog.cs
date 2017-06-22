using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{
    [Serializable]
	[Table("CustomTimeLog")]
	public class CustomTimeLogDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{ 
			public const string CustomTimeLogId        = "CustomTimeLogId";
			public const string PersonId               = "PersonId";
			public const string CustomTimeCategory     = "CustomTimeCategory";
			public const string CustomTimeCategoryId   = "CustomTimeCategoryId";
			public const string Person                 = "Person";
			public const string CustomTimeLogKey       = "CustomTimeLogKey";						
			public const string Value                  = "Value";
			public const string Name                   = "Name";
			public const string FromSearchDate         = "FromSearchDate";
			public const string ToSearchDate           = "ToSearchDate";
			public const string PromotedDate           = "PromotedDate";
            public const string Application            = "Application";
			
		}
		 
		public static readonly CustomTimeLogDataModel Empty = new CustomTimeLogDataModel();

		[Key]
        public string Application { get; set; }
		public int? CustomTimeLogId { get; set; }

        [JsonConverter(typeof(NullableIntConverter))]
		public int? PersonId { get; set; }

        [JsonConverter(typeof(NullableIntConverter))]
		public int? CustomTimeCategoryId { get; set; }
		public decimal? Value { get; set; }
		public string CustomTimeLogKey { get; set; }
		public string CustomTimeCategory { get; set; }
		public string Name { get; set; }
		public string Person { get; set; }
		public DateTime? FromSearchDate { get; set; }
		public DateTime? ToSearchDate { get; set; }
		public DateTime? PromotedDate { get; set; }

		//public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public int? CreatedByAuditId { get; set; }
		//public int? ModifiedByAuditId { get; set; }


	}
}
