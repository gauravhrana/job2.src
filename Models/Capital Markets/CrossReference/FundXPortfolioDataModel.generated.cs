using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class FundXPortfolioDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string FundXPortfolioId = "FundXPortfolioId";
			public const string FundId = "FundId";
			public const string PortfolioId = "PortfolioId";
			public const string Fund = "Fund";
			public const string Portfolio = "Portfolio";
		}

		public static readonly FundXPortfolioDataModel Empty = new FundXPortfolioDataModel();

	}
}
