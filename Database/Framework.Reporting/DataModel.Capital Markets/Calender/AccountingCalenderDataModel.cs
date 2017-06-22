using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AccountingCalenderDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AccountingCalenderId = "AccountingCalenderId";
		}

		public static readonly AccountingCalenderDataModel Empty = new AccountingCalenderDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? AccountingCalenderId { get; set; }

	}
}
