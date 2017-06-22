using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PositionDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string PositionId = "PositionId";
			public const string InvestmentCode = "InvestmentCode";
			public const string PeriodDate = "PeriodDate";
			public const string CustodianCode = "CustodianCode";
			public const string StrategyCode = "StrategyCode";
			public const string AccountCode = "AccountCode";
			public const string Quantity = "Quantity";
			public const string CostBasis = "CostBasis";
			public const string MarketValue = "MarketValue";
			public const string StartMarketValue = "StartMarketValue";
			public const string DeltaAdjustedExposure = "DeltaAdjustedExposure";
			public const string StartDeltaAdjustedExposure = "StartDeltaAdjustedExposure";
			public const string RealizedPnL = "RealizedPnL";
			public const string UnrealizedPnL = "UnrealizedPnL";
			public const string Mark = "Mark";
		}

		public static readonly PositionDataModel Empty = new PositionDataModel();

	}
}
