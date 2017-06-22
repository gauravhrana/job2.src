using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SecurityTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SecurityTypeId = "SecurityTypeId";
		}

		public static readonly SecurityTypeDataModel Empty = new SecurityTypeDataModel();

	}
}
