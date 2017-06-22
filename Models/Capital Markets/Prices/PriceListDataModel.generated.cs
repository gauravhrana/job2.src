using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PriceListDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PriceListId = "PriceListId";
		}

		public static readonly PriceListDataModel Empty = new PriceListDataModel();

	}
}
