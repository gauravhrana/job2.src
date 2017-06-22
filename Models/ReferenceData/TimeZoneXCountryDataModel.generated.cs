using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class TimeZoneXCountryDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TimeZoneXCountryId = "TimeZoneXCountryId";
			public const string TimeZoneId = "TimeZoneId";
			public const string TimeZone = "TimeZone";
			public const string CountryId = "CountryId";
			public const string Country = "Country";
		}

		public static readonly TimeZoneXCountryDataModel Empty = new TimeZoneXCountryDataModel();

	}
}
