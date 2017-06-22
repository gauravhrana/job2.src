using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class CreditFacilityDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CreditFacilityId = "CreditFacilityId";
		}

		public static readonly CreditFacilityDataModel Empty = new CreditFacilityDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? CreditFacilityId { get; set; }

	}
}
