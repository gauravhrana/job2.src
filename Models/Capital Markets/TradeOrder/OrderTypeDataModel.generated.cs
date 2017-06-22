using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class OrderTypeDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string OrderTypeId = "OrderTypeId";
			public const string Code = "Code";
			public const string Description = "Description";
		}

		public static readonly OrderTypeDataModel Empty = new OrderTypeDataModel();

	}
}
