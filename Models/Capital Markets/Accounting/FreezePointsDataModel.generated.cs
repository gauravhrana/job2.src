using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class FreezePointsDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string FreezePointsId = "FreezePointsId";
		}

		public static readonly FreezePointsDataModel Empty = new FreezePointsDataModel();

	}
}
