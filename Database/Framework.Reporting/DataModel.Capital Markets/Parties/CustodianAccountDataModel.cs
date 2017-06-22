using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class CustodianAccountDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CustodianAccountId = "CustodianAccountId";
		}

		public static readonly CustodianAccountDataModel Empty = new CustodianAccountDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? CustodianAccountId { get; set; }

	}
}
