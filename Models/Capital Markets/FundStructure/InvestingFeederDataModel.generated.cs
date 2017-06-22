using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class InvestingFeederDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string InvestingFeederId = "InvestingFeederId";
			public const string FundId = "FundId";
			public const string Fund = "Fund";
		}

		public static readonly InvestingFeederDataModel Empty = new InvestingFeederDataModel();

	}
}
