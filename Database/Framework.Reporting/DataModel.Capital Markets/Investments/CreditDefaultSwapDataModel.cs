using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class CreditDefaultSwapDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CreditDefaultSwapId = "CreditDefaultSwapId";
		}

		public static readonly CreditDefaultSwapDataModel Empty = new CreditDefaultSwapDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? CreditDefaultSwapId { get; set; }

	}
}
