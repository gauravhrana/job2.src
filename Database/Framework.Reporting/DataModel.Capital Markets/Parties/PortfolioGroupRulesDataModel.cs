using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class PortfolioGroupRulesDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
            public const string PortfolioGroupRulesId = "PortfolioGroupRulesId";

            public const string FundId = "FundId";
            public const string Fund = "Fund";
		}

		public static readonly PortfolioGroupRulesDataModel Empty = new PortfolioGroupRulesDataModel();

		[PrimaryKey, IncludeInSearch]
        public int? PortfolioGroupRulesId { get; set; }

        [ForeignKey("Fund"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? FundId { get; set; }

        [ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
        public string Fund { get; set; }

	}
}
