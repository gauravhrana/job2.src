using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class DeliveryAgentDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string DeliveryAgentId = "DeliveryAgentId";
			public const string Url = "Url";
			public const string Code = "Code";

		}

		public static readonly DeliveryAgentDataModel Empty = new DeliveryAgentDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? DeliveryAgentId { get; set; }
		public string Url { get; set; }
		public string Code { get; set; }
	}
}
