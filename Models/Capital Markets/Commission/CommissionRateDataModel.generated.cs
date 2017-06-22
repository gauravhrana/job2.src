using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CommissionRateDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string CommissionRateId = "CommissionRateId";
			public const string ClearingRate = "ClearingRate";
			public const string ExecutionRate = "ExecutionRate";
			public const string BrokerId = "BrokerId";
			public const string Broker = "Broker";
			public const string ExchangeId = "ExchangeId";
			public const string Exchange = "Exchange";
		}

		public static readonly CommissionRateDataModel Empty = new CommissionRateDataModel();

	}
}
