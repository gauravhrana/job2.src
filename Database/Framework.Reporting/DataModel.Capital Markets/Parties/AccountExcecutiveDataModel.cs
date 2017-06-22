using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class AccountExcecutiveDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AccountExcecutiveId = "AccountExcecutiveId";
		}

		public static readonly AccountExcecutiveDataModel Empty = new AccountExcecutiveDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? AccountExcecutiveId { get; set; }

	}
}
