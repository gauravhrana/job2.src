using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class RatingServicesDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string RatingServicesId = "RatingServicesId";
			public const string Url = "Url";
			public const string Code = "Code";
		}

		public static readonly RatingServicesDataModel Empty = new RatingServicesDataModel();
        [PrimaryKey, IncludeInSearch] 
		public int? RatingServicesId { get; set; }
		public string Url { get; set; }
		public string Code { get; set; }
	}

	
}
