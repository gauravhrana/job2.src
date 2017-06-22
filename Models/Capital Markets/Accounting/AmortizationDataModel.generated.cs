using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AmortizationDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AmortizationId = "AmortizationId";
		}

		public static readonly AmortizationDataModel Empty = new AmortizationDataModel();

	}
}
