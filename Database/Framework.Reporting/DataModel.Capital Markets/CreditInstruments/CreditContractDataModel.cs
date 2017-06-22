using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class CreditContractDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CreditContractId = "CreditContractId";
		}

		public static readonly CreditContractDataModel Empty = new CreditContractDataModel();
		[PrimaryKey,IncludeInSearch]
		public int? CreditContractId { get; set; }

	}
}
