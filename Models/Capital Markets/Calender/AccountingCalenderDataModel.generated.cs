using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AccountingCalenderDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AccountingCalenderId = "AccountingCalenderId";
		}

		public static readonly AccountingCalenderDataModel Empty = new AccountingCalenderDataModel();

	}
}
