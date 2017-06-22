using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class SwapInvestmentDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string SwapInvestmentId = "SwapInvestmentId";
		}

		public static readonly SwapInvestmentDataModel Empty = new SwapInvestmentDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? SwapInvestmentId { get; set; }

	}
}
