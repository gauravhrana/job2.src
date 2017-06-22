using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceProviderDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? PriceProviderId { get; set; }

	}
}
