using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class OrderRequestDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string OrderRequestId = "OrderRequestId";
			public const string EventDate = "EventDate";
			public const string Notes = "Notes";
			public const string LastModifiedBy = "LastModifiedBy";
			public const string LastModifiedOn = "LastModifiedOn";
			public const string ParentOrderRequestId = "ParentOrderRequestId";
			public const string PortfolioId = "PortfolioId";
			public const string Portfolio = "Portfolio";
		}

		public static readonly OrderRequestDataModel Empty = new OrderRequestDataModel();

	}
}
