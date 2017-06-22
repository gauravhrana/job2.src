using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class IssuerDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string IssuerId = "IssuerId";
			public const string Url = "Url";
			public const string Code = "Code";

		}

		public static readonly IssuerDataModel Empty = new IssuerDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? IssuerId { get; set; }
		public string Url { get; set; }
		public string Code { get; set; }
	}
}
