using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class PerformanceKeyDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PerformanceKeyId = "PerformanceKeyId";
		}

		public static readonly PerformanceKeyDataModel Empty = new PerformanceKeyDataModel();

	}
}
