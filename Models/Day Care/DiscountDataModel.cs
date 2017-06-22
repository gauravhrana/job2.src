using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class DiscountDataModel : StandardModel
	{
		[PrimaryKey,IncludeInSearch]
		public int? DiscountId { get; set; }
	}
}

