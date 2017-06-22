using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TxTradeAndSettleDatesDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TxTradeAndSettleDatesId = "TxTradeAndSettleDatesId";
			public const string TransactionEventId = "TransactionEventId";
			public const string TransactionEvent = "TransactionEvent";
			public const string TradeDate = "TradeDate";
			public const string ContractualDate = "ContractualDate";
			public const string ActualDate = "ActualDate";
			public const string SpotDate = "SpotDate";
			public const string SettlementDate = "SettlementDate";
			public const string FromSearchTradeDate = "FromSearchTradeDate";
			public const string ToSearchTradeDate = "ToSearchTradeDate";
			public const string FromSearchActualDate = "FromSearchActualDate";
			public const string ToSearchActualDate = "ToSearchActualDate";
			public const string FromSearchContractualDate = "FromSearchContractualDate";
			public const string ToSearchContractualDate = "ToSearchContractualDate";
			public const string CreatedDate = "CreatedDate";
			public const string CreatedByAuditId = "CreatedByAuditId";
			public const string UpdatedDate = "UpdatedDate";
			public const string ModifiedByAuditId = "ModifiedByAuditId";
		}

		public static readonly TxTradeAndSettleDatesDataModel Empty = new TxTradeAndSettleDatesDataModel();

	}
}
