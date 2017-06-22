using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AccountingViewDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AccountingViewId = "AccountingViewId";
		}

		public static readonly AccountingViewDataModel Empty = new AccountingViewDataModel();
        [PrimaryKey, IncludeInSearch] 
		public int? AccountingViewId { get; set; }

	}
}
