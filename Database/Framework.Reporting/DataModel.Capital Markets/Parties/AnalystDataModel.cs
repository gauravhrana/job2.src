using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class AnalystDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AnalystId = "AnalystId";
		}

		public static readonly AnalystDataModel Empty = new AnalystDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? AnalystId { get; set; }

	}
}
