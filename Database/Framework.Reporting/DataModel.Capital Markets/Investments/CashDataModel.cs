using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;


namespace DataModel.CapitalMarkets
{

	public partial class CashDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CashId = "CashId";
		}

		public static readonly CashDataModel Empty = new CashDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? CashId { get; set; }

	}
}
