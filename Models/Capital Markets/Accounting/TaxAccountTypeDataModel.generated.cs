using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TaxAccountTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string TaxAccountTypeId = "TaxAccountTypeId";
		}

		public static readonly TaxAccountTypeDataModel Empty = new TaxAccountTypeDataModel();

	}
}
