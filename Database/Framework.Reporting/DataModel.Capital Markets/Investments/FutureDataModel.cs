using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class FutureDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string FutureId = "FutureId";
		}

		public static readonly FutureDataModel Empty = new FutureDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? FutureId { get; set; }

	}
}
