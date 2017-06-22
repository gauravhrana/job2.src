using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceProviderDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string PriceProviderId = "PriceProviderId";
		}

		public static readonly PriceProviderDataModel Empty = new PriceProviderDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? PriceProviderId { get; set; }

	}
}
