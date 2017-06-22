using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class FiscalCalenderDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string FiscalCalenderId = "FiscalCalenderId";
		}

		public static readonly FiscalCalenderDataModel Empty = new FiscalCalenderDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? FiscalCalenderId { get; set; }

	}
}
