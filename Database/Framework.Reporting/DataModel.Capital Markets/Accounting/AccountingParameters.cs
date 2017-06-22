using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AccountingParametersDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AccountingParametersId = "AccountingParametersId";
		}

		public static readonly AccountingParametersDataModel Empty = new AccountingParametersDataModel();
		[PrimaryKey]
		public int? AccountingParametersId { get; set; }

	}
}
