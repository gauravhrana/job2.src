using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AmortizationDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AmortizationId = "AmortizationId";
		}

		public static readonly AmortizationDataModel Empty = new AmortizationDataModel();
		[PrimaryKey]
		public int? AmortizationId { get; set; }

	}
}
