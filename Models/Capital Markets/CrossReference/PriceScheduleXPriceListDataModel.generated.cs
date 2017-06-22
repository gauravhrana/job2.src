using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PriceScheduleXPriceListDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string PriceScheduleXPriceListId = "PriceScheduleXPriceListId";
			public const string PriceScheduleId = "PriceScheduleId";
			public const string PriceListId = "PriceListId";
			public const string PriceSchedule = "PriceSchedule";
			public const string PriceList = "PriceList";
		}

		public static readonly PriceScheduleXPriceListDataModel Empty = new PriceScheduleXPriceListDataModel();

	}
}
