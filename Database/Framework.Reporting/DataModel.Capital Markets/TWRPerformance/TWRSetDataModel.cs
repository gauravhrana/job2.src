using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class TWRSetDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string TWRSetId = "TWRSetId";
		}

		public static readonly TWRSetDataModel Empty = new TWRSetDataModel();
        [PrimaryKey, IncludeInSearch]
		public int? TWRSetId { get; set; }

	}
}
