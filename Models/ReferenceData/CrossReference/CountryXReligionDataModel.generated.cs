using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class CountryXReligionDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string CountryXReligionId = "CountryXReligionId";
			public const string CountryId = "CountryId";
			public const string ReligionId = "ReligionId";
			public const string Country = "Country";
			public const string Religion = "Religion";
		}

		public static readonly CountryXReligionDataModel Empty = new CountryXReligionDataModel();

	}
}
