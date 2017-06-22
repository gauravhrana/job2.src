using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PriceMarketDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PriceMarketId = "PriceMarketId";
		}

		public static readonly PriceMarketDataModel Empty = new PriceMarketDataModel();

	}
}
