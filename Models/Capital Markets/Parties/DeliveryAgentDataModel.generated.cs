using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class DeliveryAgentDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string DeliveryAgentId = "DeliveryAgentId";
			public const string Url = "Url";
			public const string Code = "Code";
		}

		public static readonly DeliveryAgentDataModel Empty = new DeliveryAgentDataModel();

	}
}
