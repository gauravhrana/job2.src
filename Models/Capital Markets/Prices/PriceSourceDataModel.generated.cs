using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PriceSourceDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PriceSourceId = "PriceSourceId";
		}

		public static readonly PriceSourceDataModel Empty = new PriceSourceDataModel();

	}
}