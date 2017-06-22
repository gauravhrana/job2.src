using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Components.DataAccess;
using Newtonsoft.Json;

namespace DataModel.CapitalMarkets
{
	public partial class SampleNonStdEntityDataModel : BaseModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? FundXLegalEntityId { get; set; }

		[ForeignKey("Fund"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
		public int? FundId { get; set; }

		[ForeignKey("LegalEntity"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
		public int? LegalEntityId { get; set; }

		[ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
		public string Fund { get; set; }
		[ForeignKeyName("LegalEntity", "LegalEntityId", "LegalEntityId", "Name"), OnlyProperty]
		public string LegalEntity { get; set; }
	}
}
