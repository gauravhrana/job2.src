using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TransactionEventSellShortDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TransactionEventSellShortId = "TransactionEventSellShortId";
			public const string TransactionEventDate = "TransactionEventDate";
			public const string TransactionSettleDate = "TransactionSettleDate";
			public const string TransactionTypeId = "TransactionTypeId";
			public const string TransactionType = "TransactionType";
			public const string CustodianId = "CustodianId";
			public const string Custodian = "Custodian";
			public const string StrategyId = "StrategyId";
			public const string Strategy = "Strategy";
			public const string AccountSpecificTypeId = "AccountSpecificTypeId";
			public const string AccountSpecificType = "AccountSpecificType";
			public const string InvestmentTypeId = "InvestmentTypeId";
			public const string InvestmentType = "InvestmentType";
			public const string Quantity = "Quantity";
			public const string Price = "Price";
			public const string Fees = "Fees";
		}

		public static readonly TransactionEventSellShortDataModel Empty = new TransactionEventSellShortDataModel();

	}
}
