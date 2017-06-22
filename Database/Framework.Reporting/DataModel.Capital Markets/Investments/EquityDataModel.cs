using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets

{

	public partial class EquityDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string EquityId = "EquityId";
		}

		public static readonly EquityDataModel Empty = new EquityDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? EquityId { get; set; }

	}
}
