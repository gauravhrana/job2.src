using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class OrderStatusTypeDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string OrderStatusTypeId = "OrderStatusTypeId";
			public const string Code = "Code";
			public const string Description = "Description";
			public const string OrderStatusGroupId = "OrderStatusGroupId";
			public const string OrderStatusGroup = "OrderStatusGroup";
		}

		public static readonly OrderStatusTypeDataModel Empty = new OrderStatusTypeDataModel();

	}
}
