using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PriceTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PriceTypeId = "PriceTypeId";
		}

		public static readonly PriceTypeDataModel Empty = new PriceTypeDataModel();

	}
}
