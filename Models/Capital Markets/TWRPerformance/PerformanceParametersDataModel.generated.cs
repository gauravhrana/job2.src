using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PerformanceParametersDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PerformanceParametersId = "PerformanceParametersId";
		}

		public static readonly PerformanceParametersDataModel Empty = new PerformanceParametersDataModel();

	}
}
