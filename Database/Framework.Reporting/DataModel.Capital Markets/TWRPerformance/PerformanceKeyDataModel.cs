using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PerformanceKeyDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string PerformanceKeyId = "PerformanceKeyId";
		}

		public static readonly PerformanceKeyDataModel Empty = new PerformanceKeyDataModel();
        [PrimaryKey, IncludeInSearch]
		public int? PerformanceKeyId { get; set; }

	}
}
