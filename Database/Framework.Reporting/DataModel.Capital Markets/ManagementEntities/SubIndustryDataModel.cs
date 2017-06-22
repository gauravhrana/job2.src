using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class SubIndustryDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string SubIndustryId	= "SubIndustryId";
			public const string Code			= "Code";
		}

		public static readonly SubIndustryDataModel Empty = new SubIndustryDataModel();

		[PrimaryKey]
		public int? SubIndustryId { get; set; }
		public string Code { get; set; }

	}
}
