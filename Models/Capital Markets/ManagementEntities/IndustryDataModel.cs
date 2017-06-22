using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class IndustryDataModel : StandardModel
	{
		[PrimaryKey]
		public int? IndustryId { get; set; }
		public string Code { get; set; }

	}
}
