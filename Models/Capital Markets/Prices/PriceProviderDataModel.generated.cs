using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PriceProviderDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PriceProviderId = "PriceProviderId";
		}

		public static readonly PriceProviderDataModel Empty = new PriceProviderDataModel();

	}
}
