using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TxTradeFiguresDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TxTradeFiguresId = "TxTradeFiguresId";
			public const string TransactionEventId = "TransactionEventId";
			public const string TransactionEvent = "TransactionEvent";
			public const string Quantity = "Quantity";
			public const string Price = "Price";
			public const string TotalCommission = "TotalCommission";
			public const string BrokerCodeId = "BrokerCodeId";
			public const string GlobalFacilityAmount = "GlobalFacilityAmount";
			public const string ExemptUnrealizedPLfromCapitalRatios = "ExemptUnrealizedPLfromCapitalRatios";
			public const string InternalTradeExcludeInByPassStrategy = "InternalTradeExcludeInByPassStrategy";
			public const string ForwardFXBookCurrencyPricing = "ForwardFXBookCurrencyPricing";
			public const string OriginalFace = "OriginalFace";
			public const string IndexRatio = "IndexRatio";
			public const string PerShareAmount = "PerShareAmount";
			public const string OpeningRate = "OpeningRate";
			public const string PercentageOwned = "PercentageOwned";
			public const string DelayedCompensationId = "DelayedCompensationId";
			public const string ReceiveFinancing = "ReceiveFinancing";
			public const string Yield = "Yield";
			public const string NotionalAmount = "NotionalAmount";
			public const string TradesAsId = "TradesAsId";
			public const string DirtyPrice = "DirtyPrice";
			public const string TradesFlat = "TradesFlat";
			public const string RestateUnrealizedGainOrLossAtPeriodEndSpotRate = "RestateUnrealizedGainOrLossAtPeriodEndSpotRate";
			public const string OverridingFinancingId = "OverridingFinancingId";
			public const string AccrueCommission = "AccrueCommission";
			public const string EffectiveYield = "EffectiveYield";
			public const string NetTrade = "NetTrade";
			public const string PayOrReceiveFullCoupon = "PayOrReceiveFullCoupon";
			public const string ExpirationDate = "ExpirationDate";
			public const string SweepCashOnSettlementDate = "SweepCashOnSettlementDate";
		}

		public static readonly TxTradeFiguresDataModel Empty = new TxTradeFiguresDataModel();

	}
}
