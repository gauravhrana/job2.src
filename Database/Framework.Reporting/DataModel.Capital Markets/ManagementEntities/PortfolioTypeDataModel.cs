using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class PortfolioTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
            public const string PortfolioTypeId = "PortfolioTypeId";

            public const string FundId = "FundId";
            public const string Fund = "Fund";
		}

		public static readonly PortfolioTypeDataModel Empty = new PortfolioTypeDataModel();

		[PrimaryKey, IncludeInSearch]
        public int? PortfolioTypeId { get; set; }

        [ForeignKey("Fund"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? FundId { get; set; }

        [ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
        public string Fund { get; set; }

	}
}
