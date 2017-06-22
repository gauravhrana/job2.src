using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class ProvinceDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ProvinceId = "ProvinceId";
			public const string CountryId = "CountryId";
			public const string Country = "Country";
			public const string ProvinceTypeId = "ProvinceTypeId";
			public const string ProvinceType = "ProvinceType";
		}

		public static readonly ProvinceDataModel Empty = new ProvinceDataModel();

	}
}
