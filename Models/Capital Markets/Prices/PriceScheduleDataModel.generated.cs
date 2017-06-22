using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PriceScheduleDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PriceScheduleId = "PriceScheduleId";
		}

		public static readonly PriceScheduleDataModel Empty = new PriceScheduleDataModel();

	}
}
