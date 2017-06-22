using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceListDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string PriceListId = "PriceListId";
		}

		public static readonly PriceListDataModel Empty = new PriceListDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? PriceListId { get; set; }

	}
}
