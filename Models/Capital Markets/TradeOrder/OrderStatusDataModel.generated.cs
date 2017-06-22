using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class OrderStatusDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string OrderStatusId = "OrderStatusId";
			public const string OrderId = "OrderId";
			public const string Comments = "Comments";
			public const string LastModifiedBy = "LastModifiedBy";
			public const string LastModifiedOn = "LastModifiedOn";
			public const string OrderStatusTypeId = "OrderStatusTypeId";
			public const string OrderStatusType = "OrderStatusType";
		}

		public static readonly OrderStatusDataModel Empty = new OrderStatusDataModel();

	}
}
