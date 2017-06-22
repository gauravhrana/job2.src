using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CommissionCodeDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string CommissionCodeId = "CommissionCodeId";
			public const string CommissionCodeCode = "CommissionCodeCode";
			public const string CommissionCodeDescription = "CommissionCodeDescription";
			public const string BrokerId = "BrokerId";
			public const string Broker = "Broker";
		}

		public static readonly CommissionCodeDataModel Empty = new CommissionCodeDataModel();

	}
}
