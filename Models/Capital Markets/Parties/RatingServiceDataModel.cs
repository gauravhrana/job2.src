using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class RatingServicesDataModel : StandardModel
	{

        [PrimaryKey, IncludeInSearch] 
		public int? RatingServicesId { get; set; }
		public string Url { get; set; }
		public string Code { get; set; }
	}

	
}
