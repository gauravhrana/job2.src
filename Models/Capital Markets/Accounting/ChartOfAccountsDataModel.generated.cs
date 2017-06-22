using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class ChartOfAccountsDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ChartOfAccountsId = "ChartOfAccountsId";
		}

		public static readonly ChartOfAccountsDataModel Empty = new ChartOfAccountsDataModel();

	}
}
