using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class TaxStatusDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string TaxStatusId = "TaxStatusId";
		}

		public static readonly TaxStatusDataModel Empty = new TaxStatusDataModel();
		
		[PrimaryKey]
		public int? TaxStatusId { get; set; }

	}
}
