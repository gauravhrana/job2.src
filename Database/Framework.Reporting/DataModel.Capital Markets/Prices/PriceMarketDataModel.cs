using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceMarketDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string PriceMarketId = "PriceMarketId";
		}

		public static readonly PriceMarketDataModel Empty = new PriceMarketDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? PriceMarketId { get; set; }

	}
}
