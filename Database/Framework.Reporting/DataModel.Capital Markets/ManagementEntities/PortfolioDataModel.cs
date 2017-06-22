using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Newtonsoft.Json;

namespace DataModel.CapitalMarkets
{
	public partial class PortfolioDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
            public const string PortfolioId = "PortfolioId";

            public const string FundId = "FundId";
            public const string Fund = "Fund";
		}

		public static readonly PortfolioDataModel Empty = new PortfolioDataModel();

		[PrimaryKey, IncludeInSearch]
        public int? PortfolioId { get; set; }

        [ForeignKey("Fund"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? FundId { get; set; }

        [ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
        public string Fund { get; set; }

	}
}
