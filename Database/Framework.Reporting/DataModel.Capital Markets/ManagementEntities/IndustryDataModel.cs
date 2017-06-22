using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class IndustryDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string IndustryId	= "IndustryId";
			public const string Code		= "Code";
		}

		public static readonly IndustryDataModel Empty = new IndustryDataModel();

		[PrimaryKey]
		public int? IndustryId { get; set; }
		public string Code { get; set; }

	}
}
