using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class InventoryStateDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string InventoryStateId = "InventoryStateId";
		}

		public static readonly InventoryStateDataModel Empty = new InventoryStateDataModel();
		[PrimaryKey]
		public int? InventoryStateId { get; set; }

	}
}
