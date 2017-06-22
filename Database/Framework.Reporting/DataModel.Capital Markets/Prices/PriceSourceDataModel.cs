using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceSourceDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string PriceSourceId = "PriceSourceId";
		}

		public static readonly PriceSourceDataModel Empty = new PriceSourceDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? PriceSourceId { get; set; }

	}
}
