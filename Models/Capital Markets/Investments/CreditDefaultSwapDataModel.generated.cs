using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CreditDefaultSwapDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CreditDefaultSwapId = "CreditDefaultSwapId";
		}

		public static readonly CreditDefaultSwapDataModel Empty = new CreditDefaultSwapDataModel();

	}
}
