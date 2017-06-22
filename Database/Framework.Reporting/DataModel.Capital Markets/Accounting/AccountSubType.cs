using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AccountSubTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AccountSubTypeId = "AccountSubTypeId";
		}

		public static readonly AccountSubTypeDataModel Empty = new AccountSubTypeDataModel();
		[PrimaryKey]
		public int? AccountSubTypeId { get; set; }

	}
}
