using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class SubIndustryDataModel : StandardModel
	{
		[PrimaryKey]
		public int? SubIndustryId { get; set; }
		public string Code { get; set; }

	}
}
