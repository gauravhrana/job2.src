using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SubIndustryDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SubIndustryId = "SubIndustryId";
			public const string Code = "Code";
		}

		public static readonly SubIndustryDataModel Empty = new SubIndustryDataModel();

	}
}
