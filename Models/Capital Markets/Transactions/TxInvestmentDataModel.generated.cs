using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TxInvestmentDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TxInvestmentId = "TxInvestmentId";
			public const string TransactionEventId = "TransactionEventId";
			public const string TransactionEvent = "TransactionEvent";
			public const string InvestmentId = "InvestmentId";
			public const string CustAccountId = "CustAccountId";
		}

		public static readonly TxInvestmentDataModel Empty = new TxInvestmentDataModel();

	}
}
