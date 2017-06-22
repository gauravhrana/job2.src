using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class InvestmentPricesDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string InvestmentPricesId = "InvestmentPricesId";
		}

		public static readonly InvestmentPricesDataModel Empty = new InvestmentPricesDataModel();

	}
}
