using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class SecurityXPartyDataModel : BaseModel
    {
       
        [PrimaryKey, IncludeInSearch]
        public int? SecurityXPartyId { get; set; }

        [ForeignKey("Exchange"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? ExchangeId { get; set; }

        [ForeignKeyName("Exchange", "ExchangeId", "ExchangeId", "Name"), OnlyProperty]
        public string Exchange { get; set; }

        [ForeignKey("Issuer"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? IssuerId { get; set; }

        [ForeignKeyName("Issuer", "IssuerId", "IssuerId", "Name"), OnlyProperty]
        public string Issuer { get; set; }

        [ForeignKey("DeliveryAgent"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? DeliveryAgentId { get; set; }

        [ForeignKeyName("DeliveryAgent", "DeliveryAgentId", "DeliveryAgentId", "Name"), OnlyProperty]
        public string DeliveryAgent { get; set; }

        [ForeignKey("Security"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? SecurityId { get; set; }

        [ForeignKeyName("Security", "SecurityId", "SecurityId", "Name"), OnlyProperty]
        public string Security { get; set; }
    }
}
