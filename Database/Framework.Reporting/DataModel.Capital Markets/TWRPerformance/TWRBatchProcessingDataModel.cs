using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class TWRBatchProcessingDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string TWRBatchProcessingId = "TWRBatchProcessingId";
		}

		public static readonly TWRBatchProcessingDataModel Empty = new TWRBatchProcessingDataModel();
        [PrimaryKey, IncludeInSearch]
		public int? TWRBatchProcessingId { get; set; }

	}
}
