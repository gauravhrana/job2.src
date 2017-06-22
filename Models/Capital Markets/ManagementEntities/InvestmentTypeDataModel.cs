using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class InvestmentTypeDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? InvestmentTypeId { get; set; }

	}
}
