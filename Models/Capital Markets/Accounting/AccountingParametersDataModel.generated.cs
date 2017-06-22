using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AccountingParametersDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AccountingParametersId = "AccountingParametersId";
		}

		public static readonly AccountingParametersDataModel Empty = new AccountingParametersDataModel();

	}
}
