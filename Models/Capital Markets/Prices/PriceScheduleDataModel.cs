using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PriceScheduleDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? PriceScheduleId { get; set; }

	}
}
