using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceListDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? PriceListId { get; set; }

	}
}
