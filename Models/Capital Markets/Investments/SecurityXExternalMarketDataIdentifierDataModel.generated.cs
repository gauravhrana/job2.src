using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SecurityXExternalMarketDataIdentifierDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string SecurityXExternalMarketDataIdentifierId = "SecurityXExternalMarketDataIdentifierId";
			public const string BloombergGlobalId = "BloombergGlobalId";
			public const string BloombergTicker = "BloombergTicker";
			public const string BloombergUniqueId = "BloombergUniqueId";
			public const string BloombergMarketSector = "BloombergMarketSector";
			public const string RICCode = "RICCode";
			public const string IDCCode = "IDCCode";
			public const string RedCode = "RedCode";
			public const string PriceWithSuperDerivatives = "PriceWithSuperDerivatives";
			public const string SecurityId = "SecurityId";
			public const string Security = "Security";
		}

		public static readonly SecurityXExternalMarketDataIdentifierDataModel Empty = new SecurityXExternalMarketDataIdentifierDataModel();

	}
}
