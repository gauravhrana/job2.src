using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TxOtherDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TxOtherId = "TxOtherId";
			public const string TransactionEventId = "TransactionEventId";
			public const string TransactionEvent = "TransactionEvent";
			public const string FundStructureId = "FundStructureId";
			public const string CashSourceId = "CashSourceId";
			public const string StrategyId = "StrategyId";
			public const string GenericLegId = "GenericLegId";
			public const string DistributionParentId = "DistributionParentId";
			public const string SettlementTypeId = "SettlementTypeId";
		}

		public static readonly TxOtherDataModel Empty = new TxOtherDataModel();

	}
}
