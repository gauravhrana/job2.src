using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class LockdownPoolsDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string LockdownPoolsId = "LockdownPoolsId";
		}

		public static readonly LockdownPoolsDataModel Empty = new LockdownPoolsDataModel();

	}
}
