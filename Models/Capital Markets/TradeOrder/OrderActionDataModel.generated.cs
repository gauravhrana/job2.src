using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class OrderActionDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string OrderActionId = "OrderActionId";
			public const string OrderActionCode = "OrderActionCode";
			public const string OrderActionDescription = "OrderActionDescription";
			public const string PositionDirection = "PositionDirection";
		}

		public static readonly OrderActionDataModel Empty = new OrderActionDataModel();

	}
}
