using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class IndustryDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string IndustryId = "IndustryId";
			public const string Code = "Code";
		}

		public static readonly IndustryDataModel Empty = new IndustryDataModel();

	}
}
