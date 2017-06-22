using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TWRSetDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string TWRSetId = "TWRSetId";
		}

		public static readonly TWRSetDataModel Empty = new TWRSetDataModel();

	}
}
