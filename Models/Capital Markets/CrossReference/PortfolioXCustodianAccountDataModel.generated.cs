using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PortfolioXCustodianAccountDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string PortfolioXCustodianAccountId = "PortfolioXCustodianAccountId";
			public const string CustodianAccountId = "CustodianAccountId";
			public const string PortfolioId = "PortfolioId";
			public const string CustodianAccount = "CustodianAccount";
			public const string Portfolio = "Portfolio";
		}

		public static readonly PortfolioXCustodianAccountDataModel Empty = new PortfolioXCustodianAccountDataModel();

	}
}
