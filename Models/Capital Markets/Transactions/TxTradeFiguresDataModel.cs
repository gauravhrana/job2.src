using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class TxTradeFiguresDataModel : BaseModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? TxTradeFiguresId { get; set; }

		[ForeignKey("TransactionEvent"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? TransactionEventId { get; set; }

		[ForeignKeyName("TransactionEvent", "TransactionEventId", "TransactionEventId", "TransactionEventId"), OnlyProperty]
		public string TransactionEvent { get; set; }
		
		public int Quantity { get; set; }
		public int Price { get; set; }
		public string TotalCommission { get; set; }
		public int? BrokerCodeId { get; set; }
		public string GlobalFacilityAmount { get; set; }
		public string ExemptUnrealizedPLfromCapitalRatios { get; set; }
		public string InternalTradeExcludeInByPassStrategy { get; set; }
		public string ForwardFXBookCurrencyPricing { get; set; }
		public string OriginalFace { get; set; }
		public string IndexRatio { get; set; }
		public string PerShareAmount { get; set; }
		public string OpeningRate { get; set; }
		public string PercentageOwned { get; set; }
		public int? DelayedCompensationId { get; set; }
		public string ReceiveFinancing { get; set; }
		public string Yield { get; set; }
		public string NotionalAmount { get; set; }
		public int? TradesAsId { get; set; }
		public string DirtyPrice { get; set; }
		public string TradesFlat { get; set; }
		public string RestateUnrealizedGainOrLossAtPeriodEndSpotRate { get; set; }
		public int? OverridingFinancingId { get; set; }
		public string AccrueCommission { get; set; }
		public string EffectiveYield { get; set; }
		public string NetTrade { get; set; }
		public string PayOrReceiveFullCoupon { get; set; }

		public string ExpirationDate { get; set; }
		public string SweepCashOnSettlementDate { get; set; }
		
	}
}

