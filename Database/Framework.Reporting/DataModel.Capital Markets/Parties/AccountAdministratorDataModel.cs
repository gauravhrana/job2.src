using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class AccountAdministratorDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AccountAdministratorId = "AccountAdministratorId";
			public const string Url = "Url";
			public const string Code = "Code";
		}

		public static readonly AccountAdministratorDataModel Empty = new AccountAdministratorDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? AccountAdministratorId { get; set; }
		public string Url { get; set; }
		public string Code { get; set; }

	}
}
