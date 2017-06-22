using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class InvestmentPricesDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string InvestmentPricesId = "InvestmentPricesId";
		}

		public static readonly InvestmentPricesDataModel Empty = new InvestmentPricesDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? InvestmentPricesId { get; set; }

	}
}
