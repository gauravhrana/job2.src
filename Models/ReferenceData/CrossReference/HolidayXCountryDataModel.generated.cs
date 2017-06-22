using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class HolidayXCountryDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string HolidayXCountryId = "HolidayXCountryId";
			public const string HolidayId = "HolidayId";
			public const string CountryId = "CountryId";
			public const string Holiday = "Holiday";
			public const string Country = "Country";
		}

		public static readonly HolidayXCountryDataModel Empty = new HolidayXCountryDataModel();

	}
}
