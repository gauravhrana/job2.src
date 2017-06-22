using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class EquityDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string EquityId = "EquityId";
		}

		public static readonly EquityDataModel Empty = new EquityDataModel();

	}
}
