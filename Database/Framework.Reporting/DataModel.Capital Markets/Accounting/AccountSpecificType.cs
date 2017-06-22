using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AccountSpecificTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AccountSpecificTypeId = "AccountSpecificTypeId";
		}

		public static readonly AccountSpecificTypeDataModel Empty = new AccountSpecificTypeDataModel();
		[PrimaryKey]
		public int? AccountSpecificTypeId { get; set; }

	}
}
