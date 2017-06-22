using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class TimeZoneDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TimeZoneId = "TimeZoneId";
		}

		public static readonly TimeZoneDataModel Empty = new TimeZoneDataModel();

		public int? TimeZoneId { get; set; }

	}
}
