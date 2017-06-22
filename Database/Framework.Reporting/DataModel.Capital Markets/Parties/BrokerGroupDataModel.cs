using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class BrokerGroupDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string BrokerGroupId = "BrokerGroupId";
		}

		public static readonly BrokerGroupDataModel Empty = new BrokerGroupDataModel();
		[PrimaryKey]
		public int? BrokerGroupId { get; set; }

	}
}
