using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class BusinessCalenderDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string BusinessCalenderId = "BusinessCalenderId";
		}

		public static readonly BusinessCalenderDataModel Empty = new BusinessCalenderDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? BusinessCalenderId { get; set; }

	}
}
