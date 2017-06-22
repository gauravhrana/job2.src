using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TradingEventTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string TradingEventTypeId = "TradingEventTypeId";
		}

		public static readonly TradingEventTypeDataModel Empty = new TradingEventTypeDataModel();

	}
}
