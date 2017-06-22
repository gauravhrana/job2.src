using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class FundXLegalEntityDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string FundXLegalEntityId = "FundXLegalEntityId";
			public const string FundId = "FundId";
			public const string LegalEntityId = "LegalEntityId";
			public const string Fund = "Fund";
			public const string LegalEntity = "LegalEntity";
		}

		public static readonly FundXLegalEntityDataModel Empty = new FundXLegalEntityDataModel();

	}
}
