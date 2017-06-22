using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceScheduleDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string PriceScheduleId = "PriceScheduleId";
		}

		public static readonly PriceScheduleDataModel Empty = new PriceScheduleDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? PriceScheduleId { get; set; }

	}
}
