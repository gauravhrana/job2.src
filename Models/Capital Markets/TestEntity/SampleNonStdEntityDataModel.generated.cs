using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.CapitalMarkets
{
	public partial class SampleNonStdEntityDataModel
	{
		public class DataColumns : BaseColumns
		{
			public const string FundXLegalEntityId = "FundXLegalEntityId";

			public const string FundId = "FundId";
			public const string LegalEntityId = "LegalEntityId";

			public const string Fund = "Fund";
			public const string LegalEntity = "LegalEntity";

		}

		public static readonly SampleNonStdEntityDataModel Empty = new SampleNonStdEntityDataModel();
	}
}
