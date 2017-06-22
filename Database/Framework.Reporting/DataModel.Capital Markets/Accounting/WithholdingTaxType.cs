using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class WithholdingTaxTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string WithholdingTaxTypeId = "WithholdingTaxTypeId";
		}

		public static readonly WithholdingTaxTypeDataModel Empty = new WithholdingTaxTypeDataModel();
		[PrimaryKey]
		public int? WithholdingTaxTypeId { get; set; }

	}
}
