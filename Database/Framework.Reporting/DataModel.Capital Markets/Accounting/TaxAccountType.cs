using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class TaxAccountTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string TaxAccountTypeId = "TaxAccountTypeId";
		}

		public static readonly TaxAccountTypeDataModel Empty = new TaxAccountTypeDataModel();
		[PrimaryKey]
		public int? TaxAccountTypeId { get; set; }

	}
}
