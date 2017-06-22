using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class CityDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CityId = "CityId";
			public const string CountryId = "CountryId";
			public const string Country = "Country";
		}

		public static readonly CityDataModel Empty = new CityDataModel();

	}
}
