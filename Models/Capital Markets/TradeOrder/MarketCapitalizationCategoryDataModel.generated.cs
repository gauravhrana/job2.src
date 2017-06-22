using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class MarketCapitalizationCategoryDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string MarketCapitalizationCategoryId = "MarketCapitalizationCategoryId";
		}

		public static readonly MarketCapitalizationCategoryDataModel Empty = new MarketCapitalizationCategoryDataModel();

	}
}
