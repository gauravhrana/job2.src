using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class InvestmentTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string InvestmentTypeId = "InvestmentTypeId";
		}

		public static readonly InvestmentTypeDataModel Empty = new InvestmentTypeDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? InvestmentTypeId { get; set; }

	}
}
