using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class FundDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string FundId = "FundId";
		}

		public static readonly FundDataModel Empty = new FundDataModel();
		
		[PrimaryKey, IncludeInSearch]
		public int? FundId { get; set; }

	}
}
