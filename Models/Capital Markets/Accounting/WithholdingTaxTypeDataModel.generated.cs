using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class WithholdingTaxTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string WithholdingTaxTypeId = "WithholdingTaxTypeId";
		}

		public static readonly WithholdingTaxTypeDataModel Empty = new WithholdingTaxTypeDataModel();

	}
}
