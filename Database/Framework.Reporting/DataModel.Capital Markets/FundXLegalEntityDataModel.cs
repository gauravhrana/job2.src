using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.CapitalMarkets
{
    public class FundXLegalEntityDataModel
    {
        public class DataColumns
        {
            public const string FundXLegalEntityId = "FundXLegalEntityId";

            public const string FundId = "FundId";
            public const string LegalEntityId = "LegalEntityId";

            public const string Fund = "Fund";
            public const string LegalEntity = "LegalEntity";

        }

        public static readonly FundXLegalEntityDataModel Empty = new FundXLegalEntityDataModel();

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
