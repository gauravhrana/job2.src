using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class AddressDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string AddressId = "AddressId";
			public const string CityId = "CityId";
			public const string City = "City";
			public const string StateId = "StateId";
			public const string State = "State";
			public const string CountryId = "CountryId";
			public const string Country = "Country";
			public const string Address1 = "Address1";
			public const string Address2 = "Address2";
			public const string PostalCode = "PostalCode";
		}

		public static readonly AddressDataModel Empty = new AddressDataModel();

	}
}
