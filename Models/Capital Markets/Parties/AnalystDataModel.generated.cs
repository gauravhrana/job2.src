using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AnalystDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AnalystId = "AnalystId";
		}

		public static readonly AnalystDataModel Empty = new AnalystDataModel();

	}
}
