using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class SecurityXExternalMarketDataIdentifierDataModel : BaseModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? SecurityXExternalMarketDataIdentifierId { get; set; }

		public int? BloombergGlobalId						{ get; set; }
		public string BloombergTicker						{ get; set; }
		public int? BloombergUniqueId						{ get; set; }
		public string BloombergMarketSector					{ get; set; }
		public string RICCode								{ get; set; }
		public string IDCCode								{ get; set; }
		public string RedCode								{ get; set; }
		public string PriceWithSuperDerivatives				{ get; set; }

		[ForeignKey("Security"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? SecurityId { get; set; }

		[ForeignKeyName("Security", "SecurityId", "SecurityId", "Name"), OnlyProperty]
		public string Security { get; set; }
	}
}
