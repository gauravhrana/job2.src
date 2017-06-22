using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class CreditDefaultSwapIndexDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CreditDefaultSwapIndexId = "CreditDefaultSwapIndexId";
		}

		public static readonly CreditDefaultSwapIndexDataModel Empty = new CreditDefaultSwapIndexDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? CreditDefaultSwapIndexId { get; set; }

	}
}
