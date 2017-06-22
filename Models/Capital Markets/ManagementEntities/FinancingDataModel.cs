using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class FinancingDataModel : StandardModel
	{

		[PrimaryKey, IncludeInSearch]
		public int? FinancingId { get; set; }

	}
}
