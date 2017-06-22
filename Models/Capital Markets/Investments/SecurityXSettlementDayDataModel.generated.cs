using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SecurityXSettlementDayDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string SecurityXSettlementDayId = "SecurityXSettlementDayId";
			public const string SettlementDay = "SettlementDay";
			public const string SecurityId = "SecurityId";
			public const string Security = "Security";
		}

		public static readonly SecurityXSettlementDayDataModel Empty = new SecurityXSettlementDayDataModel();

	}
}
