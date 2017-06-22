using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class FundPricesDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string FundPricesId = "FundPricesId";
		}

		public static readonly FundPricesDataModel Empty = new FundPricesDataModel();

	}
}
