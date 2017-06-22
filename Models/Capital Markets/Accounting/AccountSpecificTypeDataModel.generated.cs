using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AccountSpecificTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AccountSpecificTypeId = "AccountSpecificTypeId";
		}

		public static readonly AccountSpecificTypeDataModel Empty = new AccountSpecificTypeDataModel();

	}
}
