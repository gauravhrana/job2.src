using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class OrderStatusGroupDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string OrderStatusGroupId = "OrderStatusGroupId";
			public const string OrderStatusGroupCode = "OrderStatusGroupCode";
			public const string OrderStatusGroupDescription = "OrderStatusGroupDescription";
			public const string OrderProcessFlag = "OrderProcessFlag";
		}

		public static readonly OrderStatusGroupDataModel Empty = new OrderStatusGroupDataModel();

	}
}
