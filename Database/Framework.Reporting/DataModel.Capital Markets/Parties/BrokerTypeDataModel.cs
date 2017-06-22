using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class BrokerTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string BrokerTypeId = "BrokerTypeId";
		}

		public static readonly BrokerTypeDataModel Empty = new BrokerTypeDataModel();
		[PrimaryKey]
		public int? BrokerTypeId { get; set; }

	}
}
