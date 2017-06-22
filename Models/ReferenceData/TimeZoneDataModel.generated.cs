using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class TimeZoneDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string TimeZoneId = "TimeZoneId";
			public const string TimeDifference = "TimeDifference";
		}

		public static readonly TimeZoneDataModel Empty = new TimeZoneDataModel();

	}
}
