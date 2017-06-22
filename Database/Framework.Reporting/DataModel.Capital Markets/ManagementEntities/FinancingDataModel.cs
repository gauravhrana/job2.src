using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class FinancingDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string FinancingId = "FinancingId";
		}

		public static readonly FinancingDataModel Empty = new FinancingDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? FinancingId { get; set; }

	}
}
