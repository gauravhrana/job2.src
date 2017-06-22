using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SecurityTypeGroupDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SecurityTypeGroupId = "SecurityTypeGroupId";
		}

		public static readonly SecurityTypeGroupDataModel Empty = new SecurityTypeGroupDataModel();

	}
}
