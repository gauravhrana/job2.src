using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class FundPricesDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string FundPricesId = "FundPricesId";
		}

		public static readonly FundPricesDataModel Empty = new FundPricesDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? FundPricesId { get; set; }

	}
}
