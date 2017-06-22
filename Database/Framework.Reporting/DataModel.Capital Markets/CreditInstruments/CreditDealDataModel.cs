using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class CreditDealDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CreditDealId = "CreditDealId";
		}

		public static readonly CreditDealDataModel Empty = new CreditDealDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? CreditDealId { get; set; }

	}
}
