using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SubClassDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SubClassId = "SubClassId";
			public const string FundId = "FundId";
			public const string Fund = "Fund";
		}

		public static readonly SubClassDataModel Empty = new SubClassDataModel();

	}
}
