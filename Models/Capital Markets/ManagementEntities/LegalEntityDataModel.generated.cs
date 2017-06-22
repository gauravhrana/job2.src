using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class LegalEntityDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string LegalEntityId = "LegalEntityId";
			public const string FundId = "FundId";
			public const string Fund = "Fund";
		}

		public static readonly LegalEntityDataModel Empty = new LegalEntityDataModel();

	}
}
