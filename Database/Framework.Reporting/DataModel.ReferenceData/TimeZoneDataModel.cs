using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class TimeZoneDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string TimeZoneId = "TimeZoneId";

		}

		public static readonly TimeZoneDataModel Empty = new TimeZoneDataModel();

		[PrimaryKey]
		public int? TimeZoneId { get; set; }

	}
}
