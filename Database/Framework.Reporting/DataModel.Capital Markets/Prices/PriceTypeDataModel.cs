using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string PriceTypeId = "PriceTypeId";
		}

		public static readonly PriceTypeDataModel Empty = new PriceTypeDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? PriceTypeId { get; set; }

	}
}
