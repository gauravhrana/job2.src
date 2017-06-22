using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PerformanceParametersDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string PerformanceParametersId = "PerformanceParametersId";
		}

		public static readonly PerformanceParametersDataModel Empty = new PerformanceParametersDataModel();
        [PrimaryKey, IncludeInSearch]
		public int? PerformanceParametersId { get; set; }

	}
}
