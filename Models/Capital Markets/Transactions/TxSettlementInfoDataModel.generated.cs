using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TxSettlementInfoDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TxSettlementInfoId = "TxSettlementInfoId";
			public const string TransactionEventId = "TransactionEventId";
			public const string TransactionEvent = "TransactionEvent";
			public const string SettlementCurrencyId = "SettlementCurrencyId";
			public const string SellCurrencyId = "SellCurrencyId";
			public const string TradeDateFXRate = "TradeDateFXRate";
			public const string NetSettlementAmount = "NetSettlementAmount";
			public const string NetSettlementCashAmount = "NetSettlementCashAmount";
			public const string AccruedInterest = "AccruedInterest";
		}

		public static readonly TxSettlementInfoDataModel Empty = new TxSettlementInfoDataModel();

	}
}
