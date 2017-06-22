using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class InventoryStateDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string InventoryStateId = "InventoryStateId";
		}

		public static readonly InventoryStateDataModel Empty = new InventoryStateDataModel();

	}
}
