using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TxTradeInfoDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TxTradeInfoId = "TxTradeInfoId";
			public const string TransactionEventId = "TransactionEventId";
			public const string TransactionEvent = "TransactionEvent";
			public const string TradeCurrencyId = "TradeCurrencyId";
			public const string BuyCurrencyId = "BuyCurrencyId";
			public const string CrossSettlementFXRate = "CrossSettlementFXRate";
			public const string NetTradeAmount = "NetTradeAmount";
			public const string TradeAccruedInterest = "TradeAccruedInterest";
		}

		public static readonly TxTradeInfoDataModel Empty = new TxTradeInfoDataModel();

	}
}
