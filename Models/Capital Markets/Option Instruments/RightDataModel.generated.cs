using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class RightDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string RightId = "RightId";
		}

		public static readonly RightDataModel Empty = new RightDataModel();

	}
}
