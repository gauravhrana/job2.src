using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SecurityXPartyDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string SecurityXPartyId = "SecurityXPartyId";
			public const string ExchangeId = "ExchangeId";
			public const string Exchange = "Exchange";
			public const string IssuerId = "IssuerId";
			public const string Issuer = "Issuer";
			public const string DeliveryAgentId = "DeliveryAgentId";
			public const string DeliveryAgent = "DeliveryAgent";
			public const string SecurityId = "SecurityId";
			public const string Security = "Security";
		}

		public static readonly SecurityXPartyDataModel Empty = new SecurityXPartyDataModel();

	}
}
