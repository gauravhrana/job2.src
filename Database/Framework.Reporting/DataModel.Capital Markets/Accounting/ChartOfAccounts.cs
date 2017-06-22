using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class ChartOfAccountsDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string ChartOfAccountsId = "ChartOfAccountsId";
		}

		public static readonly ChartOfAccountsDataModel Empty = new ChartOfAccountsDataModel();
		[PrimaryKey]
		public int? ChartOfAccountsId { get; set; }

	}
}
