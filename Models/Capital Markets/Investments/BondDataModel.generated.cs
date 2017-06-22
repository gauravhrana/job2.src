using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class BondDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string BondId = "BondId";
		}

		public static readonly BondDataModel Empty = new BondDataModel();

	}
}
