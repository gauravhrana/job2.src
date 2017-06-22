using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class Statistic
	{
		public class DataColumns
		{
			public const string Average = "Average";
			public const string Total = "Total";
			public const string Median = "Median";
			public const string Count = "Count";
			public const string Max = "Max";
			public const string Min = "Min";
		}

		public decimal? Average { get; set; }
		public decimal? Total { get; set; }
		public decimal? Median { get; set; }
		public int? Count { get; set; }
		public decimal? Max { get; set; }
		public decimal? Min { get; set; }
	}
}
