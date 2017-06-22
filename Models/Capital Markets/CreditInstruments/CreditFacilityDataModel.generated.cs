using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CreditFacilityDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CreditFacilityId = "CreditFacilityId";
		}

		public static readonly CreditFacilityDataModel Empty = new CreditFacilityDataModel();

	}
}
