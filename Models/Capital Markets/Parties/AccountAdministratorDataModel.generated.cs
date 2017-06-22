using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AccountAdministratorDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AccountAdministratorId = "AccountAdministratorId";
			public const string Url = "Url";
			public const string Code = "Code";
		}

		public static readonly AccountAdministratorDataModel Empty = new AccountAdministratorDataModel();

	}
}
