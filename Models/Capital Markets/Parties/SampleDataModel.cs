using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class SampleDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string SampleId = "SampleId";
		}

		public static readonly SampleDataModel Empty = new SampleDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? SampleId { get; set; }

	}
}
