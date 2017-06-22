using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	
	public partial class StrategyGroupDataModel : BaseModel
	{
		
		[PrimaryKey, IncludeInSearch]
        public int? StrategyGroupId { get; set; }

        [ForeignKey("Fund"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? FundId { get; set; }

        [ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
        public string Fund { get; set; }

        [JsonConverter(typeof(NullableIntConverter))]
		public int? ClassificationId { get; set; }
		public int? PortfolioId { get; set; }
		public int? ParentStrategyGroupId { get; set; }
		public int? DefaultUSecurityId { get; set; }
		public int? ActiveYN { get; set; }
		public int? OpenDateId { get; set; }
		public int? CloseDateId { get; set; }
		public int? ClosedYN { get; set; }
		public int? ThemeId { get; set; }

		public string StrategyGroupCode { get; set; }
		public string StrategyGroupDesc { get; set; }

	}
}
